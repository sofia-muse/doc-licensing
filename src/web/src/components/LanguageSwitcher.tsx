import { useEffect } from 'react'
import { useTranslation } from 'react-i18next'
import { RTL_LANGS } from '../i18n'

function applyDirection(lng: string) {
  document.documentElement.lang = lng
  document.documentElement.dir = RTL_LANGS.includes(lng) ? 'rtl' : 'ltr'
}

export function LanguageSwitcher() {
  const { i18n, t } = useTranslation()

  // Keep <html lang|dir> in sync with the active language — covers the
  // initial mount (and any language change coming from elsewhere), not just
  // user clicks on the dropdown.
  useEffect(() => {
    applyDirection(i18n.language)
  }, [i18n.language])

  const change = (lng: string) => {
    void i18n.changeLanguage(lng)
    applyDirection(lng)
  }

  return (
    <label className="lang">
      {t('language')}:{' '}
      <select value={i18n.language} onChange={(e) => change(e.target.value)}>
        <option value="en">English</option>
        <option value="he">עברית</option>
      </select>
    </label>
  )
}
