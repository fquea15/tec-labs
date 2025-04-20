import { ChevronRight, Heart, Minus, Plus, Share2, ShoppingCart, Star, Truck } from 'lucide-react'
import React, { useState } from 'react'
import { Link } from 'react-router'

// UI Shadcn
import { Button } from "@ui/button"
import { Badge } from "@ui/badge"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@ui/tabs"

// Components product
import ProductCard from "@components/product/product-card"


const ProductDetailPage: React.FC = () => {
  const [mainImage, setMainImage] = useState(product.images[0])
  const [quantity, setQuantity] = useState(1)
  const [isFavorite, setIsFavorite] = useState(false)

  const increaseQuantity = () => {
    if (quantity < product.stock) {
      setQuantity(quantity + 1)
    }
  }

  const decreaseQuantity = () => {
    if (quantity > 1) {
      setQuantity(quantity - 1)
    }
  }

  return (
    <section className='flex justify-center'>
      <div className="container py-8">
        {/* Breadcrumbs */}
        <div className="flex items-center text-sm text-muted-foreground mb-8">
          <Link to="/" className="hover:text-primary">Home</Link>
          <ChevronRight className="h-4 w-4 mx-2" />
          <Link to="/categories" className="hover:text-primary">Categories</Link>
          <ChevronRight className="h-4 w-4 mx-2" />
          <Link to="/categories/electronics" className="hover:text-primary">Electronics</Link>
          <ChevronRight className="h-4 w-4 mx-2" />
          <span className="text-foreground">{product.name}</span>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-8 mb-12">
          {/* Product Images */}
          <div className="space-y-4">
            <div className="relative aspect-square overflow-hidden rounded-lg border">
              <img
                src={mainImage || "/placeholder.svg"}
                alt={product.name}
                className="object-cover"
              />
            </div>
            <div className="grid grid-cols-4 gap-4">
              {product.images.map((image, index) => (
                <button
                  key={index}
                  className={`relative aspect-square overflow-hidden rounded-md border ${mainImage === image ? "ring-2 ring-primary" : ""
                    }`}
                  onClick={() => setMainImage(image)}
                >
                  <img
                    src={image || "/placeholder.svg"}
                    alt={`${product.name} - Image ${index + 1}`}
                    className="object-cover"
                  />
                </button>
              ))}
            </div>
          </div>

          {/* Product Details */}
          <div className="flex flex-col">
            <div className="mb-4">
              {product.categories.map((category) => (
                <Badge key={category} variant="secondary" className="mr-2">
                  {category}
                </Badge>
              ))}
            </div>

            <h1 className="text-2xl md:text-3xl font-bold">{product.name}</h1>

            <div className="flex items-center mt-2 mb-4">
              <div className="flex items-center">
                {Array.from({ length: 5 }).map((_, i) => (
                  <Star
                    key={i}
                    className={`h-5 w-5 ${i < Math.floor(product.rating)
                      ? "fill-primary text-primary"
                      : "fill-muted text-muted-foreground"
                      }`}
                  />
                ))}
                <span className="ml-2 text-sm font-medium">{product.rating}</span>
              </div>
              <span className="mx-2 text-muted-foreground">·</span>
              <span className="text-sm text-muted-foreground">
                {product.reviews} reviews
              </span>
              <span className="mx-2 text-muted-foreground">·</span>
              <span className="text-sm text-muted-foreground">
                SKU: {product.sku}
              </span>
            </div>

            <div className="flex items-baseline mt-2 mb-6">
              <span className="text-3xl font-bold">${product.price.toFixed(2)}</span>
              {product.originalPrice && (
                <span className="ml-2 text-lg text-muted-foreground line-through">
                  ${product.originalPrice.toFixed(2)}
                </span>
              )}
              {product.originalPrice && (
                <Badge className="ml-2 bg-green-600 hover:bg-green-600">
                  Save ${(product.originalPrice - product.price).toFixed(2)}
                </Badge>
              )}
            </div>

            <p className="text-muted-foreground mb-6">{product.description}</p>

            <div className="flex items-center mb-6">
              <div className="flex items-center border rounded-md">
                <Button
                  variant="ghost"
                  size="icon"
                  className="h-10 w-10 rounded-none"
                  onClick={decreaseQuantity}
                  disabled={quantity <= 1}
                >
                  <Minus className="h-4 w-4" />
                  <span className="sr-only">Decrease quantity</span>
                </Button>
                <span className="w-12 text-center">{quantity}</span>
                <Button
                  variant="ghost"
                  size="icon"
                  className="h-10 w-10 rounded-none"
                  onClick={increaseQuantity}
                  disabled={quantity >= product.stock}
                >
                  <Plus className="h-4 w-4" />
                  <span className="sr-only">Increase quantity</span>
                </Button>
              </div>
              <span className="ml-4 text-sm text-muted-foreground">
                {product.stock} items available
              </span>
            </div>

            <div className="flex flex-col sm:flex-row gap-4 mb-6">
              <Button size="lg" className="flex-1 gap-2">
                <ShoppingCart className="h-5 w-5" />
                Add to Cart
              </Button>
              <Button
                variant="outline"
                size="lg"
                className={`flex-1 gap-2 ${isFavorite ? "text-red-500" : ""}`}
                onClick={() => setIsFavorite(!isFavorite)}
              >
                <Heart className={`h-5 w-5 ${isFavorite ? "fill-current" : ""}`} />
                {isFavorite ? "Added to Wishlist" : "Add to Wishlist"}
              </Button>
              <Button variant="outline" size="icon" className="h-12 w-12">
                <Share2 className="h-5 w-5" />
                <span className="sr-only">Share</span>
              </Button>
            </div>

            <div className="flex items-center p-4 bg-slate-50 dark:bg-slate-900 rounded-lg">
              <Truck className="h-5 w-5 mr-2 text-primary" />
              <span className="text-sm">
                Free shipping on orders over $50. Estimated delivery: 3-5 business days
              </span>
            </div>
          </div>
        </div>

        {/* Product Tabs */}
        <Tabs defaultValue="details" className="mb-12">
          <TabsList className="w-full justify-start border-b rounded-none h-auto p-0">
            <TabsTrigger
              value="details"
              className="rounded-none data-[state=active]:border-b-2 data-[state=active]:border-primary"
            >
              Details
            </TabsTrigger>
            <TabsTrigger
              value="specifications"
              className="rounded-none data-[state=active]:border-b-2 data-[state=active]:border-primary"
            >
              Specifications
            </TabsTrigger>
            <TabsTrigger
              value="reviews"
              className="rounded-none data-[state=active]:border-b-2 data-[state=active]:border-primary"
            >
              Reviews ({reviews.length})
            </TabsTrigger>
          </TabsList>
          <TabsContent value="details" className="pt-6">
            <div className="space-y-4">
              <h3 className="text-lg font-medium">Features</h3>
              <ul className="list-disc pl-5 space-y-2">
                {product.features.map((feature, index) => (
                  <li key={index}>{feature}</li>
                ))}
              </ul>
            </div>
          </TabsContent>
          <TabsContent value="specifications" className="pt-6">
            <div className="space-y-4">
              <h3 className="text-lg font-medium">Technical Specifications</h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                {Object.entries(product.specifications).map(([key, value]) => (
                  <div key={key} className="flex border-b pb-2">
                    <span className="font-medium w-1/3">{key}</span>
                    <span className="w-2/3">{value}</span>
                  </div>
                ))}
              </div>
            </div>
          </TabsContent>
          <TabsContent value="reviews" className="pt-6">
            <div className="space-y-6">
              <div className="flex items-center justify-between">
                <h3 className="text-lg font-medium">Customer Reviews</h3>
                <Button>Write a Review</Button>
              </div>

              <div className="space-y-6">
                {reviews.map((review) => (
                  <div key={review.id} className="border-b pb-6">
                    <div className="flex items-center mb-2">
                      <img
                        src={review.avatar || "/placeholder.svg"}
                        alt={review.user}
                        width={40}
                        height={40}
                        className="rounded-full mr-3"
                      />
                      <div>
                        <h4 className="font-medium">{review.user}</h4>
                        <div className="flex items-center">
                          <div className="flex">
                            {Array.from({ length: 5 }).map((_, i) => (
                              <Star
                                key={i}
                                className={`h-4 w-4 ${i < review.rating
                                  ? "fill-primary text-primary"
                                  : "fill-muted text-muted-foreground"
                                  }`}
                              />
                            ))}
                          </div>
                          <span className="ml-2 text-sm text-muted-foreground">
                            {review.date}
                          </span>
                        </div>
                      </div>
                    </div>
                    <h5 className="font-medium mb-1">{review.title}</h5>
                    <p className="text-muted-foreground">{review.content}</p>
                  </div>
                ))}
              </div>
            </div>
          </TabsContent>
        </Tabs>

        {/* Related Products */}
        <div className="space-y-6">
          <div className="flex items-center justify-between">
            <h2 className="text-2xl font-bold">Related Products</h2>
            <Link to="/categories/electronics" className="text-primary hover:underline">
              View All
            </Link>
          </div>
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-6">
            {relatedProducts.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
        </div>
      </div>
    </section>
  )
}

export default ProductDetailPage

// Mock product data
const product = {
  id: "1",
  name: "Premium Wireless Headphones",
  price: 129.99,
  originalPrice: 159.99,
  description: "Experience premium sound quality with our wireless headphones. Featuring active noise cancellation, long battery life, and comfortable ear cushions for extended listening sessions.",
  features: [
    "Active Noise Cancellation",
    "40-hour battery life",
    "Bluetooth 5.0 connectivity",
    "Built-in microphone for calls",
    "Foldable design for easy storage",
    "Premium sound quality with deep bass",
  ],
  specifications: {
    "Brand": "SoundMaster",
    "Model": "WH-1000XM4",
    "Color": "Matte Black",
    "Connectivity": "Bluetooth 5.0, 3.5mm audio jack",
    "Battery Life": "Up to 40 hours",
    "Charging Time": "3 hours",
    "Weight": "250g",
    "Dimensions": "7.3 x 3.1 x 9.4 inches",
    "Warranty": "1 year manufacturer warranty",
  },
  images: [
    "/placeholder.svg?height=600&width=600",
    "/placeholder.svg?height=600&width=600",
    "/placeholder.svg?height=600&width=600",
    "/placeholder.svg?height=600&width=600",
  ],
  rating: 4.8,
  reviews: 256,
  stock: 15,
  sku: "WH-1000XM4-BLK",
  categories: ["Electronics", "Audio", "Headphones"],
  tags: ["wireless", "bluetooth", "noise-cancellation", "headphones"],
}

// Mock related products
const relatedProducts = [
  { id: "2", name: "Wireless Earbuds", price: 89.99, image: "/placeholder.svg?height=400&width=400", rating: 4.7, reviews: 142, isNew: false },
  { id: "3", name: "Bluetooth Speaker", price: 59.99, image: "/placeholder.svg?height=400&width=400", rating: 4.6, reviews: 175, isNew: true },
  { id: "4", name: "Noise Cancelling Headphones", price: 149.99, image: "/placeholder.svg?height=400&width=400", rating: 4.9, reviews: 89, isNew: false },
  { id: "5", name: "Gaming Headset", price: 99.99, image: "/placeholder.svg?height=400&width=400", rating: 4.5, reviews: 112, isNew: true },
]

// Mock reviews
const reviews = [
  {
    id: "1",
    user: "John D.",
    avatar: "/placeholder.svg?height=40&width=40",
    rating: 5,
    date: "2 months ago",
    title: "Excellent sound quality!",
    content: "I've been using these headphones for about a month now and I'm extremely impressed with the sound quality. The noise cancellation is top-notch and the battery life is amazing. Highly recommend!",
  },
  {
    id: "2",
    user: "Sarah M.",
    avatar: "/placeholder.svg?height=40&width=40",
    rating: 4,
    date: "3 months ago",
    title: "Great headphones, but a bit heavy",
    content: "The sound quality is excellent and the noise cancellation works really well. My only complaint is that they get a bit uncomfortable after wearing them for several hours. Otherwise, they're perfect!",
  },
  {
    id: "3",
    user: "Michael T.",
    avatar: "/placeholder.svg?height=40&width=40",
    rating: 5,
    date: "1 month ago",
    title: "Best headphones I've ever owned",
    content: "These headphones are absolutely worth every penny. The sound is crisp and clear, the noise cancellation is incredible, and they're very comfortable to wear for long periods. I use them daily for work and they've been a game changer.",
  },
]