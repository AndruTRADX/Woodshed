import { useCallback, useMemo } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import type { UserAccountResponse } from "@/shared/schemas/response/UserAccountResponse";
import { FieldGroup } from "@/shared/components/ui/field";
import TextInput from "@/shared/components/forms/TextInput";
import { Button } from "@/shared/components/ui/button";
import { Spinner } from "@/shared/components/ui/spinner";
import {
  EditAccountRequestSchema,
  type EditAccountRequest,
} from "@/features/account/schemas/request/EditAccountRequest";
import { toast } from "@/shared/stores/toastStore";
import { useEditAccount } from "@/features/account/hooks/api/useAccount";
import { useGetCurrentUser } from "@/shared/hooks/api/useAccount";

interface Props {
  account: UserAccountResponse;
  onSuccess: () => void;
  onCancel: () => void;
}

export default function EditAccountForm({
  account,
  onSuccess,
  onCancel,
}: Props) {
  const { editAccountAsync, isPendingEditAccount } = useEditAccount();
  const { user } = useGetCurrentUser();

  const form = useForm<EditAccountRequest>({
    resolver: zodResolver(EditAccountRequestSchema),
    mode: "onTouched",
    defaultValues: {
      nickName: account.nickName,
      name: user?.name ?? "",
      lastName: user?.lastName ?? "",
      biography: account.biography ?? "",
    },
  });

  const {
    formState: { isValid },
  } = form;

  const onSubmit = useCallback(
    async (data: EditAccountRequest) => {
      await editAccountAsync(data, {
        onSuccess: () => {
          toast.add({ title: "Account updated successfully" });
          onSuccess();
        },
      });
    },
    [editAccountAsync, onSuccess],
  );

  const isDisabled = useMemo(
    () => isPendingEditAccount || !isValid,
    [isPendingEditAccount, isValid],
  );

  return (
    <form id="edit-Account-form" onSubmit={form.handleSubmit(onSubmit)}>
      <FieldGroup>
        <TextInput
          control={form.control}
          name="nickName"
          label="Nickname"
          placeholder="Nickname"
        />
        <TextInput
          control={form.control}
          name="name"
          label="Name"
          placeholder="Name"
        />
        <TextInput
          control={form.control}
          name="lastName"
          label="Last Name"
          placeholder="Last Name"
        />
        <TextInput
          control={form.control}
          name="biography"
          label="Biography"
          multiline
          rows={4}
          placeholder="Tell us about yourself"
        />
      </FieldGroup>

      <div className="flex gap-2 justify-end mt-6">
        <Button type="button" variant="outline" onClick={onCancel}>
          Cancel
        </Button>
        <Button type="submit" disabled={isDisabled}>
          {isPendingEditAccount ? (
            <>
              <Spinner /> Saving
            </>
          ) : (
            "Save changes"
          )}
        </Button>
      </div>
    </form>
  );
}
