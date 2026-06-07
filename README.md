# DocLicensing

A small, production-shaped **document-licensing** application: browse a bilingual (Hebrew/English)
catalog of standards documents, license one through a card payment, and receive a **watermarked,
single-licence PDF** plus an email receipt.

It's a generic, publishable analog of the kind of public-sector e-commerce work I do day-to-day —
catalog → search → payment → document delivery — built to show how I structure a real .NET + React
codebase rather than to be a finished product.

**Stack:** ASP.NET Core Web API (.NET 8) · Entity Framework Core · SQL Server · React + TypeScript
(Vite) · react-i18next with full RTL support · xUnit · Docker · GitHub Actions.

---

## Architecture

A layered backend behind a thin API, with the integration points (payment, PDF, email) hidden
behind interfaces so they can be swapped or faked.

```mermaid
flowchart LR
    subgraph Web [React + Vite + TS]
        UI[Catalog UI\nEN/HE - LTR/RTL]
    end
    subgraph Api [DocLicensing.Api]
        C[Controllers\nCatalog / Checkout / Documents / Auth]
    end
    subgraph Core [DocLicensing.Core]
        S[Services\nCatalogService / CheckoutService]
        I[Interfaces\nIPaymentService / IPdfService / IEmailService / IDocumentRepository]
    end
    subgraph Infra [DocLicensing.Infrastructure]
        R[EF Core + SQL Server]
        P[Stripe / Fake payment]
        D[QuestPDF watermark]
        M[SendGrid / Log email]
    end

    UI -->|HTTP/JSON| C
    C --> S
    S --> I
    I -.implemented by.-> R
    I -.implemented by.-> P
    I -.implemented by.-> D
    I -.implemented by.-> M
```

**Projects**

| Project | Responsibility |
|---|---|
| `DocLicensing.Core` | Domain models, DTOs, service interfaces, business logic. No framework dependencies. |
| `DocLicensing.Infrastructure` | EF Core `DbContext` + repository, and the payment/PDF/email implementations. |
| `DocLicensing.Api` | Controllers, DI wiring, Swagger, JWT auth, CORS. |
| `DocLicensing.Tests` | xUnit tests for the checkout flow using in-memory fakes. |
| `src/web` | React + TypeScript frontend with i18n and RTL. |

---

## Running it

### Backend (API)

Requires the .NET 8 SDK (or newer) and either SQL Server LocalDB (default) or the Docker setup below.

```bash
dotnet restore
dotnet run --project src/DocLicensing.Api
```

Then open **http://localhost:5080/swagger**. In Development the database is created from the EF
migrations and seeded on startup, so the catalog works immediately.

Try the flow in Swagger:

1. `GET /api/catalog?lang=he` — bilingual catalog.
2. `GET /api/catalog/search?q=security&lang=he&page=1&pageSize=5` — paged search across code,
   titles (EN+HE), descriptions and category.
3. `POST /api/auth/token` with `{ "username": "sofia" }` — copy the `access_token`, click
   **Authorize** in Swagger, paste `Bearer <token>`.
4. `POST /api/checkout` with `{ "documentId": 1, "buyerName": "Sofia", "buyerEmail": "s@example.com" }`.
5. Open the returned `downloadUrl` — `GET /api/documents/1/download?purchase=…` — to get the
   watermarked PDF.

### Migrations

EF Core migrations are checked in under `src/DocLicensing.Infrastructure/Migrations/`. `dotnet-ef`
is a local tool — restore it once with `dotnet tool restore`, then add new migrations with:

```bash
dotnet ef migrations add <Name> \
  --project src/DocLicensing.Infrastructure \
  --startup-project src/DocLicensing.Api
```

The dev startup applies pending migrations automatically.

### Frontend (React)

Requires Node 20+.

```bash
cd src/web
npm install
npm run dev
```

Open **http://localhost:5173**. The catalog loads from the API; the language switcher toggles
EN ↔ HE and flips the whole layout LTR ↔ RTL. (Set `VITE_API_URL` via `.env` if the API isn't on
`localhost:5080` — see `.env.example`.)

### Everything via Docker

```bash
docker compose up --build
```

Brings up SQL Server + the API (seeded) on http://localhost:5080.

### Tests

```bash
dotnet test
```

---

## Design decisions

These are the choices I'd defend in a review — no metrics I can't measure, just the reasoning.

- **Layered with interface seams (Core / Infrastructure / Api).** `Core` holds the business logic
  and depends on nothing. The payment, PDF, and email providers sit behind interfaces, so the API
  doesn't know whether payments go to Stripe or a fake. This is what makes `CheckoutService`
  testable without a database or a real payment provider.
- **Provider strategy via configuration.** `Payments:Provider` / `Email:Provider` select the real
  vs. fake implementation in `AddInfrastructure`. The defaults (`Fake`, `Log`) let the project run
  end-to-end with zero secrets; flip them once Stripe/SendGrid keys are in user-secrets.
- **Localisation in the data model, not duplicated rows.** A `Document` carries both `TitleEn` and
  `TitleHe`; `CatalogService` projects to a single localised `DocumentDto` based on the requested
  language. The frontend mirrors this with one resource bundle per language and an RTL flip driven
  by the document `dir` attribute.
- **EnsureCreated for the demo, migrations for real.** Startup seeding keeps first-run friction at
  zero. Moving to EF migrations is the first roadmap item.

---

## One-week build roadmap

This repository is the **starting skeleton**. The plan to take it to "portfolio-ready":

- [x] **Day 1** — Solution skeleton: layered API, EF Core + seed, catalog endpoint, React catalog
      with EN/HE + RTL, Swagger, tests, Docker, CI.
- [x] **Day 2** — EF Core migrations replace `EnsureCreated`; `GET /api/catalog/search` with
      `q` / `category` filtering and paging via `EF.Functions.Like` across code, titles (EN+HE)
      and descriptions; catalog expanded to 12 bilingual documents across 12 categories;
      `dotnet-ef` registered as a local tool; search tests added.
- [x] **Day 5** — Frontend: routes via `react-router-dom`, JWT-backed `AuthProvider` + demo
      login flow, debounced `SearchBar` driving `GET /api/catalog/search` with paging, modal
      `CheckoutDialog` with form validation and success state, "My Purchases" page backed by
      `localStorage`, and Vitest + Testing Library tests for the RTL flip, search debounce, and
      checkout sign-in gating.
- [ ] **Day 3** — Implement `StripePaymentService` (test mode) and `SendGridEmailService` for real;
      handle the Stripe webhook to confirm payment.
- [ ] **Day 4** — Replace demo JWT with ASP.NET Core Identity (or Azure AD); a real "my purchases"
      page behind auth.
- [ ] **Day 6** _(deferred)_ — Deploy to Azure. First attempt was blocked by network restrictions
      (corporate DNS, Safe Browsing on the unique-hostname URL, no log visibility). Will retry from
      an unrestricted network with the same target architecture (App Service + Azure SQL + Static
      Web Apps).
- [ ] **Day 7** — Polish: architecture notes, screenshots/GIF in this README, accessibility pass on
      the RTL layout.

---

## Notes

- **No secrets are committed.** Stripe/SendGrid keys are read from configuration / user-secrets /
  environment variables. The JWT signing key in `appsettings.json` is a development placeholder —
  replace it for any real deployment.
- The `Stripe`/`SendGrid` classes are intentionally **stubs** marked with `TODO` — the seams are in
  place; Day 3 fills them in.
