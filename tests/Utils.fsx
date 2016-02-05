module Utils
#load "../src/App.fsx"
#r "../packages/Suave/lib/net40/Suave.dll"
#r "../packages/Suave.Testing/lib/net40/Suave.Testing.dll"

open App
open Suave
open Suave.Testing

let startServerWith () =
  runWith defaultConfig app

let get path testCtx = reqQuery HttpMethod.GET path "" testCtx
