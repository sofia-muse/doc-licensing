import { useCallback, useEffect, useState } from 'react'
import { useSearchParams } from 'react-router-dom'
import { useTranslation } from 'react-i18next'
import { searchCatalog, type DocumentDto, type PagedResult } from '../api'
import { CatalogCard } from '../components/CatalogCard'
import { SearchBar } from '../components/SearchBar'
import { CheckoutDialog } from '../components/CheckoutDialog'

const PAGE_SIZE = 6

export function CatalogPage() {
  const { t, i18n } = useTranslation()
  const [searchParams, setSearchParams] = useSearchParams()
  const q = searchParams.get('q') ?? ''
  const page = Math.max(1, Number(searchParams.get('page') ?? '1'))

  const [result, setResult] = useState<PagedResult<DocumentDto> | null>(null)
  const [state, setState] = useState<'loading' | 'ready' | 'error'>('loading')
  const [selected, setSelected] = useState<DocumentDto | null>(null)

  const updateQ = useCallback(
    (next: string) => {
      const sp = new URLSearchParams(searchParams)
      if (next) sp.set('q', next)
      else sp.delete('q')
      sp.set('page', '1')
      setSearchParams(sp, { replace: true })
    },
    [searchParams, setSearchParams],
  )

  const updatePage = useCallback(
    (next: number) => {
      const sp = new URLSearchParams(searchParams)
      sp.set('page', String(next))
      setSearchParams(sp, { replace: true })
    },
    [searchParams, setSearchParams],
  )

  useEffect(() => {
    let active = true
    setState('loading')
    searchCatalog({ q, lang: i18n.language, page, pageSize: PAGE_SIZE })
      .then((r) => {
        if (active) {
          setResult(r)
          setState('ready')
        }
      })
      .catch(() => {
        if (active) setState('error')
      })
    return () => {
      active = false
    }
  }, [q, page, i18n.language])

  const totalPages = result ? Math.max(1, Math.ceil(result.total / result.pageSize)) : 1

  return (
    <>
      <SearchBar value={q} onChange={updateQ} />

      {state === 'loading' && <p>{t('loading')}</p>}
      {state === 'error' && <p className="error">{t('error')}</p>}

      {state === 'ready' && result && (
        result.items.length === 0 ? (
          <p className="muted">{q ? t('search.empty', { q }) : t('catalog.empty')}</p>
        ) : (
          <>
            <p className="muted">{t('search.total', { total: result.total })}</p>
            <ul className="catalog">
              {result.items.map((d) => (
                <CatalogCard key={d.id} doc={d} onLicense={setSelected} />
              ))}
            </ul>
            {totalPages > 1 && (
              <div className="pager">
                <button type="button" disabled={page <= 1} onClick={() => updatePage(page - 1)}>
                  {t('pager.prev')}
                </button>
                <span>{t('pager.position', { page, total: totalPages })}</span>
                <button type="button" disabled={page >= totalPages} onClick={() => updatePage(page + 1)}>
                  {t('pager.next')}
                </button>
              </div>
            )}
          </>
        )
      )}

      {selected && (
        <CheckoutDialog doc={selected} onClose={() => setSelected(null)} />
      )}
    </>
  )
}
