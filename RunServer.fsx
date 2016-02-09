module RunServer
#load "App.fsx"

open Suave
open App

printf "Running server\n"

startWebServer defaultConfig app
