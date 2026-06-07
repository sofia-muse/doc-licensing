import type { CheckoutResult, DocumentDto } from './api'

export interface PurchaseRecord {
  documentId: number
  code: string
  title: string
  purchaseId: number
  paymentReference: string
  amountPaid: number
  currency: string
  downloadUrl: string
  buyerName: string
  createdAt: string
}

const KEY = 'doc-licensing.purchases'

export function loadPurchases(): PurchaseRecord[] {
  try {
    const raw = localStorage.getItem(KEY)
    return raw ? (JSON.parse(raw) as PurchaseRecord[]) : []
  } catch {
    return []
  }
}

export function savePurchase(
  doc: DocumentDto,
  result: CheckoutResult,
  buyerName: string,
): PurchaseRecord {
  const record: PurchaseRecord = {
    documentId: doc.id,
    code: doc.code,
    title: doc.title,
    purchaseId: result.purchaseId,
    paymentReference: result.paymentReference,
    amountPaid: result.amountPaid,
    currency: result.currency,
    downloadUrl: result.downloadUrl,
    buyerName,
    createdAt: new Date().toISOString(),
  }
  const existing = loadPurchases()
  localStorage.setItem(KEY, JSON.stringify([record, ...existing]))
  return record
}
