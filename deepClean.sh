#!/bin/sh

dotnet clean

find . -iname "bin" -o -iname "obj" | xargs rm -rf