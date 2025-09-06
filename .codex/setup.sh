#!/bin/bash
set -euo pipefail

# Ensure dotnet is available
if ! command -v dotnet >/dev/null; then
  apt update && apt install -y --no-install-recommends dotnet-sdk-9.0
fi

export PATH="$PATH:$HOME/.dotnet/tools"

# Restore local tools (including csharpier)
dotnet tool restore || true

# Restore the project dependencies
dotnet restore || true

# Prepare Resonite libraries
resonite_dir="$(pwd)/Resonite"
mkdir -p "$resonite_dir/Libraries" "$resonite_dir/rml_libs"
if [ ! -f "$resonite_dir/Libraries/ResoniteModLoader.dll" ]; then
  curl -L -o "$resonite_dir/Libraries/ResoniteModLoader.dll" \
    https://github.com/resonite-modding-group/ResoniteModLoader/releases/download/4.0.0/ResoniteModLoader.dll
fi
if [ ! -f "$resonite_dir/rml_libs/0Harmony.dll" ]; then
  curl -L -o "$resonite_dir/rml_libs/0Harmony.dll" \
    https://github.com/resonite-modding-group/ResoniteModLoader/releases/download/4.0.0/0Harmony.dll
fi
gamelibs_path="$HOME/.nuget/packages/resonite.gamelibs/2025.9.2.430/ref/net9.0"
if [ -d "$gamelibs_path" ]; then
  cp -n "$gamelibs_path"/*.dll "$resonite_dir/"
fi
