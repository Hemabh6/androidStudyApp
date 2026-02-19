#!/usr/bin/env bash
set -euo pipefail

OUTPUT="StudySaaS-FullStack-Starter.zip"

rm -f "$OUTPUT"
zip -r "$OUTPUT" Backend AndroidApp docs README.md
zip -T "$OUTPUT"
echo "Created $OUTPUT"
