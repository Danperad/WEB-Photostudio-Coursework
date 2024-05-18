import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { VitePWA } from 'vite-plugin-pwa'
import * as path from 'path'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    react(),
    VitePWA({
      srcDir: path.resolve(__dirname, 'src'),
      filename: "sw.js",
      registerType: "autoUpdate",
      injectRegister: 'auto',
      manifest: {
        short_name: 'Sunrise Employee',
        name: 'Sunrise Employee',
        start_url: '.',
        display: 'standalone',
        theme_color: '#000000',
        background_color: '#ffffff',
        icons: [
          {
            src: 'pwa-144x144.png',
            sizes: '144x144',
            type: 'image/png'
          },
          {
            src: 'pwa-192x192.png',
            sizes: '192x192',
            type: 'image/png'
          },
          {
            src: 'pwa-512x512.png',
            sizes: '512x512',
            type: 'image/png'
          }
        ]
      }
    })
  ],
  resolve: {
    alias: [
      {find: '@', replacement: path.resolve(__dirname, 'src')},
    ]
  }
})
