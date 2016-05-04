#!/bin/sh
mono .paket/paket.bootstrapper.exe
mono .paket/paket.exe restore

fsharpi --consolecolors+ Tests.fsx
