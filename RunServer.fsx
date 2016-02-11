module RunServer
#load "App.fsx"
#load "Config.fsx"

open Suave
open App

printf "Running server\n"

startWebServer config app
