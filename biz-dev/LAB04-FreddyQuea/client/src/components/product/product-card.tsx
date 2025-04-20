import React, { useState } from 'react'

// UI Shadcn
import { Button } from "@ui/button"
import { Card, CardContent, CardFooter } from "@ui/card"
import { Badge } from "@ui/badge"
import { cn } from '@/lib/utils'
import { Link } from 'react-router'
import { Heart, ShoppingCart, Star } from 'lucide-react'

interface Product {
  id: string
  name: string
  price: number
  image: string
  rating: number
  reviews: number
  isNew?: boolean
}

const ProductCard: React.FC<{
  product: Product
  className?: string
}> = ({product, className}) => {
  const [isHovered, setIsHovered] = useState(false)
  const [isFavorite, setIsFavorite] = useState(false)

  return (
    <Card
      className={cn("overflow-hidden transition-all duration-200 hover:shadow-md", className)}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <div className="relative aspect-square overflow-hidden">
        <Link to={`/product/${product.id}`}>
          <img
            src={product.image || "/placeholder.svg"}
            alt={product.name}
            className={cn(
              "object-cover transition-transform duration-300",
              isHovered && "scale-105"
            )}
          />
        </Link>
        {product.isNew && (
          <Badge className="absolute top-3 left-3 bg-primary hover:bg-primary">
            New
          </Badge>
        )}
        <Button
          variant="secondary"
          size="icon"
          className={cn(
            "absolute top-3 right-3 h-8 w-8 rounded-full opacity-0 transition-opacity duration-200",
            isHovered && "opacity-100",
            isFavorite && "opacity-100 text-red-500"
          )}
          onClick={() => setIsFavorite(!isFavorite)}
        >
          <Heart className={cn("h-4 w-4", isFavorite && "fill-current")} />
          <span className="sr-only">Add to wishlist</span>
        </Button>
      </div>
      <CardContent className="p-4">
        <div className="mb-2 flex items-center">
          <div className="flex items-center">
            <Star className="h-4 w-4 fill-primary text-primary" />
            <span className="ml-1 text-sm font-medium">{product.rating}</span>
          </div>
          <span className="mx-2 text-muted-foreground">·</span>
          <span className="text-xs text-muted-foreground">
            {product.reviews} reviews
          </span>
        </div>
        <Link to={`/product/${product.id}`} className="group">
          <h3 className="font-medium group-hover:text-primary transition-colors">
            {product.name}
          </h3>
        </Link>
        <p className="mt-1 font-medium">${product.price.toFixed(2)}</p>
      </CardContent>
      <CardFooter className="p-4 pt-0">
        <Button
          className="w-full gap-2 transition-all duration-200"
          size="sm"
        >
          <ShoppingCart className="h-4 w-4" />
          Add to Cart
        </Button>
      </CardFooter>
    </Card>
  )
}

export default ProductCard