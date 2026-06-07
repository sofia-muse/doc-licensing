import { useState, type FormEvent } from 'react'
import { useTranslation } from 'react-i18next'
import { checkout, downloadUrl, type CheckoutResult, type DocumentDto } from '../api'
import { useAuth } from '../auth'
import { savePurchase } from '../purchases'

interface Props {
  doc: DocumentDto
  onClose: () => void
}

export function CheckoutDialog({ doc, onClose }: Props) {
  const { t } = useTranslation()
  const { token, username } = useAuth()

  const [name, setName] = useState(username ?? '')
  const [email, setEmail] = useState('')
  const [submitting, setSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [result, setResult] = useState<CheckoutResult | null>(null)

  // If the user isn't authenticated, point them at the sign-in flow.
  if (!token) {
    return (
      <div className="dialog" role="dialog" aria-modal="true">
        <div className="dialog-body">
          <h2>{doc.code}</h2>
          <p>{t('checkout.signInFirst')}</p>
          <div className="dialog-actions">
            <button type="button" onClick={onClose}>{t('common.close')}</button>
          </div>
        </div>
      </div>
    )
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    setError(null)
    setSubmitting(true)
    try {
      const r = await checkout(token!, { documentId: doc.id, buyerName: name, buyerEmail: email })
      savePurchase(doc, r, name)
      setResult(r)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unknown error')
    } finally {
      setSubmitting(false)
    }
  }

  return (
    <div className="dialog" role="dialog" aria-modal="true">
      <div className="dialog-body">
        <h2>{doc.code} — {doc.title}</h2>
        <p className="muted">
          {t('checkout.price')}: {doc.price.toFixed(2)} {doc.currency}
        </p>

        {result ? (
          <div className="success">
            <p>{t('checkout.success')}</p>
            <a
              className="download"
              href={downloadUrl(result.downloadUrl)}
              target="_blank"
              rel="noreferrer"
            >
              {t('checkout.download')}
            </a>
            <div className="dialog-actions">
              <button type="button" onClick={onClose}>{t('common.close')}</button>
            </div>
          </div>
        ) : (
          <form onSubmit={handleSubmit}>
            <label>
              {t('checkout.buyerName')}
              <input
                value={name}
                onChange={(e) => setName(e.target.value)}
                required
                maxLength={120}
              />
            </label>
            <label>
              {t('checkout.buyerEmail')}
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
            </label>
            {error && <p className="error">{error}</p>}
            <div className="dialog-actions">
              <button type="button" onClick={onClose} disabled={submitting}>
                {t('common.cancel')}
              </button>
              <button type="submit" disabled={submitting}>
                {submitting ? t('checkout.processing') : t('checkout.confirm')}
              </button>
            </div>
          </form>
        )}
      </div>
    </div>
  )
}
