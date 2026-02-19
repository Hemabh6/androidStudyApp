#!/usr/bin/env bash
set -euo pipefail

BASE_URL="${1:-http://localhost:5000}"
TENANT="${2:-demo}"
EMAIL="${3:-student@demo.com}"
PASSWORD="${4:-Pass@123}"

if ! command -v curl >/dev/null 2>&1; then
  echo "Error: curl not found" >&2
  exit 1
fi

LOGIN_RESPONSE=$(curl -sS -X POST "${BASE_URL}/api/auth/login" \
  -H "Content-Type: application/json" \
  -H "X-Tenant: ${TENANT}" \
  -d "{\"email\":\"${EMAIL}\",\"password\":\"${PASSWORD}\"}")

if command -v python3 >/dev/null 2>&1; then
  TOKEN=$(python3 - <<'PY' "$LOGIN_RESPONSE"
import json,sys
try:
    print(json.loads(sys.argv[1]).get('token',''))
except Exception:
    print('')
PY
)
else
  TOKEN=$(printf '%s' "$LOGIN_RESPONSE" | sed -n 's/.*"token"[[:space:]]*:[[:space:]]*"\([^"]*\)".*/\1/p')
fi

if [ -z "$TOKEN" ]; then
  echo "Login failed. Response: $LOGIN_RESPONSE" >&2
  exit 1
fi

echo "Login OK"

echo "Fetching courses..."
COURSES=$(curl -sS "${BASE_URL}/api/courses" \
  -H "X-Tenant: ${TENANT}" \
  -H "Authorization: Bearer ${TOKEN}")

echo "$COURSES"

echo "Smoke test passed"
