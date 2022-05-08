const gulp = require("gulp");
let {run} = require('gulp-dotnet-cli');
const ts = require("gulp-typescript");
const webpack = require('webpack-stream');
const tsProject = ts.createProject("tsconfig.json");

gulp.task("typescript", function () {
    return tsProject.src().pipe(tsProject()).js.pipe(gulp.dest("dist"));
});

gulp.task("webpack", ()=> {
    return gulp.src('src/index.tsx').pipe(webpack(require('./webpack.config.js'))).pipe(gulp.dest('dist/'));
})

gulp.task("run", ()=> {
    return gulp.src('../WebServer/WebServer/WebServer.csproj', {read: false}).pipe(run())
})

gulp.task("default", gulp.series(gulp.parallel("run", "typescript")))