import { describe, it, expect, vi } from 'vitest'
import { render, screen, waitFor } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { SearchBar } from '../SearchBar'

describe('SearchBar', () => {
  it('debounces calls to onChange while the user is typing', async () => {
    const user = userEvent.setup()
    const onChange = vi.fn()

    render(<SearchBar value="" onChange={onChange} debounceMs={50} />)
    const input = screen.getByRole('searchbox')

    await user.type(input, 'iso')

    // Only the final, settled value should propagate.
    await waitFor(
      () => {
        expect(onChange).toHaveBeenLastCalledWith('iso')
      },
      { timeout: 500 },
    )
  })

  it('does not fire onChange when nothing changed', async () => {
    const onChange = vi.fn()
    render(<SearchBar value="initial" onChange={onChange} debounceMs={50} />)

    await new Promise((r) => setTimeout(r, 120))
    expect(onChange).not.toHaveBeenCalled()
  })
})
