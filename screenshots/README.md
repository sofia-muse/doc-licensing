# Screenshots — capture checklist

The main README references four PNGs in this folder. Capture them locally with the dev
servers running, drop the files in here, and the README will render them on GitHub.

## Setup

In one terminal:

```bash
dotnet run --project src/DocLicensing.Api
```

In another:

```bash
cd src/web && npm run dev
```

Open the **frontend** at <http://localhost:5173> and the **API Swagger UI** at
<http://localhost:5080/swagger>.

**Recommended viewport**: 1280 × 800 (Chrome DevTools → device toolbar → Responsive). Larger
windows produce a lot of empty space; smaller drops to a single column.

## Captures

| File | What it shows | How to set it up |
|---|---|---|
| `catalog-en.png` | The catalog page in English with several items and the search bar visible | Sign in (`/login`, any username), language = English, no search query. Capture the whole `.app` container. |
| `catalog-he.png` | Same view in Hebrew so the full layout flips RTL | Switch the language dropdown to **עברית**. Confirm the nav, cards, and header sit on the right. |
| `checkout-success.png` | The success state of `CheckoutDialog` with the "Download PDF" button | Click **License** on any card → fill the form → submit. Capture the open dialog (works in EN or HE). |
| `swagger.png` | The API surface | <http://localhost:5080/swagger> — expand the `Catalog` controller so two endpoints are visible. |

## Conventions

- **Format**: PNG, no transparency.
- **Size**: target ≤ 400 KB per file; the GitHub README loads them inline. Use a tool like
  [`oxipng`](https://github.com/shssoichiro/oxipng) if you need to compress.
- **Filenames**: exactly as listed — the main README hardcodes them.
- **No personal data**: use a sample buyer name + email (e.g. `Sofia Demo`, `sofia@example.com`).
