import { Button } from "@/shared/components/ui/button"
import {
  Empty,
  EmptyContent,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@/shared/components/ui/empty"
import { ComputerIcon } from "lucide-react";
import { useLocation, useNavigate } from "react-router"

export default function ServerErrorPage() {
  const navigate = useNavigate()
  const { state } = useLocation()

  return (
    <Empty>
      <EmptyHeader>
        <EmptyMedia variant="icon">
          <ComputerIcon stroke="2" />
        </EmptyMedia>
        <EmptyTitle>{state?.title ?? "There has been an error"}</EmptyTitle>
        <EmptyDescription>{state?.message ?? "Internal Server error"}</EmptyDescription>
      </EmptyHeader>
      <EmptyContent className="flex-row justify-center gap-2">
        <Button onClick={() => navigate("/activities")} size="lg">
          Go to the activities
        </Button>
      </EmptyContent>
    </Empty>
  )
}
