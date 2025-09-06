#!/bin/bash
set -euo pipefail

props="$(git rev-parse --show-toplevel)/Directory.Packages.props"
latest=$(curl -s https://api.nuget.org/v3-flatcontainer/resonite.gamelibs/index.json | jq -r '.versions[-1]')
current=$(grep -oPm1 '(?<=<PackageVersion Include="Resonite.GameLibs" Version=")[^"]+' "$props" || true)

if [ "$latest" != "$current" ]; then
  sed -i -E "s/(<PackageVersion Include=\"Resonite.GameLibs\" Version=\")[^"]+(\" \/>)/\1${latest}\2/" "$props"
fi

