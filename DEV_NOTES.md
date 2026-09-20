# DEV Notes

These are some insights that might be useful for anyone looking at this repository who wants to know how things were developed in the first place. For me, though, it's just a place to put my yapping about how and why I did things the way I did.

## Use of Radix UI and `menuColor: default`

If you want to keep the animations of shadcn components, use either React Aria or Radix UI for the ones that open a menu to display information (like dropdowns). In my case, I migrated the project from Base UI to Radix UI.

Keep in mind that the migration is not just swapping the library: Base UI composes components through the `render` prop, while Radix UI uses `asChild` (for example, in `SidebarMenuButton` and `DropdownMenuTrigger`).

Also, for some weird reason, if you set `menuColor` to `default-translucent`, the animations of these components are lost. So don't do that if you want to keep the animations in the project.

## Use of music effects to enhance user experience

I use GarageBand on my iPhone to create the sound effects of the application. The idea is to have an interface that feels responsive when performing actions. I took inspiration from classic Nintendo interfaces, which made you feel like the application was a place where you could live. This is a very primitive version of that, and I want to explore more ways of making Woodshed feel like a personalized place where users can own the result of their actions.

Also, if you think the effects are shitty, my bad: I'm not much of a pianist, but I want to learn to play better someday. So, if you have suggestions, feel free to share them :3

## Piano-like interface

As mentioned in the previous section, I want Woodshed to be a personalized experience for musicians, so the application tries to have some sort of resemblance to an actual piano. I took inspiration from real pianos to design it.
