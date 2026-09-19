import App from "@/app/layout/App";
import LoginPage from "@/features/account/LoginPage";
import RegisterPage from "@/features/account/RegisterPage";
import PostsPage from "@/features/posts/PostPage";
import { createBrowserRouter } from "react-router";

export const router = createBrowserRouter([
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/register",
    element: <RegisterPage />,
  },
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "/posts",
        element: <PostsPage />,
      },
    ],
  },
]);
