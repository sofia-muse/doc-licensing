import { useState, type FormEvent } from 'react'
import { useNavigate } from 'react-router-dom'
import { useTranslation } from 'react-i18next'
import { useAuth } from '../auth'

export function LoginPage() {
  const { t } = useTranslation()
  const { signIn } = useAuth()
  const navigate = useNavigate()

  const [username, setUsername] = useState('')
  const [submitting, setSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    setError(null)
    setSubmitting(true)
    try {
      await signIn(username)
      navigate('/')
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Login failed')
    } finally {
      setSubmitting(false)
    }
  }

  return (
    <form className="login" onSubmit={handleSubmit}>
      <h2>{t('login.title')}</h2>
      <p className="muted">{t('login.demoNote')}</p>
      <label>
        {t('login.username')}
        <input
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
          minLength={1}
          autoComplete="username"
        />
      </label>
      {error && <p className="error">{error}</p>}
      <button type="submit" disabled={submitting}>
        {submitting ? t('login.submitting') : t('login.submit')}
      </button>
    </form>
  )
}
