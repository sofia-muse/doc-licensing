import { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'

interface Props {
  value: string
  onChange: (v: string) => void
  debounceMs?: number
}

/**
 * Free-text search input. Owns its local draft so typing is responsive,
 * and fires `onChange` only after the user stops typing for `debounceMs` ms.
 */
export function SearchBar({ value, onChange, debounceMs = 300 }: Props) {
  const { t } = useTranslation()
  const [draft, setDraft] = useState(value)

  // External value resets (e.g. clearing on navigation) propagate back to the input.
  useEffect(() => {
    setDraft(value)
  }, [value])

  // Debounce: only call onChange once the draft has settled.
  useEffect(() => {
    if (draft === value) return
    const id = setTimeout(() => onChange(draft), debounceMs)
    return () => clearTimeout(id)
  }, [draft, debounceMs, onChange, value])

  return (
    <input
      type="search"
      className="search-input"
      placeholder={t('search.placeholder')}
      value={draft}
      onChange={(e) => setDraft(e.target.value)}
      aria-label={t('search.placeholder')}
    />
  )
}
