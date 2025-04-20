import React, { useState } from 'react'
import { Link, useLocation } from 'react-router'

// ui shadcn
import { Button } from "@ui/button"
import { Input } from "@ui/input"
import { Badge } from "@ui/badge"
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuLabel, DropdownMenuSeparator, DropdownMenuTrigger } from "@ui/dropdown-menu"
import { Sheet, SheetContent, SheetTrigger } from "@ui/sheet"
import { Menu, Search, ShoppingCart, User } from 'lucide-react'
import ThemeToggle from '../theme-toggle'

const mainNav = [
  { name: "Inicio", to: "/" },
  { name: "Categorias", to: "/categories" },
  { name: "Novedades", to: "/new-arrivals" },
  { name: "Tienda", to: "/sale" },
]

const Header: React.FC = () => {
  const location = useLocation()
  const pathname = location.pathname
  const [isCartOpen, setIsCartOpen] = useState(false)
  const [isSearchOpen, setIsSearchOpen] = useState(false)

  return (
    <header className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60 dark:border-slate-700 dark:bg-slate-900/95 dark:supports-[backdrop-filter]:bg-slate-900/60 flex items-center justify-center">
      <div className="container flex h-16 items-center justify-between">
        <div className="flex items-center gap-6 md:gap-8 lg:gap-10">
          <Sheet>
            <SheetTrigger asChild>
              <Button variant="ghost" size="icon" className="md:hidden">
                <Menu className="h-5 w-5" />
                <span className="sr-only">Toggle menu</span>
              </Button>
            </SheetTrigger>
            <SheetContent side="left" className="w-[300px] sm:w-[400px]">
              <nav className="flex flex-col gap-4 mt-8">
                {mainNav.map((item) => (
                  <Link
                    key={item.name}
                    to={item.to}
                    className="text-lg font-medium transition-colors hover:text-primary"
                  >
                    {item.name}
                  </Link>
                ))}
              </nav>
            </SheetContent>
          </Sheet>

          <Link to="/" className="flex items-center space-x-2">
            <span className="font-bold text-xl">Fshop</span>
          </Link>

          <nav className="hidden md:flex gap-6">
            {mainNav.map((item) => (
              <Link
                key={item.name}
                to={item.to}
                className={`text-sm font-medium transition-colors hover:text-primary ${pathname === item.to ? "text-primary" : "text-muted-foreground"
                  }`}
              >
                {item.name}
              </Link>
            ))}
          </nav>
        </div>

        <div className="flex items-center gap-4">
          <div className="hidden md:flex relative">
            <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
            <Input
              type="search"
              placeholder="Search products..."
              className="w-[200px] lg:w-[300px] pl-8"
            />
          </div>

          <Button variant="ghost" size="icon" className="md:hidden" onClick={() => setIsSearchOpen(!isSearchOpen)}>
            <Search className="h-5 w-5" />
            <span className="sr-only">Search</span>
          </Button>

          <ThemeToggle />

          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" size="icon">
                <User className="h-5 w-5" />
                <span className="sr-only">User menu</span>
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end" className="w-56">
              <DropdownMenuLabel>My Account</DropdownMenuLabel>
              <DropdownMenuSeparator />
              <DropdownMenuItem asChild>
                <Link to="/profile">Profile</Link>
              </DropdownMenuItem>
              <DropdownMenuItem asChild>
                <Link to="/profile/orders">Orders</Link>
              </DropdownMenuItem>
              <DropdownMenuItem asChild>
                <Link to="/profile/wishlist">Wishlist</Link>
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>Log out</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>

          <Button variant="ghost" size="icon" className="relative" onClick={() => setIsCartOpen(!isCartOpen)}>
            <ShoppingCart className="h-5 w-5" />
            <Badge className="absolute -top-1 -right-1 h-5 w-5 flex items-center justify-center p-0">3</Badge>
            <span className="sr-only">Shopping cart</span>
          </Button>

          {/* <CartDropdown open={isCartOpen} onOpenChange={setIsCartOpen} /> */}
        </div>
      </div>

      {isSearchOpen && (
        <div className="container py-2 md:hidden">
          <div className="relative">
            <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
            <Input
              type="search"
              placeholder="Search products..."
              className="w-full pl-8"
            />
          </div>
        </div>
      )}
    </header>
  )
}

export default Header