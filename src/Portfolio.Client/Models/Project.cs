namespace Portfolio.Client.Models;

public record Project(
    string Id,
    string Title,
    string Description,
    string[] Technologies,
    string? GithubUrl,
    string? DemoUrl,
    bool IsFeatured
);
