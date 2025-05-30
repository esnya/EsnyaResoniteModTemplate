#!/bin/bash
set -euo pipefail

export PATH="$PATH:$HOME/.dotnet/tools"

# Ensure csharpier is available
if ! command -v csharpier >/dev/null; then
  dotnet tool install --tool-path "$HOME/.dotnet/tools" csharpier || true
fi

# Attempt to restore local tools if possible
dotnet tool restore || true
