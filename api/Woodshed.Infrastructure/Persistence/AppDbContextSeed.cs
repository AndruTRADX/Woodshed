using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Woodshed.Domain;
using Woodshed.Domain.Identity;

namespace Woodshed.Infrastructure.Persistence;

public class AppDbContextSeed
{
    public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
    {
        if (context.Instruments != null && !context.Instruments.Any())
        {
            var logger = loggerFactory.CreateLogger<AppDbContextSeed>();

            List<Instrument> instrument = [
                new() { Name = "Piano" },
                new() { Name = "Guitar" },
                new() { Name = "Violin" },
                new() { Name = "Drums" },
                new() { Name = "Flute" },
                new() { Name = "Saxophone" },
                new() { Name = "Trumpet" },
                new() { Name = "Trombone" },
                new() { Name = "Clarinet" },
                new() { Name = "Cello" },
                new() { Name = "Bass Guitar" },
                new() { Name = "Keyboard/Synthesizer" },
                new() { Name = "Harp" },
                new() { Name = "Harmonica" },
                new() { Name = "Ukulele" },
            ];

            context.Instruments.AddRange(instrument);
            await context.SaveChangesAsync();

            logger.LogInformation("Seed instruments completed for {Context}", nameof(AppDbContext));
        }

        if (context.InstrumentProficiencies != null && !context.InstrumentProficiencies.Any())
        {
            var logger = loggerFactory.CreateLogger<AppDbContextSeed>();

            List<InstrumentProficiency> instrumentProficiencies = [
                new()
                {
                    Code = Domain.Enums.ProficiencyLevel.Novice,
                    Name = "Novice",
                    Description = "You're just starting out, learning how the instrument works and getting comfortable with the basics."
                },
                new()
                {
                    Code = Domain.Enums.ProficiencyLevel.Beginner,
                    Name = "Beginner",
                    Description = "You know the fundamentals and can play simple melodies or chords, though you're still building consistency."
                },
                new()
                {
                    Code = Domain.Enums.ProficiencyLevel.Intermediate,
                    Name = "Intermediate",
                    Description = "You can play full songs comfortably, combine techniques, and you're starting to develop your own style."
                },
                new()
                {
                    Code = Domain.Enums.ProficiencyLevel.Advanced,
                    Name = "Advanced",
                    Description = "You have strong technical control, can tackle complex pieces, and adapt easily across different styles or genres."
                },
                new()
                {
                    Code = Domain.Enums.ProficiencyLevel.Expert,
                    Name = "Expert",
                    Description = "You can compose your own music. Being an expert isn't about flashy playing, it's about truly understanding the instrument and creating something of your own :3"
                },
            ];

            context.InstrumentProficiencies.AddRange(instrumentProficiencies);
            await context.SaveChangesAsync();

            logger.LogInformation("Seed instrument proficiencies completed for {Context}", nameof(AppDbContext));
        }
    }
}
