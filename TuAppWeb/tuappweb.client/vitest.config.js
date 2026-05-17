import { fileURLToPath } from 'node:url'
import { mergeConfig, defineConfig } from 'vite'
import { defineConfig as defineVitestConfig } from 'vitest/config'
import vue from '@vitejs/plugin-vue'

export default mergeConfig(
  defineConfig({
    plugins: [vue()],
    resolve: {
      alias: {
        '@': fileURLToPath(new URL('./src', import.meta.url)),
      },
    },
  }),
  defineVitestConfig({
    test: {
      environment: 'happy-dom',
      coverage: {
        provider: 'v8',
        reporter: ['text', 'json-summary'],
        include: ['src/**/*.{js,vue}'],
        exclude: ['src/main.js'],
        thresholds: {
          lines: 70,
          branches: 70,
          statements: 70,
        },
      },
    },
  }),
)
