import React from 'react'
import { Link } from 'react-router'
import { ArrowRight, ChevronRight } from 'lucide-react'

// UI Shadcn
import { Button } from "@ui/button"
import { Card } from "@ui/card"
import { Badge } from "@ui/badge"
import ProductCard from '@/components/product/product-card'

const HomePage: React.FC = () => {
  return (
    <div className="flex flex-col gap-12 pb-8">
      {/* Hero Banner */}
      <section className="relative h-[500px] overflow-hidden">
        <div className="absolute inset-0 bg-slate-900/60 z-10" />
        <img
          src="/placeholder.svg?height=1000&width=2000"
          alt="Hero Banner"
          className="object-cover"
        />
        <div className="container relative z-20 flex h-full flex-col items-center justify-center text-center text-white">
          <h1 className="text-4xl font-bold tracking-tight sm:text-5xl md:text-6xl">
            Summer Collection 2025
          </h1>
          <p className="mt-4 max-w-2xl text-lg sm:text-xl">
            Discover our latest arrivals and trending products with exclusive offers.
          </p>
          <div className="mt-8 flex flex-wrap gap-4 justify-center">
            <Button size="lg" asChild>
              <Link to="/categories">Shop Now</Link>
            </Button>
            <Button size="lg" variant="outline" className="bg-transparent text-white border-white hover:bg-white hover:text-slate-900">
              <Link to="/new-arrivals">New Arrivals</Link>
            </Button>
          </div>
        </div>
      </section>

      <div className="flex flex-col justify-center w-full items-center gap-12">
        {/* Featured Categories */}
        <section className="container">
          <div className="flex items-center justify-between mb-8">
            <h2 className="text-2xl font-bold tracking-tight sm:text-3xl">Shop by Category</h2>
            <Link to="/categories" className="flex items-center text-sm font-medium text-primary">
              View All Categories <ChevronRight className="ml-1 h-4 w-4" />
            </Link>
          </div>
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-6">
            {categories.map((category) => (
              <Link key={category.id} to={`/categories/${category.id}`}>
                <Card className="overflow-hidden transition-all duration-200 hover:shadow-md group">
                  <div className="relative aspect-square overflow-hidden">
                    <img
                      src={category.image || "/placeholder.svg"}
                      alt={category.name}
                      className="object-cover transition-transform duration-300 group-hover:scale-105"
                    />
                    <div className="absolute inset-0 bg-gradient-to-t from-slate-900/60 to-transparent" />
                    <div className="absolute bottom-0 left-0 right-0 p-4 text-white">
                      <h3 className="text-xl font-semibold">{category.name}</h3>
                      <p className="text-sm opacity-90">{category.count} products</p>
                    </div>
                  </div>
                </Card>
              </Link>
            ))}
          </div>
        </section>

        {/* Featured Products */}
        <section className="container">
          <div className="flex items-center justify-between mb-8">
            <h2 className="text-2xl font-bold tracking-tight sm:text-3xl">Featured Products</h2>
            <Link to="/products" className="flex items-center text-sm font-medium text-primary">
              View All Products <ChevronRight className="ml-1 h-4 w-4" />
            </Link>
          </div>
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
            {featuredProducts.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
        </section>

        {/* Promotion Banner */}
        <section className="container">
          <div className="relative overflow-hidden rounded-xl">
            <div className="absolute inset-0 bg-slate-900/40 z-10" />
            <img
              src="/placeholder.svg?height=600&width=1200"
              alt="Promotion Banner"
              width={1200}
              height={400}
              className="w-full h-[300px] md:h-[400px] object-cover"
            />
            <div className="absolute inset-0 z-20 flex flex-col items-start justify-center p-8 md:p-12">
              <Badge className="mb-4 bg-primary hover:bg-primary">Limited Time Offer</Badge>
              <h2 className="max-w-md text-3xl font-bold tracking-tight text-white sm:text-4xl md:text-5xl">
                Up to 50% Off Summer Sale
              </h2>
              <p className="mt-4 max-w-md text-lg text-white/90">
                Don't miss out on our biggest sale of the season. Shop now for the best deals.
              </p>
              <Button size="lg" className="mt-8" asChild>
                <Link to="/sale">
                  Shop the Sale <ArrowRight className="ml-2 h-4 w-4" />
                </Link>
              </Button>
            </div>
          </div>
        </section>
      </div>
    </div>
  )
}

export default HomePage

// Mock data
const categories = [
  { id: "1", name: "Electronics", image: "/placeholder.svg?height=300&width=300", count: 120 },
  { id: "2", name: "Clothing", image: "/placeholder.svg?height=300&width=300", count: 250 },
  { id: "3", name: "Home & Kitchen", image: "/placeholder.svg?height=300&width=300", count: 180 },
  { id: "4", name: "Beauty", image: "/placeholder.svg?height=300&width=300", count: 95 },
]

const featuredProducts = [
  { id: "1", name: "Wireless Headphones", price: 129.99, image: "/placeholder.svg?height=400&width=400", rating: 4.5, reviews: 128, isNew: true },
  { id: "2", name: "Smart Watch", price: 199.99, image: "/placeholder.svg?height=400&width=400", rating: 4.8, reviews: 256, isNew: false },
  { id: "3", name: "Laptop Backpack", price: 79.99, image: "/placeholder.svg?height=400&width=400", rating: 4.3, reviews: 89, isNew: false },
  { id: "4", name: "Bluetooth Speaker", price: 59.99, image: "/placeholder.svg?height=400&width=400", rating: 4.6, reviews: 175, isNew: true },
  { id: "5", name: "Fitness Tracker", price: 49.99, image: "/placeholder.svg?height=400&width=400", rating: 4.2, reviews: 63, isNew: false },
  { id: "6", name: "Wireless Earbuds", price: 89.99, image: "/placeholder.svg?height=400&width=400", rating: 4.7, reviews: 142, isNew: false },
  { id: "7", name: "Digital Camera", price: 349.99, image: "/placeholder.svg?height=400&width=400", rating: 4.4, reviews: 78, isNew: true },
  { id: "8", name: "Portable Charger", price: 39.99, image: "/placeholder.svg?height=400&width=400", rating: 4.1, reviews: 112, isNew: false },
]