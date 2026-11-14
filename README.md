# Teambase-Assessment

Small .NET API for the Teambase insurance premium task (SQLite + EF Core).

**Repo:** https://github.com/DanishBuilds/Teambase-Assessment

## Run

```bash
dotnet run --project Teambase.Assessment.Api
```

Swagger (dev): https://localhost:7147/swagger  
HTTP: http://localhost:5043

`dotnet test` runs calculator tests. Sample requests live under `postman/`.

## Auth (dev)

I added JWT so protected endpoints are easy to try in Swagger/Postman.

| | |
|---|---|
| Login | `POST /api/auth/login` |
| User | `test-user` |
| Pass | `Test@123` |

Header: `Authorization: Bearer <token>`

## Endpoints

| Task | Method | Route |
|------|--------|-------|
| SaveEmployeeData | POST | `/api/employees` |
| GetPremium | GET | `/api/premium` |

Query params for premium: `employeeId`, `pricingModel` (0/1/2), `prorataMethod` (0/1), `policyStartDate`, `policyEndDate`.

Import `postman/Teambase-Assessment.postman_collection.json` and the matching environment file for example bodies.

## Assumptions

- Age is counted as of `memberAdditionDate` (full years).
- Remaining **days** = inclusive from addition date through policy end (Mar 1–Dec 31 → 306 days).
- Remaining **months** = inclusive from addition month through end month (Mar–Dec → 10).
- Female gender-age rate uses 1.5× from age 18 upward.
- Prorated amount rounded to 2 decimals (`AwayFromZero`).