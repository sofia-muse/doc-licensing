import { Routes, Route, Navigate } from 'react-router-dom'
import { Nav } from './components/Nav'
import { CatalogPage } from './pages/CatalogPage'
import { PurchasesPage } from './pages/PurchasesPage'
import { LoginPage } from './pages/LoginPage'

export default function App() {
  return (
    <div className="app">
      <Nav />
      <main>
        <Routes>
          <Route path="/" element={<CatalogPage />} />
          <Route path="/purchases" element={<PurchasesPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </main>
    </div>
  )
}
