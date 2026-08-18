import { Button } from "@/shared/components/ui/button"
import {
  Empty,
  EmptyContent,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@/shared/components/ui/empty"
import { XIcon } from "lucide-react";
import { useLocation, useNavigate } from "react-router"

export default function NotFoundPage() {
  const navigate = useNavigate()
  const { state } = useLocation()

  return (
    <Empty>
      <EmptyHeader>
        <EmptyMedia variant="icon">
          <XIcon stroke="2" />
        </EmptyMedia>
        <EmptyTitle>{state?.title ?? "Content not found"}</EmptyTitle>
        <EmptyDescription>
          {state?.message ?? "The content you are looking for has not been found"}
        </EmptyDescription>
      </EmptyHeader>
      <EmptyContent className="flex-row justify-center gap-2">
        <Button onClick={() => navigate("/activities")} size="lg">
          Go to the activities
        </Button>
      </EmptyContent>
    </Empty>
  )
}
