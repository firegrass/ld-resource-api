#load "App.fsx"
#load "Config.fsx"

open Suave
open App
open Config

printf "Running server\n"

startWebServer config app
