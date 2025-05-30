#!/bin/bash
set -euo pipefail

# Install csharpier for formatting checks
export PATH="$PATH:$HOME/.dotnet/tools"

dotnet tool install --tool-path "$HOME/.dotnet/tools" csharpier || \
  dotnet tool update --tool-path "$HOME/.dotnet/tools" csharpier
