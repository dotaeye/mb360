var webpack = require('webpack');
var path = require('path');
var fs = require('fs');
var webpackDevConfig = require('./webpack.dev.config');
var CHUNK_REGEX = /^([A-Za-z0-9_\-]+)\..*/;
var babelrc = fs.readFileSync('./.babelrc');
var babelLoaderQuery = {};
try {
  babelLoaderQuery = JSON.parse(babelrc);
} catch (err) {
  console.error('==>     ERROR: Error parsing your .babelrc.');
  console.error(err);
}
var env = {
  hot_server_host: '127.0.0.1',
  hot_server_port: 5591
};

module.exports = function (grunt) {

  require("load-grunt-tasks")(grunt);

  grunt.initConfig({
    // project variables
    project: {
      build: path.join(__dirname, '/src/public/build'),
      public: 'src/public',
      test: './unitTest/build'

    },
    // clean build
    clean: [path.join(__dirname, '/src/public/build/js')],

    less: {
      dev: {
        files: {
          './src/public/styles/main.css': './src/public/styles/main.less',
        }
      },
      prod: {
        files: {
          './src/public/styles/main.css': './src/public/styles/main.less',
        },
        options: {
          compress: true
        }
      }
    },
    // webpack bundling
    webpack: {
      prod: {
        resolve: {
          extensions: ['', '.js', '.jsx']
        },
        entry: './src/client.js',
        output: {
          path: '<%= project.build %>/js',
          filename: '[name].min.js',
          chunkFilename: '[name].min.js'
        },
        module: {
          loaders: [
            {
              test: /\.jsx?$/,
              exclude: /node_modules/,
              loaders: ['react-hot', 'babel?' + JSON.stringify(babelLoaderQuery),]
            }, {
              test: /\.json$/,
              exclude: /node_modules/,
              loaders: ['json-loader']
            },
            {
              test: /\.css$/,
              loaders: ['style-loader', 'css-loader']
            },
            {
              test: /\.less$/,
              loaders: ['style-loader', 'css-loader', 'less-loader']
            }
          ]
        },
        plugins: [
          new webpack.DefinePlugin({
            'process.env': {
              NODE_ENV: JSON.stringify('production')
            }
          }),

          // These are performance optimizations for your bundles
          new webpack.optimize.DedupePlugin(),
          new webpack.optimize.OccurenceOrderPlugin(),
          new webpack.optimize.CommonsChunkPlugin('common.min.js', 2),
          new webpack.optimize.UglifyJsPlugin({
            compress: {
              warnings: false
            },
            output: {
              comments: false
            }
          })
        ],
        // removes verbosity from builds
        progress: false
      }
    },
    karma: {
      unit: {
        configFile: 'karma.conf.js'
      }
    },

    'webpack-dev-server': {
      options: {
        hot: true,
        historyApiFallback: true,
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Headers": "Origin, X-Requested-With, Content-Type, Accept"
        },
        port: env.hot_server_port,
        webpack: webpackDevConfig,
        publicPath: webpackDevConfig.output.publicPath,
        contentBase: 'http://' + env.hot_server_host + ':' + env.hot_server_port
      },
      start: {
        keepAlive: true
      }
    },

    watch: {
      options: {
        nospawn: true
      },

      less: {
        files: ["<%= project.public %>/styles/**/*.less"],
        tasks: ["less:dev"]
      }
    }

  });

  //development environment task
  grunt.registerTask('default', ['clean', 'less:dev', 'webpack-dev-server']);

  //production environment task
  grunt.registerTask('prod', ['clean', 'less:prod', 'webpack:prod']);

  //test task
  grunt.registerTask('test', ['clean', 'webpack:test', 'karma']);

};