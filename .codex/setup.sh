#!/bin/bash
set -euo pipefail

# Ensure dotnet is available
if ! command -v dotnet sdk check >/dev/null; then
  apt update && apt install -y --no-install-recommended dotnet-sdk-8.0 # Replace by 9.0 when Codex image updated
fi

export PATH="$PATH:$HOME/.dotnet/tools"

# Ensure csharpier is available
if ! command -v csharpier >/dev/null; then
  dotnet tool install --tool-path "$HOME/.dotnet/tools" csharpier || true
fi

# Attempt to restore local tools if possible
dotnet tool restore || true
