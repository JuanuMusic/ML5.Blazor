const path = require("path");

module.exports = {
    module: {
        rules: [
            {
                test: /\.(js)$/,
                exclude: '/node_modules/',
                use: {
                    loader: "babel-loader"
                }
            }
        ]
    },
    output: {
        path: __dirname,
        filename: "ml5Interop.js",
        library: "ML5Interop"
    }
};