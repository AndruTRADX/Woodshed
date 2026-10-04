import { useCallback, useState } from "react";
import { Button } from "@sharedUi/button";
import { Card, CardContent, CardHeader, CardTitle } from "@sharedUi/card";
import { PenBox, X } from "lucide-react";
import { useGetCurrentUser } from "@/shared/hooks/api/useAccount";
import type { UserAccountResponse } from "@/shared/schemas/response/UserAccountResponse";
import EditAccountForm from "@/features/account/forms/EditAccountForm";

interface Props {
  account: UserAccountResponse;
}

export default function AccountAboutCard({ account }: Props) {
  const [editMode, setEditMode] = useState(false);
  const { user: currentUser } = useGetCurrentUser();
  const isCurrentUser = currentUser?.id === account.id;

  const handleEditAccount = useCallback(() => {
    setEditMode((prev) => !prev);
  }, []);

  return (
    <Card>
      <CardHeader>
        <CardTitle className="w-full flex justify-between">
          About Me{" "}
          {isCurrentUser && (
            <Button
              variant={editMode ? "destructive" : "default"}
              size="icon-sm"
              onClick={handleEditAccount}
            >
              {editMode ? <X /> : <PenBox />}
            </Button>
          )}
        </CardTitle>
      </CardHeader>

      <CardContent>
        {editMode ? (
          <EditAccountForm
            account={account}
            onSuccess={() => setEditMode(false)}
            onCancel={() => setEditMode(false)}
          />
        ) : (
          <div className="bg-background backdrop-blur-lg rounded-r-xl border-l-4 border-foreground p-4 italic">
            {account.biography ? (
              <p className="text-sm leading-relaxed text-foreground">
                {account.biography}
              </p>
            ) : (
              <p className="text-sm text-muted-foreground">
                {isCurrentUser
                  ? "Tell others a bit about yourself by editing your Account."
                  : "This user hasn't added a bio yet."}
              </p>
            )}
          </div>
        )}
      </CardContent>
    </Card>
  );
}
