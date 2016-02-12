module Config
#r "packages/Suave/lib/net40/Suave.dll"

open Suave

let rootDir = "/data"
let config = { defaultConfig with
                             homeFolder = Some (rootDir)}

