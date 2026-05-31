import { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { fetchCatalog, type DocumentDto } from '../api'

export function CatalogList() {
  const { t, i18n } = useTranslation()
  const [docs, setDocs] = useState<DocumentDto[]>([])
  const [state, setState] = useState<'loading' | 'ready' | 'error'>('loading')

  useEffect(() => {
    let active = true
    setState('loading')
    fetchCatalog(i18n.language)
      .then((d) => {
        if (active) {
          setDocs(d)
          setState('ready')
        }
      })
      .catch(() => {
        if (active) setState('error')
      })
    return () => {
      active = false
    }
  }, [i18n.language])

  if (state === 'loading') return <p>{t('loading')}</p>
  if (state === 'error') return <p className="error">{t('error')}</p>

  return (
    <ul className="catalog">
      {docs.map((d) => (
        <li key={d.id} className="card">
          <div className="card-head">
            <span className="code">{d.code}</span>
            <span className="cat">
              {t('category')}: {d.category}
            </span>
          </div>
          <h3>{d.title}</h3>
          <p>{d.description}</p>
          <div className="card-foot">
            <span className="price">
              {t('price')}: {d.price.toFixed(2)} {d.currency}
            </span>
            <button type="button">{t('buy')}</button>
          </div>
        </li>
      ))}
    </ul>
  )
}
