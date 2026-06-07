import { NavLink } from 'react-router-dom'
import { useTranslation } from 'react-i18next'
import { LanguageSwitcher } from './LanguageSwitcher'
import { useAuth } from '../auth'

export function Nav() {
  const { t } = useTranslation()
  const { username, signOut } = useAuth()

  return (
    <header>
      <div className="brand">
        <h1>{t('title')}</h1>
        <p className="subtitle">{t('subtitle')}</p>
      </div>
      <nav className="nav-links" aria-label="primary">
        <NavLink to="/" end>{t('nav.catalog')}</NavLink>
        <NavLink to="/purchases">{t('nav.purchases')}</NavLink>
        {username ? (
          <button type="button" onClick={signOut} className="nav-signout">
            {t('nav.signOut')} · {username}
          </button>
        ) : (
          <NavLink to="/login">{t('nav.signIn')}</NavLink>
        )}
      </nav>
      <LanguageSwitcher />
    </header>
  )
}
