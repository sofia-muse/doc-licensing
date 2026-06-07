import { describe, it, expect, vi } from 'vitest'
import { render, screen } from '@testing-library/react'
import { AuthProvider } from '../../auth'
import { CheckoutDialog } from '../CheckoutDialog'

const sampleDoc = {
  id: 1,
  code: 'ISO-9001',
  title: 'Quality Management',
  description: 'Sample',
  price: 129,
  currency: 'ILS',
  category: 'Management',
}

describe('CheckoutDialog', () => {
  it('shows the sign-in prompt when the user has no token', () => {
    window.localStorage.clear()
    render(
      <AuthProvider>
        <CheckoutDialog doc={sampleDoc} onClose={vi.fn()} />
      </AuthProvider>,
    )
    expect(screen.getByText(/sign in first/i)).toBeInTheDocument()
    expect(screen.queryByRole('button', { name: /confirm/i })).not.toBeInTheDocument()
  })

  it('shows the buyer form when the user is authenticated', () => {
    window.localStorage.setItem('doc-licensing.token', 'fake-jwt')
    window.localStorage.setItem('doc-licensing.username', 'sofia')

    render(
      <AuthProvider>
        <CheckoutDialog doc={sampleDoc} onClose={vi.fn()} />
      </AuthProvider>,
    )

    expect(screen.getByLabelText(/your name/i)).toBeInTheDocument()
    expect(screen.getByLabelText(/email/i)).toBeInTheDocument()
    expect(screen.getByRole('button', { name: /confirm/i })).toBeInTheDocument()
  })
})
