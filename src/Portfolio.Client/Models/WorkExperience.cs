namespace Portfolio.Client.Models;

public record WorkExperience(
    string Company,
    string Role,
    DateOnly StartDate,
    DateOnly? EndDate,
    string Description,
    string[] Achievements
);
