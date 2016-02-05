#load "../src/App.fsx"

#r "../packages/Fuchu/lib/Fuchu.dll"
#r "../packages/Unquote/lib/net45/Unquote.dll"
#r "../packages/Suave.Testing/lib/net40/Suave.Testing.dll"

open Fuchu
open Swensen.Unquote
open Suave
open Suave.Testing
open App

let runServer () =
  runWith defaultConfig app
let get path testCtx = reqQuery HttpMethod.GET path "" testCtx

let tests =
  testList "resource api tests" [
    testCase "first test" <| fun _ ->
      let res =
        runServer ()
        |> get "/"

      test <@ res = "something here " @>
  ]

run tests
