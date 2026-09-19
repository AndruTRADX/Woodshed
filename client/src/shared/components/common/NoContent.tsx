import { Empty, EmptyDescription, EmptyHeader, EmptyMedia, EmptyTitle } from "@sharedUi/empty"
import { X } from "lucide-react";


interface Props {
  title?: string
  description?: string
}

export function NoContent({ title, description }: Props) {
  return (
    <Empty>
      <EmptyHeader>
        <EmptyMedia variant="icon">
          <X stroke="2" />
        </EmptyMedia>
        <EmptyTitle>{title ?? "Content not found"}</EmptyTitle>
        <EmptyDescription>
          {description ?? "The content you are looking for has not been found"}
        </EmptyDescription>
      </EmptyHeader>
    </Empty>
  )
}