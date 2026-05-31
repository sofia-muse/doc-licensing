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

export async function fetchCatalog(lang: string): Promise<DocumentDto[]> {
  const res = await fetch(`${BASE}/api/catalog?lang=${encodeURIComponent(lang)}`)
  if (!res.ok) throw new Error(`API ${res.status}`)
  return res.json()
}
