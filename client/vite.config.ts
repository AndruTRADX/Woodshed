import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";
import path from "path";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
      "@sharedUi": path.resolve(__dirname, "./src/shared/components/ui"),
      "@sharedForms": path.resolve(__dirname, "./src/shared/components/forms"),
      "@account": path.resolve(__dirname, "./src/features/account"),
    },
  },
  build: {
    outDir: "../api/Woodshed.API/wwwroot",
    chunkSizeWarningLimit: 1500,
    emptyOutDir: true,
  },
});
