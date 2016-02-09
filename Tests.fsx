#load "App.fsx"
#load "TestUtils.fsx"

#r "System.Core.dll"
#r "System.dll"
#r "packages/Fuchu/lib/Fuchu.dll"
#r "packages/Unquote/lib/net45/Unquote.dll"
#r "packages/Suave.Testing/lib/net40/Suave.Testing.dll"
#r "packages/Microsoft.Net.Http/lib/net40/System.Net.Http.dll"

open Fuchu
open Swensen.Unquote
open Suave
open Suave.Testing
open App
open TestUtils
open System
open System.Net
open System.IO
open System.Net.Http


try
  Directory.CreateDirectory "/data/statements" |> ignore
with _ -> ()

let tests =
  testList "resource api tests:" [
    testList "When POSTing a statement" [
      testCase "it should return CREATED 201" <| fun _ ->

        use data = new StringContent("")

        let res = 
          runServer ()
          |> post "/statement/qs1/st1" (Some data)

        test <@ res.StatusCode = HttpStatusCode.Created @>

      testCase "it should create it" <| fun _ ->

        use data = new StringContent("content")

        runServer ()
        |> post "/statement/qs1/st1" (Some data)
        |> ignore

        test <@ File.ReadAllText "/statements/qs1/st1/Statement.html" = "content" @>

    ]
  ]

run tests

