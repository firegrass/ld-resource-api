module Config
#r "packages/Suave/lib/net40/Suave.dll"

open Suave

let config = { defaultConfig with
                             homeFolder = Some ("/data/publishedstatements")}
