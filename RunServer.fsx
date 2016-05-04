#load "App.fsx"
#load "Config.fsx"

open Suave
open App
open Config

printf "Running server\n"

let defaultConfig =
  { defaultConfig with
                  bindings = [ HttpBinding.mkSimple HTTP "0.0.0.0" 8083 ]}
startWebServer config app
