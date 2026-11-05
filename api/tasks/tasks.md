# Tasks

## Fix generic `object?` parameter in `GetAllWithSpec`

There is a parameter in the method `GetAllWithSpec` in for:

- api\Woodshed.Application\Contracts\Persistence\IAsyncRepository.cs.
- api\Woodshed.Infrastructure\Repositories\RepositoryBase.cs.

Which is used to serve as a bridge between the MappingProfile and the Handlers to share variables needed in the mapping process.

Maybe it's worth creating a generic class which contains this variables, though I should come up with a way to keep consistency between the variables defined in the Mapping Profile and this class or whatever. Since the name of the variable in the MappingProfile and this class or object or whatever must be the same and duplicated values which must keep consistence just sounds like a headache to maintain.

I'll leave it as a task while I come up with a way to do this better and consistent.

## Add email confirmation

Any dork without a validated email, like, sending a code and validate it, can register in the app and use it as normal, which should not be a thing at all, I have to come up with a way to validate email addresses that fits nicely with the architecture.

I was thinking I could send an email with a code, and have that code be written in cache, with the supposed email and the code. Example `<email>:<verification_code>` and have that resource be written in cache for a limited period of time, but I think this also needs some sort of validation of how much we can store in that cache before we just get rid of it, to avoid some sort of attack where they just blow up the cache memory entirely

I'm sure there is a way to properly do this, I have to investigate on this one.

---

If there are no task to be done, just leave this default message :D.
