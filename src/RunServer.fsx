#load "App.fsx"

open Suave

printf "Running server\n"

startWebServer defaultConfig app
