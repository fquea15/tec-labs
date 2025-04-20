import {
  createBrowserRouter,
  RouterProvider,
} from "react-router";

// Layouts
import RootLayout from "@components/layout"; 
import HomePage from "@pages/home";
import ProductDetailPage from "@/pages/product/product-detail";

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    children: [
      { index: true, element: <HomePage /> },
      // { path: "category/:categoryId", element: <CategoryPage /> },
      { path: "product/:productId", element: <ProductDetailPage /> },
      // { path: "cart", element: <CartPage /> },
      // { path: "checkout", element: <CheckoutPage /> },
      // { path: "profile", element: <ProfilePage /> },
    ],
  },
  // {
  //   path: "/admin",
  //   // element: <AdminDashboard />,
  // },
  // {
  //   path: "*",
  //   element: <NotFound />,
  // },
]);

export function AppRouter() {
  return <RouterProvider router={router} />;
}
