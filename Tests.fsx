#load "App.fsx"
#load "Config.fsx"
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
open Config

let statementDir = sprintf "%s/resource" rootDir

let setup () =
  Directory.CreateDirectory statementDir |> ignore

let teardown () =
  Directory.Delete(statementDir, true)

let runTestWith setup teardown test () =
  setup ()
  let r = test ()
  teardown ()
  r

let writeStatement path content =
  let dir = (sprintf "%s/%s" statementDir path)
  let file = dir + "/Statement.html"
  dir |> Directory.CreateDirectory |> ignore
  File.WriteAllText(file, content)

let readStatement path =
  File.ReadAllText (sprintf "%s/%s/Statement.html" statementDir path)

let tests =
  testList "published statement api" [
    testList "POST a statement" [

      yield! testFixture (runTestWith setup teardown) [
        "should return CREATED 201", fun _ ->
          let res = post "/resource/qs1/st1" ""
          test <@ res.StatusCode = HttpStatusCode.Created @>

        "should create the statement on disk", fun _ ->
          post "/resource/qs1/st1" "content" |> ignore
          test <@ readStatement "qs1/st1" = "content" @>

        "should update the statement on disk", fun _ ->
          post "/resource/qs1/st1" "initial content" |> ignore
          post "/resource/qs1/st1" "updated content" |> ignore
          test <@ readStatement "qs1/st1" = "updated content" @>
      ]
    ]
    testList "GET a statement" [
      yield! testFixture (runTestWith setup teardown) [
        "should return statement when it exists", fun _ ->
          writeStatement "qs1/st1" "content"
          let res = get "/resource/qs1/st1/Statement.html"
          test <@ res = {StatusCode = HttpStatusCode.OK; Content = "content"} @>
        "should return not found if it doesnt exists", fun _ ->
          let res = get "/resource/doesnot/exist/Statement.html"
          test <@ res = {StatusCode = HttpStatusCode.NotFound; Content = "Found no handlers"} @>
      ]
    ]
  //  testList "DELETE all resources" [
  //    yield! testFixture (runTestWith setup teardown) [
  //      "should delete all statements", fun _ ->
  //        writeStatement "qs1/st1" "content"
  //        let res = delete "/resource"
  //        test <@ res = {StatusCode = HttpStatusCode.OK; Content = "Deleted"} @>
  //        let getRes = get "/resource/qs1/st1/Statement.html"
  //        test <@ getRes = {StatusCode = HttpStatusCode.NotFound; Content = "Found no handlers"} @>
  //    ]
  //  ]
  ]

runWithPrinter tests

