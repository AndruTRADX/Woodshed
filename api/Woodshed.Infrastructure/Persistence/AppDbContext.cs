using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Woodshed.Domain;
using Woodshed.Domain.Identity;

namespace Woodshed.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Instrument> Instruments { get; set; }
    public DbSet<InstrumentProficiency> InstrumentProficiencies { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostComment> PostComments { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
    public DbSet<UserFollower> UserFollowers { get; set; }
    public DbSet<UserInstrument> UserInstruments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(x =>
        {
            x.Property(u => u.Id).HasMaxLength(36);
            x.HasIndex(u => u.NickName).IsUnique();
        });

        #region Identity
        builder.Entity<IdentityUserClaim<string>>()
            .Property(c => c.UserId)
            .HasMaxLength(36);

        builder.Entity<IdentityUserLogin<string>>()
            .Property(l => l.UserId)
            .HasMaxLength(36);

        builder.Entity<IdentityUserToken<string>>()
            .Property(t => t.UserId)
            .HasMaxLength(36);

        builder.Entity<IdentityUserRole<string>>()
            .Property(r => r.UserId)
            .HasMaxLength(36);
        #endregion Identity

        builder.Entity<Instrument>()
            .HasIndex(i => i.Name)
            .IsUnique();

        builder.Entity<Photo>()
            .HasOne(au => au.User)
            .WithMany(a => a.Photos)
            .HasForeignKey(au => au.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Post>()
            .HasOne(au => au.User)
            .WithMany(a => a.Posts)
            .HasForeignKey(au => au.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PostComment>()
            .HasOne(pc => pc.Post)
            .WithMany(pc => pc.Comments)
            .HasForeignKey(pc => pc.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PostComment>()
            .HasOne(pc => pc.User)
            .WithMany(pc => pc.PostComments)
            .HasForeignKey(pc => pc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserInstrument>(x =>
        {
            x.HasKey(k => new { k.InstrumentId, k.UserId });

            x.HasOne(u => u.User)
                .WithMany(ui => ui.UserInstruments)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            x.HasOne(i => i.Instrument)
                .WithMany(ui => ui.UserInstruments)
                .HasForeignKey(i => i.InstrumentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<UserFollower>(x =>
        {
            x.HasKey(k => new { k.FollowerId, k.FolloweeId });

            x.HasOne(f => f.Follower)
                .WithMany(f => f.Following)
                .HasForeignKey(o => o.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            x.HasOne(f => f.Followee)
                .WithMany(f => f.Followers)
                .HasForeignKey(o => o.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<PostLike>(x =>
        {
            x.HasKey(k => new { k.PostId, k.UserId });

            x.HasOne(f => f.User)
                .WithMany(f => f.PostLikes)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            x.HasOne(f => f.Post)
                .WithMany(f => f.Likes)
                .HasForeignKey(o => o.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        base.OnConfiguring(optionsBuilder);
    }
}
