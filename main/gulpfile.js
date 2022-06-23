const gulp = require("gulp");
const {run} = require('gulp-dotnet-cli');
const ts = require("gulp-typescript");
const webpack = require('webpack-stream');
const tsProject = ts.createProject("tsconfig.json");
const runCommand = require('gulp-run-command');
const shell = require('gulp-shell');

gulp.task("webpack", () => {
    return gulp.src('src/index.tsx').pipe(webpack(require('./webpack.config.js'))).pipe(gulp.dest('dist/'));
})

gulp.task("http", () => {
    return gulp.src('../WebServer/WebServer/WebServer.csproj', {read: false}).pipe(run())
})

gulp.task("serve-dev", () => {
    return gulp.src('./').pipe(shell((['npm run start'])))
});

gulp.task("build", () => {
    return gulp.src('./').pipe(shell((['npm run build'])))
})

gulp.task("serve-prod", () => {
    return gulp.src('./').pipe(shell((['serve dist'])))
})

gulp.task('prod', gulp.series("webpack", "serve-prod"))

gulp.task("full-prod", gulp.series(gulp.parallel("http", "prod")))

gulp.task("default", gulp.series(gulp.parallel("http", "serve-dev")))