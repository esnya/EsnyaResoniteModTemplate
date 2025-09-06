#pragma warning disable CS1591
using Xunit;

namespace EsnyaResoniteModTemplate.Tests;

public sealed class ProjectPropertiesTests
{
    [Fact]
    public void Debug_ProjectPropertiesReader()
    {
        var repositoryUrl = ProjectPropertiesReader.ExpectedRepositoryUrl;
        var author = ProjectPropertiesReader.ExpectedAuthor;
        var version = ProjectPropertiesReader.ExpectedVersion;
        var assemblyTitle = ProjectPropertiesReader.ExpectedAssemblyTitle;

        Assert.NotNull(repositoryUrl);
        Assert.NotEmpty(repositoryUrl);
        Assert.NotNull(author);
        Assert.NotEmpty(author);
        Assert.NotNull(version);
        Assert.NotEmpty(version);
        Assert.NotNull(assemblyTitle);
        Assert.NotEmpty(assemblyTitle);
    }
}
#pragma warning restore CS1591
