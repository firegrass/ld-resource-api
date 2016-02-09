module App
#r "packages/Suave/lib/net40/Suave.dll"

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors
open System.IO

let createFile path (req:HttpRequest) =
  CREATED "arse"

let app =
    choose
        [ POST >=> choose
            [ pathScan "/statement/%s" (fun path -> request(fun req -> createFile path req))]
          NOT_FOUND "Found no handlers" ]
