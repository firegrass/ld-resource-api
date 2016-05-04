module Config
#r "packages/Suave/lib/net40/Suave.dll"

open Suave

let rootDir = "/data"
let config = { defaultConfig with
                             bindings = [ HttpBinding.mkSimple HTTP "0.0.0.0" 8083 ]
                             homeFolder = Some (rootDir)}
