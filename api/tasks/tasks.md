# Fix generic `object?` parameter in `GetAllWithSpec`

There is a parameter in the method `GetAllWithSpec` in for:

- api\Woodshed.Application\Contracts\Persistence\IAsyncRepository.cs.
- api\Woodshed.Infrastructure\Repositories\RepositoryBase.cs.

Which is used to serve as a bridge between the MappingProfile and the Handlers to share variables needed in the mapping process.

Maybe it's worth creating a generic class which contains this variables, though I should come up with a way to keep consistency between the variables defined in the Mapping Profile and this class or whatever. Since the name of the variable in the MappingProfile and this class or object or whatever must be the same and duplicated values which must keep consistence just sounds like a headache to maintain.

I'll leave it as a task while I come up with a way to do this better and consistent.

---

If there are no task to be done, just leave this default message :D.
