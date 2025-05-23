import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import '@styles/globals.css'
import { AppRouter } from './routes'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AppRouter />
  </StrictMode>,
)
