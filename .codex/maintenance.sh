#!/bin/bash
set -euo pipefail

export DOTNET_ROOT="$HOME/.dotnet"
export PATH="$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools"

# Update .NET SDK to latest 9.0 patch
curl -sSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
bash /tmp/dotnet-install.sh --channel 9.0
rm /tmp/dotnet-install.sh

# Restore local tools and project dependencies
dotnet tool restore || true
dotnet restore || true

# Prepare Resonite libraries
resonite_dir="$(pwd)/Resonite"
mkdir -p "$resonite_dir/Libraries" "$resonite_dir/rml_libs"

curl -L -z "$resonite_dir/Libraries/ResoniteModLoader.dll" \
  -o "$resonite_dir/Libraries/ResoniteModLoader.dll" \
  https://github.com/resonite-modding-group/ResoniteModLoader/releases/download/4.0.0/ResoniteModLoader.dll

curl -L -z "$resonite_dir/rml_libs/0Harmony.dll" \
  -o "$resonite_dir/rml_libs/0Harmony.dll" \
  https://github.com/resonite-modding-group/ResoniteModLoader/releases/download/4.0.0/0Harmony.dll

# Update Resonite.GameLibs package to latest version
latest_version=$(curl -s https://api.nuget.org/v3-flatcontainer/resonite.gamelibs/index.json | jq -r '.versions[-1]')
current_version=$(grep -oPm1 '(?<=<PackageVersion Include="Resonite.GameLibs" Version=")[^"]+' Directory.Packages.props || true)

if [ "$latest_version" != "$current_version" ]; then
  sed -i -E "s/(<PackageVersion Include=\"Resonite.GameLibs\" Version=\")[^"]+(\" \/>)/\1${latest_version}\2/" Directory.Packages.props
  dotnet restore || true
fi

# Re-read the actual current version after possible update and restore
current_version=$(grep -oPm1 '(?<=<PackageVersion Include="Resonite.GameLibs" Version=")[^"]+' Directory.Packages.props || true)
gamelibs_path="$HOME/.nuget/packages/resonite.gamelibs/${current_version}/ref/net9.0"
if [ -d "$gamelibs_path" ]; then
  cp -f "$gamelibs_path"/*.dll "$resonite_dir/"
fi

