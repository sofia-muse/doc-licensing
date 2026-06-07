import { createContext, useContext, useState, type ReactNode } from 'react'
import { login as apiLogin } from './api'

interface AuthState {
  token: string | null
  username: string | null
  signIn: (username: string) => Promise<void>
  signOut: () => void
}

const AuthContext = createContext<AuthState | null>(null)

const TOKEN_KEY = 'doc-licensing.token'
const USERNAME_KEY = 'doc-licensing.username'

export function AuthProvider({ children }: { children: ReactNode }) {
  const [token, setToken] = useState<string | null>(() => localStorage.getItem(TOKEN_KEY))
  const [username, setUsername] = useState<string | null>(() => localStorage.getItem(USERNAME_KEY))

  async function signIn(name: string) {
    const t = await apiLogin(name)
    localStorage.setItem(TOKEN_KEY, t)
    localStorage.setItem(USERNAME_KEY, name)
    setToken(t)
    setUsername(name)
  }

  function signOut() {
    localStorage.removeItem(TOKEN_KEY)
    localStorage.removeItem(USERNAME_KEY)
    setToken(null)
    setUsername(null)
  }

  return (
    <AuthContext.Provider value={{ token, username, signIn, signOut }}>
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth(): AuthState {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('useAuth must be used within AuthProvider')
  return ctx
}
