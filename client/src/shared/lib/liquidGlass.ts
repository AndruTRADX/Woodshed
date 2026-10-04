// `depth` sets the width of the surface's distortion edge. A number is a fixed px
// width; `{ percent }` scales it to the surface's own size (percent of its shorter
// side), so the edge stays proportionally sized instead of a fixed px reading too
// thick on a small surface or too thin on a large one.
export type LiquidGlassDepth = number | { percent: number };

export function resolveDepth(
  depth: LiquidGlassDepth,
  width: number,
  height: number,
) {
  if (typeof depth === "number") return depth;
  return (depth.percent / 100) * Math.min(width, height);
}

interface DisplacementMapParams {
  width: number;
  height: number;
  radius: number;
  depth: number;
  normalPow?: number;
}

interface DisplacementFilterParams extends DisplacementMapParams {
  strength: number;
  chromaticAberration: number;
}

// Signed distance from `(px, py)` (relative to the centre) to a rounded rectangle
// with half-size `(hw, hh)` and corner radius `r`: negative inside, 0 on the edge.
// Same function as `roundedRectangleDist` in kwin-effects-glass' glass.glsl.
function roundedRectDist(
  px: number,
  py: number,
  hw: number,
  hh: number,
  r: number,
) {
  const qx = Math.abs(px) - hw + r;
  const qy = Math.abs(py) - hh + r;
  return (
    Math.min(Math.max(qx, qy), 0) +
    Math.hypot(Math.max(qx, 0), Math.max(qy, 0)) -
    r
  );
}

const smoothstep = (t: number) => t * t * (3 - 2 * t);

// Per-pixel displacement for a rounded-rect lens, encoded for feDisplacementMap
// (R = x, G = y, 128 = no shift). Each pixel is pushed along the edge *normal* by a
// convex bevel profile, so straight edges only bend perpendicular to themselves and
// corners bend radially - instead of the old linear-gradient map, which also smeared
// content sideways along straight edges. Ported from kwin-effects-glass:
//   edgeFactor   = 1 - |dist| / band
//   concave      = 1 - sqrt(1 - smoothstep(edgeFactor)^normalPow)
//   sample point = p - outwardNormal * concave   (i.e. pull from inside → lens)
export function computeDisplacementPixels({
  width,
  height,
  radius,
  depth,
  normalPow = 3,
}: DisplacementMapParams) {
  const w = Math.max(1, Math.round(width));
  const h = Math.max(1, Math.round(height));
  const hw = w / 2;
  const hh = h / 2;
  const r = Math.min(radius, hw, hh);
  const band = Math.max(0.1, Math.min(depth, Math.min(hw, hh) * 0.9));
  const data = new Uint8ClampedArray(w * h * 4);

  for (let y = 0; y < h; y++) {
    const py = y + 0.5 - hh;
    for (let x = 0; x < w; x++) {
      const px = x + 0.5 - hw;
      const i = (y * w + x) * 4;
      let dx = 0;
      let dy = 0;

      const dist = roundedRectDist(px, py, hw, hh, r);
      if (dist < 0 && -dist < band) {
        const edge = 1 - -dist / band;
        const concave =
          1 - Math.sqrt(1 - Math.pow(smoothstep(edge), normalPow));
        const gx =
          roundedRectDist(px + 1, py, hw, hh, r) -
          roundedRectDist(px - 1, py, hw, hh, r);
        const gy =
          roundedRectDist(px, py + 1, hw, hh, r) -
          roundedRectDist(px, py - 1, hw, hh, r);
        const len = Math.hypot(gx, gy) || 1;
        dx = -(gx / len) * concave;
        dy = -(gy / len) * concave;
      }

      data[i] = 128 + dx * 127;
      data[i + 1] = 128 + dy * 127;
      data[i + 2] = 128;
      data[i + 3] = 255;
    }
  }

  return { data, width: w, height: h };
}

// Maps are pure functions of their params, and surfaces re-measure on every resize,
// so cache the encoded PNGs. Small LRU: there are only a handful of glass surfaces.
const MAP_CACHE_LIMIT = 32;
const mapCache = new Map<string, string>();

export function buildDisplacementMap(params: DisplacementMapParams) {
  const w = Math.max(1, Math.round(params.width));
  const h = Math.max(1, Math.round(params.height));
  const key = `${w}x${h}|${params.radius.toFixed(1)}|${params.depth.toFixed(1)}|${params.normalPow ?? 3}`;

  const cached = mapCache.get(key);
  if (cached) {
    mapCache.delete(key);
    mapCache.set(key, cached);
    return cached;
  }

  const { data } = computeDisplacementPixels({
    ...params,
    width: w,
    height: h,
  });
  const canvas = document.createElement("canvas");
  canvas.width = w;
  canvas.height = h;
  canvas.getContext("2d")?.putImageData(new ImageData(data, w, h), 0, 0);
  const url = canvas.toDataURL("image/png");

  mapCache.set(key, url);
  if (mapCache.size > MAP_CACHE_LIMIT) {
    mapCache.delete(mapCache.keys().next().value as string);
  }
  return url;
}

