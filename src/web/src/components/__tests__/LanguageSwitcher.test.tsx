import { describe, it, expect, beforeEach } from 'vitest'
import { render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import i18n from '../../i18n'
import { LanguageSwitcher } from '../LanguageSwitcher'

describe('LanguageSwitcher', () => {
  beforeEach(async () => {
    await i18n.changeLanguage('en')
    document.documentElement.dir = 'ltr'
    document.documentElement.lang = 'en'
  })

  it('flips the document direction to RTL when Hebrew is selected', async () => {
    const user = userEvent.setup()
    render(<LanguageSwitcher />)

    expect(document.documentElement.dir).toBe('ltr')

    await user.selectOptions(screen.getByRole('combobox'), 'he')

    expect(document.documentElement.dir).toBe('rtl')
    expect(document.documentElement.lang).toBe('he')
  })

  it('switches back to LTR when English is selected', async () => {
    const user = userEvent.setup()
    document.documentElement.dir = 'rtl'
    render(<LanguageSwitcher />)

    await user.selectOptions(screen.getByRole('combobox'), 'en')

    expect(document.documentElement.dir).toBe('ltr')
    expect(document.documentElement.lang).toBe('en')
  })
})
