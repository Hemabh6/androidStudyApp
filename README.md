# StudySaaS Multi-Tenant Starter (Backend + Android)

This repository contains a **full-stack starter** for a multi-tenant SaaS study app:

- `Backend/StudySaaS.Api`: ASP.NET Core 8 Web API starter with JWT auth, tenant resolution, course/material/test APIs.
- `AndroidApp`: Native Android (Kotlin) app with Login, Course List, and Tests screens consuming backend APIs.
- `docs/Architecture.md`: in-depth SaaS architecture, scaling, data, and deployment blueprint.

## Multi-Tenant Model

- Shared database strategy.
- Tenant is resolved using `X-Tenant` header (maps to institute subdomain/code).
- JWT includes `instituteId`, role, and user identity.
- Every query is filtered by `InstituteId`.

## Backend API Quick Notes

Base URL: `http://localhost:5000/`

Key endpoints:

- `POST /api/auth/register-institute`
- `POST /api/auth/login`
- `GET /api/courses`
- `POST /api/courses`
- `GET /api/courses/{courseId}/materials`
- `GET /api/tests/course/{courseId}`
- `POST /api/tests/submit`

Use header:

```
X-Tenant: demo
Authorization: Bearer <jwt>
```

Demo login:

- `student@demo.com / Pass@123`
- `teacher@demo.com / Pass@123`

## Android App

Update API endpoint in:

- `AndroidApp/app/build.gradle.kts` via `API_BASE_URL` and `TENANT_CODE` build config fields.

For Android Emulator, use:

- `http://10.0.2.2:5000/`

## Quick Run (Step-by-Step)

### Prerequisites

- .NET 8 SDK (for backend)
- Android Studio + Android SDK (for app)
- Java 17+

### 1) Start backend

```bash
./scripts/run_backend.sh
```

Backend starts at `http://localhost:5000`.

### 2) Smoke-test backend APIs

In another terminal:

```bash
./scripts/smoke_test_backend.sh
```

This performs login using demo credentials and fetches courses.

### 3) Run Android app

- Open `AndroidApp/` in Android Studio.
- Let Gradle sync.
- Start emulator and run app.

> API URL is already configured for emulator loopback (`http://10.0.2.2:5000/`).

## ZIP Delivery

To generate a release ZIP locally (instead of committing binary artifacts):

- `./scripts_create_release_zip.sh`

This creates:

- `StudySaaS-FullStack-Starter.zip`

## Next production upgrades

- EF Core + PostgreSQL and tenant global query filters.
- Refresh token flow and password hashing.
- Payments/subscriptions with Razorpay/Stripe.
- Redis caching and background jobs.
- Blob storage + CDN for videos/PDFs.
- Push notifications, analytics dashboards, and offline sync.

## PR Creation Troubleshooting

If you cannot open a GitHub PR directly from this environment, use this flow:

1. Commit changes locally.
2. Push your branch to your remote repo.
3. Open a PR from that branch in GitHub.

If you're using an automated agent environment, the PR title/body can also be generated via an integrated PR tool and then pasted into GitHub.
