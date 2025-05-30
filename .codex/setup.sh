#!/bin/bash
set -euo pipefail

# Install csharpier for formatting checks
export PATH="$PATH:$HOME/.dotnet/tools"

if ! dotnet tool list --tool-path "$HOME/.dotnet/tools" | grep -q csharpier; then
  dotnet tool install --tool-path "$HOME/.dotnet/tools" csharpier
else
  dotnet tool update --tool-path "$HOME/.dotnet/tools" csharpier
fi
