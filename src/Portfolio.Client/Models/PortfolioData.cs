namespace Portfolio.Client.Models;

public record PortfolioData(
    PersonalInfo Personal,
    Skill[] Skills,
    Project[] Projects,
    WorkExperience[] Experience
);
