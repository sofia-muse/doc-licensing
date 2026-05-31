import { useTranslation } from 'react-i18next'
import { LanguageSwitcher } from './components/LanguageSwitcher'
import { CatalogList } from './components/CatalogList'

export default function App() {
  const { t } = useTranslation()

  return (
    <div className="app">
      <header>
        <div>
          <h1>{t('title')}</h1>
          <p className="subtitle">{t('subtitle')}</p>
        </div>
        <LanguageSwitcher />
      </header>
      <main>
        <CatalogList />
      </main>
    </div>
  )
}
