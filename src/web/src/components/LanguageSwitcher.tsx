import { useTranslation } from 'react-i18next'
import { RTL_LANGS } from '../i18n'

export function LanguageSwitcher() {
  const { i18n, t } = useTranslation()

  const change = (lng: string) => {
    void i18n.changeLanguage(lng)
    document.documentElement.lang = lng
    document.documentElement.dir = RTL_LANGS.includes(lng) ? 'rtl' : 'ltr'
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
