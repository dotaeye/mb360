var webpack = require('webpack');
var path = require('path');
var fs = require('fs');
var CHUNK_REGEX = /^([A-Za-z0-9_\-]+)\..*/;
var _ = require('lodash');
var babelrc = fs.readFileSync('./.babelrc');
var babelLoaderQuery={};
try {
    babelLoaderQuery = JSON.parse(babelrc);
    console.log(babelLoaderQuery);
} catch (err) {
    console.error('==>     ERROR: Error parsing your .babelrc.');
    console.error(err);
}


var env = {
    hot_server_host:'127.0.0.1',
    hot_server_port:5591
};
var config = {
    resolve: {
        extensions: ['', '.js', '.jsx']
    },
    entry: [
        'webpack-dev-server/client?http://' + env.hot_server_host + ':' + env.hot_server_port,
        'webpack/hot/dev-server',
        './src/client.js'
    ],
    output: {
        path: path.join(__dirname, '/src/public/build/js'),
        filename: '[name].js',
        publicPath: 'http://' + env.hot_server_host + ':' + env.hot_server_port + '/'
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                loaders: ['react-hot', 'babel?'+JSON.stringify(babelLoaderQuery),]
            },{
                test: /\.json$/,
                exclude: /node_modules/,
                loaders:['json-loader']
            },
            {
                test: /\.css$/,
                loaders: ['style-loader', 'css-loader']
            },
            {
                test: /\.less$/,
                loaders: ['style-loader','css-loader','less-loader']
            }
        ]
    },
    plugins: [
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: JSON.stringify('development')
            }
        }),

        // See:
        // https://github.com/yahoo/fluxible/issues/138
        new webpack.IgnorePlugin(/vertx/),
        new webpack.HotModuleReplacementPlugin(),
        new webpack.NoErrorsPlugin(),
        new webpack.optimize.CommonsChunkPlugin('common.js', undefined, 2)
        // new webpack.NormalModuleReplacementPlugin(/^react(\/addons)?$/, require.resolve('react/addons'))
    ],
    stats: {
        colors: true
    },
    devtool: "eval"
};

module.exports = config;