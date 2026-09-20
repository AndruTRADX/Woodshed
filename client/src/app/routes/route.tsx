import App from "@/app/layout/App";
import AccountPage from "@/features/account/AccountPage";
import LoginPage from "@/features/account/LoginPage";
import RegisterPage from "@/features/account/RegisterPage";
import MessagePage from "@/features/messages/MessagePage";
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
      {
        path: "/messages",
        element: <MessagePage />,
      },
      {
        path: "/account",
        element: <AccountPage />,
      },
    ],
  },
]);
