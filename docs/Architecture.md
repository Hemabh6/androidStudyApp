# In-Depth SaaS Architecture (Multi-Tenant Study Platform)

## 1) Product Scope

Platform users:

- Institute Admin
- Teacher
- Student

Core capability:

- Multi-tenant courses, materials, tests, analytics.

## 2) Tenant Isolation Strategy

### Shared Database (initial recommended)

All records include `InstituteId`.

Pros:

- Fast launch
- Lower cost
- Easier reporting

Controls required:

- Mandatory tenant resolution middleware
- Global tenant query filters in EF Core
- Tenant-aware caching keys (`tenant:{id}:courses`)

### Optional future tier

- Enterprise plan can use dedicated DB per institute.

## 3) Logical Architecture

```text
Android App
   |
API Gateway / WAF
   |
ASP.NET Core APIs (Auth, Courses, Tests, Materials, Analytics)
   |
PostgreSQL + Redis + Blob Storage + Queue Worker
```

## 4) Authentication and Authorization

- Login endpoint requires `X-Tenant` (or subdomain parsing in gateway).
- JWT claims:
  - `sub` (user id)
  - `instituteId`
  - `role`
  - `email`
- Role policies:
  - InstituteAdmin: full institute scope
  - Teacher: content + test creation
  - Student: content consumption + test submission

## 5) Domain Model

Entities:

- Institute
- User
- Course
- Enrollment
- StudyMaterial
- Test
- Question
- Submission
- Subscription

Key rules:

- All entities (except plan metadata) are tenant scoped.
- Teachers can only mutate resources in their institute.

## 6) API Design

Recommended module split:

- `/api/auth/*`
- `/api/institutes/*`
- `/api/courses/*`
- `/api/materials/*`
- `/api/tests/*`
- `/api/analytics/*`
- `/api/subscriptions/*`

Standards:

- RFC7807 problem details
- API versioning
- Correlation IDs

## 7) Data and Performance

- PostgreSQL indexes:
  - `(InstituteId, Email)` unique for users
  - `(InstituteId, CourseId)` for materials/tests
- Redis:
  - hot course list
  - test metadata
- CDN for static assets and videos
- Async processing for heavy tasks:
  - report generation
  - notification fan-out

## 8) Security Hardening

- Password hashing with ASP.NET Identity/Argon2/BCrypt
- Refresh token rotation
- Rate limiting and login lockout
- Audit trails for admin/teacher actions
- Signed URLs for private study content

## 9) Observability

- Structured logs with Serilog
- OpenTelemetry traces + metrics
- Dashboards for API latency and tenant errors

## 10) Deployment Blueprint

### Azure

- App Service / Container Apps for API
- Azure Database for PostgreSQL
- Azure Cache for Redis
- Blob Storage + CDN
- Key Vault for secrets

### AWS

- ECS Fargate / EKS
- RDS PostgreSQL
- Elasticache Redis
- S3 + CloudFront
- Secrets Manager

## 11) CI/CD

- PR checks: format, unit tests, API tests
- Build and container publish
- Migrations gated per environment
- Blue/green or canary deployments

## 12) Mobile App Strategy

Screens in this starter:

- Login
- Courses
- Tests preview

Planned next:

- Materials viewer
- Attempt test and instant results
- Offline downloads with encrypted local storage
- Push notifications
