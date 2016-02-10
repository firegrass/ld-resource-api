module App
#r "packages/Suave/lib/net40/Suave.dll"

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors
open System.IO

let writeFile path (req:HttpRequest) =
  let dir = "/data/publishedstatements/" + path
  let file = dir + "/Statement.html"

  dir |> Directory.CreateDirectory |> ignore
  File.WriteAllBytes (file, req.rawForm)
  CREATED (sprintf "Created %s" path)

let app =
  choose
    [ POST >=> pathScan "/publishedstatement/%s" (fun path -> request(fun req -> writeFile path req))
      NOT_FOUND "Found no handlers" ]
