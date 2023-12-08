const { defineConfig } = require("@vue/cli-service");

module.exports = defineConfig({
  transpileDependencies: ["vuetify", "vuex-module-decorators"],
  outputDir: "dist",
  devServer: {
    host: "localhost",
    port: process.env.VUE_APP_PROXY_PORT,
    hot: true,
    open: true,
    headers: { "Access-Control-Allow-Origin": "*" },
    allowedHosts: "all",
    proxy: {
      "/api/data": {
        target: process.env.VUE_APP_API_BE_DATA_URL,
        changeOrigin: true,
        pathRewrite: {
          "^/api/data": "",
        },
      },
      "/api/publicdata": {
        target: process.env.VUE_APP_API_BE_PUBLICDATA_URL,
        changeOrigin: true,
        pathRewrite: {
          "^/api/publicdata": "",
        },
      },
      "/api/document": {
        target: process.env.VUE_APP_API_BE_DOCUMENT_URL,
        changeOrigin: true,
        pathRewrite: {
          "^/api/document": "",
        },
      },
    },
  },
  configureWebpack: {
    devtool: "source-map",
  },
});
