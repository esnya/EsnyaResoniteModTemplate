using System.Reflection;
using Xunit;

namespace EsnyaResoniteModTemplate.Tests;

public sealed class TemplateModTests
{
    [Fact]
    public void Public_metadata_matches_project_properties()
    {
        // Arrange
        TemplateMod mod = new();
        Assembly assembly = typeof(TemplateMod).Assembly;
        AssemblyTitleAttribute? titleAttribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
        AssemblyCompanyAttribute? companyAttribute = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
        AssemblyInformationalVersionAttribute? versionAttribute =
            assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        AssemblyMetadataAttribute? repositoryUrlAttribute = assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(meta => meta.Key == "RepositoryUrl");

        // Assert
        Assert.Equal(ProjectPropertiesReader.ExpectedAssemblyTitle, mod.Name);
        Assert.Equal(ProjectPropertiesReader.ExpectedAuthor, mod.Author);
        Assert.False(string.IsNullOrWhiteSpace(mod.Version));
        Assert.Equal(ProjectPropertiesReader.ExpectedRepositoryUrl, mod.Link);

        // Guard that assembly attributes are present and non-empty to keep future refactors honest.
        Assert.False(string.IsNullOrWhiteSpace(titleAttribute?.Title));
        Assert.False(string.IsNullOrWhiteSpace(companyAttribute?.Company));
        Assert.False(string.IsNullOrWhiteSpace(versionAttribute?.InformationalVersion));
        Assert.False(string.IsNullOrWhiteSpace(repositoryUrlAttribute?.Value));
    }
}
