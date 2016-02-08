#!/bin/sh
mono .paket/paket.bootstrapper.exe
mono .paket/paket.exe restore

fsharpi tests/Tests.fsx
