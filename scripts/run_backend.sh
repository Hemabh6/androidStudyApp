#!/usr/bin/env bash
set -euo pipefail

PORT="${1:-5000}"

if ! command -v dotnet >/dev/null 2>&1; then
  echo "Error: dotnet SDK is not installed. Install .NET 8 SDK and retry." >&2
  exit 1
fi

cd "$(dirname "$0")/../Backend/StudySaaS.Api"
dotnet restore
dotnet run --urls "http://localhost:${PORT}"