export function buildDisplacementFilter({
  width,
  height,
  radius,
  depth,
  normalPow,
  strength,
  chromaticAberration,
}: DisplacementFilterParams) {
  const displacementMapUrl = buildDisplacementMap({
    width,
    height,
    radius,
    depth,
    normalPow,
  });

  const svg = `<svg height="${height}" width="${width}" viewBox="0 0 ${width} ${height}" xmlns="http://www.w3.org/2000/svg">
    <defs>
      <filter id="displace" color-interpolation-filters="sRGB">
        <feImage x="0" y="0" height="${height}" width="${width}" href="${displacementMapUrl}" result="displacementMap" />
        <feDisplacementMap
          transform-origin="center"
          in="SourceGraphic"
          in2="displacementMap"
          scale="${strength + chromaticAberration * 2}"
          xChannelSelector="R"
          yChannelSelector="G" />
        <feColorMatrix
          type="matrix"
          values="1 0 0 0 0
                  0 0 0 0 0
                  0 0 0 0 0
                  0 0 0 1 0"
          result="displacedR" />
        <feDisplacementMap
          in="SourceGraphic"
          in2="displacementMap"
          scale="${strength + chromaticAberration}"
          xChannelSelector="R"
          yChannelSelector="G" />
        <feColorMatrix
          type="matrix"
          values="0 0 0 0 0
                  0 1 0 0 0
                  0 0 0 0 0
                  0 0 0 1 0"
          result="displacedG" />
        <feDisplacementMap
          in="SourceGraphic"
          in2="displacementMap"
          scale="${strength}"
          xChannelSelector="R"
          yChannelSelector="G" />
        <feColorMatrix
          type="matrix"
          values="0 0 0 0 0
                  0 0 0 0 0
                  0 0 1 0 0
                  0 0 0 1 0"
          result="displacedB" />
        <feBlend in="displacedR" in2="displacedG" mode="screen" />
        <feBlend in2="displacedB" mode="screen" />
      </filter>
    </defs>
  </svg>`;

  return `data:image/svg+xml;utf8,${encodeURIComponent(svg)}#displace`;
}

let svgFilterSupport: boolean | null = null;

export function detectLiquidGlassSupport() {
  if (svgFilterSupport !== null) return svgFilterSupport;

  const testElement = document.createElement("div");
  testElement.style.backdropFilter = "blur(1px)";

  if (!testElement.style.backdropFilter) {
    svgFilterSupport = false;
    return svgFilterSupport;
  }

  const userAgent = navigator.userAgent.toLowerCase();
  const isChrome =
    /chrome|chromium|crios|edg/.test(userAgent) &&
    !/firefox|fxios/.test(userAgent);
  const isFirefox = /firefox|fxios/.test(userAgent);
  const isSafari =
    /safari/.test(userAgent) && !/chrome|chromium|crios|edg/.test(userAgent);

  if (isChrome) {
    svgFilterSupport = true;
  } else if (isFirefox || isSafari) {
    svgFilterSupport = false;
  } else {
    testElement.style.backdropFilter = "url(#test)";
    svgFilterSupport = testElement.style.backdropFilter.includes("url");
  }

  return svgFilterSupport;
}

// "large" is for big, low-frequency surfaces (navbar, dialogs, toasts, the profile
// avatar frame). "compact" is for small/dense ones (Select, Combobox, Popover,
// DropdownMenu, HoverCard, glass-* buttons) - a thinner bevel and softer lens so the
// distortion still reads at that size.
//
// Values follow the macOS-26 tuning used for kwin-effects-glass: a wide-ish bevel
// with a steep profile (lensing hugs the rim, centre stays clear) and only a hint of
// RGB dispersion. Brightness/saturation are not here: they're theme tokens
// (--glass-brightness / --glass-saturate in styles.css), because dark glass wants to
// be slightly dimmer and light glass slightly brighter.
//
// `strength` is the feDisplacementMap scale: the max pull at the rim is ~strength/2 px.
export const LIQUID_GLASS_PRESETS = {
  large: {
    depth: 18,
    normalPow: 3,
    blur: 2,
    strength: 48,
    chromaticAberration: 1.5,
  },
  compact: {
    depth: 8,
    normalPow: 3,
    blur: 3,
    strength: 32,
    chromaticAberration: 1,
  },
} as const;

export type LiquidGlassPreset = keyof typeof LIQUID_GLASS_PRESETS;
