import { ThemeProvider } from 'next-themes'
import React from 'react'
import Header from './Header'
import Footer from './Footer'
import { Outlet } from 'react-router';

const RootLayout: React.FC = () => {
  return (
    <ThemeProvider
      attribute="class"
      defaultTheme="system"
      enableSystem
      disableTransitionOnChange
    >
      <div className="flex min-h-screen flex-col">
        <Header />
        <main className="flex-1 justify-center">
          <Outlet />
        </main>
        <Footer />
      </div>
    </ThemeProvider>
  );
};

export default RootLayout;