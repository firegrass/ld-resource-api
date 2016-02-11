#load "App.fsx"
#load "TestUtils.fsx"
#load "TestRunner.fsx"

#r "System.Core.dll"
#r "System.dll"
#r "packages/Fuchu/lib/Fuchu.dll"
#r "packages/Unquote/lib/net45/Unquote.dll"
#r "packages/Suave.Testing/lib/net40/Suave.Testing.dll"
#r "packages/Microsoft.Net.Http/lib/net40/System.Net.Http.dll"

open Fuchu
open Fuchu.Test
open Fuchu.Impl
open Swensen.Unquote
open Suave
open Suave.Testing
open TestRunner
open TestUtils
open System
open System.Net
open System.IO
open System.Net.Http
open App

let setup () =
  Directory.CreateDirectory "/data/publishedstatements" |> ignore

let teardown () =
  Directory.Delete("/data/publishedstatements", true)

let runTestWith setup teardown test () =
  setup ()
  let r = test ()
  teardown ()
  r

let tests =
  testList "published statement api" [
    testList "POST a statement" [

      yield! testFixture (runTestWith setup teardown) [
        "should return CREATED 201", fun _ ->
          let res = post "/publishedstatement/qs1/st1" ""
          test <@ res.StatusCode = HttpStatusCode.Created @>

        "should create the statement on disk", fun _ ->
          post "/publishedstatement/qs1/st1" "content" |> ignore
          test <@ File.ReadAllText "/data/publishedstatements/qs1/st1/Statement.html" = "content" @>

        "should update the statement on disk", fun _ ->
          post "/publishedstatement/qs1/st1" "initial content" |> ignore
          post "/publishedstatement/qs1/st1" "updated content" |> ignore
          test <@ File.ReadAllText "/data/publishedstatements/qs1/st1/Statement.html" = "updated content" @>
      ]
    ]
  ]

runWithPrinter tests

