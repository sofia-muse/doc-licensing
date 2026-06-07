import { useTranslation } from 'react-i18next'
import type { DocumentDto } from '../api'

interface Props {
  doc: DocumentDto
  onLicense: (doc: DocumentDto) => void
}

export function CatalogCard({ doc, onLicense }: Props) {
  const { t } = useTranslation()
  return (
    <li className="card">
      <div className="card-head">
        <span className="code">{doc.code}</span>
        <span className="cat">{t('category')}: {doc.category}</span>
      </div>
      <h3>{doc.title}</h3>
      <p>{doc.description}</p>
      <div className="card-foot">
        <span className="price">
          {t('price')}: {doc.price.toFixed(2)} {doc.currency}
        </span>
        <button type="button" onClick={() => onLicense(doc)}>{t('buy')}</button>
      </div>
    </li>
  )
}
