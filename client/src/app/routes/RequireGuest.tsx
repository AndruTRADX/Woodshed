import { useGetCurrentUser } from "@/shared/hooks/api/useAccount";
import { Spinner } from "@sharedUi/spinner";
import { Navigate, Outlet } from "react-router";

export default function RequireGuest() {
  const { user, isLoadingUser } = useGetCurrentUser();

  if (isLoadingUser) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <Spinner />
      </div>
    );
  }

  if (user) {
    return <Navigate to="/posts" replace />;
  }

  return <Outlet />;
}
