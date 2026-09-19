import { cn } from "cn";

import { Button } from "@sharedUi/button";
import { Card, CardContent } from "@sharedUi/card";
import { Field, FieldDescription, FieldGroup } from "@sharedUi/field";
import { Spinner } from "@sharedUi/spinner";
import { toast } from "@sharedUi/toast";
import TextInput from "@sharedForms/TextInput";

import { useMemo } from "react";
import { Piano } from "lucide-react";
import { useLocation, useNavigate } from "react-router";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import { useRegisterAccount } from "@account/hooks/api/useAccount";
import {
  RegisterRequestSchema,
  type RegisterRequest,
} from "@account/schemas/request/RegisterRequest";

export default function RegisterForm({
  className,
  ...props
}: React.ComponentProps<"div">) {
  const { isPendingRegisterAccount, registerAccountAsync } =
    useRegisterAccount();
  const navigate = useNavigate();
  const location = useLocation();

  const form = useForm({
    resolver: zodResolver(RegisterRequestSchema),
    mode: "onTouched",
  });

  const {
    formState: { isValid },
  } = form;

  async function onSubmit(data: RegisterRequest) {
    await registerAccountAsync(data, {
      onSuccess: () => {
        form.reset();
        navigate(location.state?.from || "/login");
      },
      onError: (e) => {
        console.log({ ...e });
        toast.add({ type: "error", title: e.message });
      },
    });
  }

  const isSubmitting = useMemo(() => {
    return isPendingRegisterAccount;
  }, [isPendingRegisterAccount]);

  const isDisabled = useMemo(() => {
    return isPendingRegisterAccount || !isValid;
  }, [isPendingRegisterAccount, isValid]);

  return (
    <div className={cn("flex flex-col gap-6", className)} {...props}>
      <Card className="overflow-hidden p-0">
        <CardContent className="grid p-0 md:grid-cols-2">
          <form
            className="p-6 md:p-8"
            id="login-form"
            onSubmit={form.handleSubmit(onSubmit)}
          >
            <FieldGroup>
              <div className="flex flex-col items-center gap-2 text-center">
                <h1 className="text-2xl font-bold flex gap-2 items-center">
                  This is your place <Piano className="text-primary" />
                </h1>
                <p className="text-balance text-muted-foreground">
                  Create an account and join a community of people as passionate
                  as you!
                </p>
              </div>

              <TextInput
                label="Email"
                type="email"
                control={form.control}
                name="email"
                required
                placeholder="akira-senju@acme.com"
              />
              <TextInput
                label="Password"
                control={form.control}
                type="password"
                name="password"
                required
                placeholder="Enter your password"
              />
              <TextInput
                label="Nickname"
                control={form.control}
                name="nickName"
                required
                placeholder="AkiraSenju10"
              />
              <TextInput
                label="Name"
                control={form.control}
                name="name"
                placeholder="Akira Senju"
              />

              <Field>
                <Button type="submit" disabled={isDisabled}>
                  {isSubmitting ? (
                    <>
                      <Spinner /> Creating account
                    </>
                  ) : (
                    "Register"
                  )}
                </Button>
              </Field>

              <FieldDescription className="text-center">
                Already have an account?{" "}
                <a
                  className="text-primary cursor-pointer"
                  onClick={() => navigate("/login")}
                >
                  Login
                </a>
              </FieldDescription>
            </FieldGroup>
          </form>
          <div className="relative hidden bg-muted md:block">
            <img
              src="images/login-image.jpg"
              alt="[CHANGE_THIS_LATER] - reference image"
              className="absolute inset-0 h-full w-full object-cover dark:brightness-[0.2] "
            />
          </div>
        </CardContent>
      </Card>
      <FieldDescription className="px-6 text-center">
        By clicking continue, you agree to our{" "}
        <a href="#" className="text-primary">
          Terms of Service
        </a>{" "}
        and{" "}
        <a href="#" className="text-primary">
          Privacy Policy
        </a>
        .
      </FieldDescription>
    </div>
  );
}
