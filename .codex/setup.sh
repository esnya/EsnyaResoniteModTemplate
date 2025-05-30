#!/bin/bash
set -euo pipefail

apt update && apt install -y --no-install-recommends dotnet-sdk-8.0

# Install csharpier for formatting checks
export PATH="$PATH:$HOME/.dotnet/tools"

if ! dotnet tool list --tool-path "$HOME/.dotnet/tools" | grep -q csharpier; then
  dotnet tool install --tool-path "$HOME/.dotnet/tools" csharpier
else
  dotnet tool update --tool-path "$HOME/.dotnet/tools" csharpier
fi
