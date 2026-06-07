import { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { downloadUrl } from '../api'
import { loadPurchases, type PurchaseRecord } from '../purchases'

export function PurchasesPage() {
  const { t } = useTranslation()
  const [items, setItems] = useState<PurchaseRecord[]>([])

  useEffect(() => {
    setItems(loadPurchases())
  }, [])

  if (items.length === 0) {
    return <p className="muted">{t('purchases.empty')}</p>
  }

  return (
    <ul className="purchases">
      {items.map((p) => (
        <li key={p.purchaseId} className="card">
          <div className="card-head">
            <span className="code">{p.code}</span>
            <span className="muted">{new Date(p.createdAt).toLocaleDateString()}</span>
          </div>
          <h3>{p.title}</h3>
          <p className="muted">{t('purchases.licensedTo')}: {p.buyerName}</p>
          <p className="muted">{t('purchases.ref')}: {p.paymentReference}</p>
          <div className="card-foot">
            <span className="price">{p.amountPaid.toFixed(2)} {p.currency}</span>
            <a
              className="download"
              href={downloadUrl(p.downloadUrl)}
              target="_blank"
              rel="noreferrer"
            >
              {t('purchases.download')}
            </a>
          </div>
        </li>
      ))}
    </ul>
  )
}
