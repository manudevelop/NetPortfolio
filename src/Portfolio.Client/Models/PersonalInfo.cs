namespace Portfolio.Client.Models;

public record PersonalInfo(
    string Name,
    string Title,
    string Tagline,
    string Email,
    string Github,
    string LinkedIn,
    string Location,
    string? Phone = null
);
