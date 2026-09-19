import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";
import path from "path";

const dirname = import.meta.dirname;

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
  resolve: {
    alias: {
      "@": path.resolve(dirname, "./src"),
      "@sharedUi": path.resolve(dirname, "./src/shared/components/ui"),
      "@sharedForms": path.resolve(dirname, "./src/shared/components/forms"),
      "@account": path.resolve(dirname, "./src/features/account"),
    },
  },
  build: {
    outDir: "../api/Woodshed.API/wwwroot",
    chunkSizeWarningLimit: 1500,
    emptyOutDir: true,
  },
});
