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

try
  Directory.CreateDirectory "/data/statements" |> ignore
with _ -> ()

let tests =
  testList "resource api tests:" [
    testList "POST a statement" [
      testCase "should return CREATED 201" <| fun _ ->
        let res = post "/statement/qs1/st1" ""
        test <@ res.StatusCode = HttpStatusCode.Created @>

      testCase "it should create the statement on disk" <| fun _ ->
        post "/statement/qs1/st1" "content" |> ignore
        test <@ File.ReadAllText "/data/statements/qs1/st1/Statement.html" = "content" @>

      testCase "it should update the statement on disk" <| fun _ ->
        post "/statement/qs1/st1" "initial content" |> ignore
        post "/statement/qs1/st1" "updated content" |> ignore
        test <@ File.ReadAllText "/data/statements/qs1/st1/Statement.html" = "updated content" @>

    ]
  ]

runWithPrinter tests

