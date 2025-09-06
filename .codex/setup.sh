#!/bin/bash
set -euo pipefail

export DOTNET_ROOT="$HOME/.dotnet"
export PATH="$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools"

# Install the .NET SDK if missing
if ! command -v dotnet >/dev/null; then
  curl -sSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
  bash /tmp/dotnet-install.sh --channel 9.0
  rm /tmp/dotnet-install.sh
fi

"$(dirname "$0")/maintenance.sh"

