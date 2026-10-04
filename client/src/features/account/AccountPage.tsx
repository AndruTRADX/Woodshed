import SkeletonPage from "@/app/layout/components/SkeletonPage";
import AccountContent from "@/features/account/components/AccountContent";
import AccountHeader from "@/features/account/components/AccountHeader";
import { useGetAccountById } from "@/features/account/hooks/api/useAccount";
import { ErrorShow } from "@/shared/components/common/ErrorShow";
import { NoContent } from "@/shared/components/common/NoContent";
import { useParams } from "react-router";

export default function AccountPage() {
  const { id } = useParams();
  const { account, isLoadingAccount, errorAccount } = useGetAccountById(id);

  if (isLoadingAccount) {
    return <SkeletonPage />;
  }

  if (errorAccount) {
    return <ErrorShow error={errorAccount} />;
  }

  if (!account) {
    return (
      <NoContent
        title="No profile"
        description={`The profile you are looking for does not exists`}
      />
    );
  }

  return (
    <div className="flex flex-col w-full gap-6">
      <AccountHeader account={account} />
      <AccountContent account={account} />
    </div>
  );
}
