#!/bin/bash
set -euo pipefail

export DOTNET_ROOT="$HOME/.dotnet"
export PATH="$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools"

# Ensure dotnet 9 SDK is installed
if ! command -v dotnet >/dev/null || ! dotnet --list-sdks 2>/dev/null | grep -q '^9\.'; then
  curl -sSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
  bash /tmp/dotnet-install.sh --channel 9.0
  rm /tmp/dotnet-install.sh
fi

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
