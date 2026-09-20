import App from "@/app/layout/App";
import RequireAuth from "@/app/routes/RequireAuth";
import RequireGuest from "@/app/routes/RequireGuest";
import AccountPage from "@/features/account/AccountPage";
import LoginPage from "@/features/account/LoginPage";
import RegisterPage from "@/features/account/RegisterPage";
import NotFoundPage from "@/features/errors/NotFoundPage";
import ServerErrorPage from "@/features/errors/ServerErrorPage";
import MessagePage from "@/features/messages/MessagePage";
import PostsPage from "@/features/posts/PostPage";
import { createBrowserRouter } from "react-router";

export const router = createBrowserRouter([
  {
    element: <RequireGuest />,
    children: [
      {
        path: "login",
        element: <LoginPage />,
      },
      {
        path: "register",
        element: <RegisterPage />,
      },
    ],
  },
  {
    element: <RequireAuth />,
    children: [
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
    ],
  },
  {
    path: "not-found",
    element: <NotFoundPage />,
  },
  {
    path: "server-error",
    element: <ServerErrorPage />,
  },
  {
    path: "*",
    element: <NotFoundPage />,
  },
]);
