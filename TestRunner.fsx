module TestRunner

#r "packages/Fuchu/lib/Fuchu.dll"

open Fuchu
open Fuchu.Test
open Fuchu.Impl

let printTestName name =
  printf "%s\n" name

let printer = 
  { TestPrinters.Default with 
      BeforeRun = printTestName
      Failed = printFailed
      Exception = printException }

let runWithPrinter tests = 
  printf "Running tests...\n"
  runEval (eval printer Seq.map) tests
