module App
#load "Config.fsx"
#r "packages/Suave/lib/net40/Suave.dll"

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors
open System.IO
open Config

let writeFile path (req:HttpRequest) =
  let dir = (sprintf "%s/publishedstatements/%s" rootDir path)
  let file = dir + "/Statement.html"

  dir |> Directory.CreateDirectory |> ignore
  File.WriteAllBytes (file, req.rawForm)
  CREATED (sprintf "Created %s" path)

let app =
  choose
    [ POST >=> pathScan "/publishedstatements/%s" (fun path -> request(fun req -> writeFile path req))
      Files.browse rootDir
      NOT_FOUND "Found no handlers" ]
