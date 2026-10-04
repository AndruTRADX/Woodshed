import { Tabs, TabsContent, TabsList, TabsTrigger } from "@sharedUi/tabs"
import { useMediaQuery } from "@/shared/hooks/useMediaQuery"
import type { UserAccountResponse } from "@/shared/schemas/response/UserAccountResponse";
import AccountAboutCard from "@/features/account/components/cards/AccountAboutCard";
import AccountPhotosCard from "@/features/account/components/cards/AccountPhotosCard";
import AccountFollowersCard from "@/features/account/components/cards/AccountFollowersCard";
import AccountFollowingCard from "@/features/account/components/cards/AccountFollowingCard";

interface Props {
  account: UserAccountResponse
}

export default function AccountContent({ account }: Props) {
  const isDesktop = useMediaQuery("(min-width: 1024px)")

  return (
    <Tabs defaultValue="about" orientation={isDesktop ? "vertical" : "horizontal"}>
      <TabsList
        variant="line"
        className={isDesktop ? undefined : "w-full justify-start overflow-x-auto"}
      >
        <TabsTrigger value="about">About</TabsTrigger>
        <TabsTrigger value="photos">Photos</TabsTrigger>
        <TabsTrigger value="followers">Followers</TabsTrigger>
        <TabsTrigger value="following">Following</TabsTrigger>
      </TabsList>
      <TabsContent value="about">
        <AccountAboutCard account={account} />
      </TabsContent>
      <TabsContent value="photos">
        <AccountPhotosCard />
      </TabsContent>
      <TabsContent value="followers">
        <AccountFollowersCard />
      </TabsContent>
      <TabsContent value="following">
        <AccountFollowingCard />
      </TabsContent>
    </Tabs>
  )
}