module TestUtils
#load "App.fsx"
#load "Config.fsx"

#r "packages/Suave/lib/net40/Suave.dll"
#r "packages/Suave.Testing/lib/net40/Suave.Testing.dll"
#r "packages/Microsoft.Net.Http/lib/net40/System.Net.Http.dll"

open App
open Config
open Suave
open Suave.Testing
open System.Net
open System.Net.Http

type Response = {
  StatusCode : HttpStatusCode
  Content : string
}

let runServer () =
  runWith config app

let respParse (response : HttpResponseMessage) =
  {StatusCode = response.StatusCode
   Content = response.Content.ReadAsStringAsync().Result}

let coreReq methd path data =
  reqResp methd path "" data None System.Net.DecompressionMethods.None id respParse

let post path content =
  use data = new StringContent(content)
  runServer() |> coreReq HttpMethod.POST path (Some data)

let get path =
  runServer() |> coreReq HttpMethod.GET path None
