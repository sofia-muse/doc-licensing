const BASE = import.meta.env.VITE_API_URL ?? 'http://localhost:5080'

export interface DocumentDto {
  id: number
  code: string
  title: string
  description: string
  price: number
  currency: string
  category: string
}

export interface PagedResult<T> {
  items: T[]
  total: number
  page: number
  pageSize: number
}

export interface CheckoutResult {
  purchaseId: number
  paymentReference: string
  amountPaid: number
  currency: string
  downloadUrl: string
}

export async function fetchCatalog(lang: string): Promise<DocumentDto[]> {
  const res = await fetch(`${BASE}/api/catalog?lang=${encodeURIComponent(lang)}`)
  if (!res.ok) throw new Error(`API ${res.status}`)
  return res.json()
}

export async function searchCatalog(params: {
  q?: string
  category?: string
  lang: string
  page: number
  pageSize: number
}): Promise<PagedResult<DocumentDto>> {
  const sp = new URLSearchParams()
  if (params.q) sp.set('q', params.q)
  if (params.category) sp.set('category', params.category)
  sp.set('lang', params.lang)
  sp.set('page', String(params.page))
  sp.set('pageSize', String(params.pageSize))

  const res = await fetch(`${BASE}/api/catalog/search?${sp}`)
  if (!res.ok) throw new Error(`API ${res.status}`)
  return res.json()
}

export async function login(username: string): Promise<string> {
  const res = await fetch(`${BASE}/api/auth/token`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username }),
  })
  if (!res.ok) throw new Error(`API ${res.status}`)
  const data = (await res.json()) as { access_token: string }
  return data.access_token
}

export async function checkout(
  token: string,
  body: { documentId: number; buyerName: string; buyerEmail: string },
): Promise<CheckoutResult> {
  const res = await fetch(`${BASE}/api/checkout`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(body),
  })
  if (!res.ok) {
    const text = await res.text().catch(() => '')
    throw new Error(`API ${res.status}${text ? `: ${text}` : ''}`)
  }
  return res.json()
}

/** Turn the relative downloadUrl returned by checkout into an absolute URL. */
export function downloadUrl(relative: string): string {
  return `${BASE}${relative}`
}
