#!/bin/sh
mono .paket/paket.bootstrapper.exe
mono .paket/paket.exe update

fsharpi --consolecolors+ Tests.fsx
