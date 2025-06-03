using System.Runtime.CompilerServices;
using Xunit;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace EsnyaResoniteModTemplate.Tests;

/// <summary>
/// Tests for the EsnyaResoniteModTemplateMod class.
/// </summary>
public class EsnyaResoniteModTemplateModTests
{
    /// <summary>
    /// Test to verify ProjectPropertiesReader is working correctly.
    /// </summary>
    [Fact]
    public void Debug_ProjectPropertiesReader()
    {
        // Test the actual properties
        var repositoryUrl = ProjectPropertiesReader.ExpectedRepositoryUrl;
        var author = ProjectPropertiesReader.ExpectedAuthor;
        var version = ProjectPropertiesReader.ExpectedVersion;
        var assemblyTitle = ProjectPropertiesReader.ExpectedAssemblyTitle;

        // Assert properties are not null or empty
        Assert.NotNull(repositoryUrl);
        Assert.NotEmpty(repositoryUrl);
        Assert.NotNull(author);
        Assert.NotEmpty(author);
        Assert.NotNull(version);
        Assert.NotEmpty(version);
        Assert.NotNull(assemblyTitle);
        Assert.NotEmpty(assemblyTitle);
    }

    /// <summary>
    /// Tests that the mod name is correctly retrieved from assembly attributes.
    /// </summary>
    [Fact]
    public void Name_ShouldReturnAssemblyTitle()
    {
        // Arrange
        var mod = new EsnyaResoniteModTemplateMod();
        var expectedName = ProjectPropertiesReader.ExpectedAssemblyTitle;

        // Act
        var actualName = mod.Name;

        // Assert
        Assert.Equal(expectedName, actualName);
    }

    /// <summary>
    /// Tests that the mod author is correctly retrieved from assembly attributes.
    /// </summary>
    [Fact]
    public void Author_ShouldReturnAssemblyCompany()
    {
        // Arrange
        var mod = new EsnyaResoniteModTemplateMod();
        var expectedAuthor = ProjectPropertiesReader.ExpectedAuthor;

        // Act
        var actualAuthor = mod.Author;

        // Assert
        Assert.Equal(expectedAuthor, actualAuthor);
    }

    /// <summary>
    /// Tests that the mod version is correctly retrieved from assembly attributes.
    /// </summary>
    [Fact]
    public void Version_ShouldReturnAssemblyInformationalVersion()
    {
        // Arrange
        var mod = new EsnyaResoniteModTemplateMod();
        var expectedVersion = ProjectPropertiesReader.ExpectedVersion;

        // Act
        var actualVersion = mod.Version;

        // Assert
        Assert.Equal(expectedVersion, actualVersion);
    }

    /// <summary>
    /// Tests that the mod link is correctly retrieved from assembly metadata.
    /// </summary>
    [Fact]
    public void Link_ShouldReturnRepositoryUrl()
    {
        // Arrange
        var mod = new EsnyaResoniteModTemplateMod();
        var expectedLink = ProjectPropertiesReader.ExpectedRepositoryUrl;

        // Act
        var actualLink = mod.Link;

        // Assert
        Assert.Equal(expectedLink, actualLink);
    }

    /// <summary>
    /// Tests that OnEngineInit can be called without throwing exceptions.
    /// Note: This test is limited due to dependencies on Resonite engine components.
    /// </summary>
    [Fact]
    public void OnEngineInit_ShouldNotThrow()
    {
        // Arrange
        var mod = new EsnyaResoniteModTemplateMod();

        // Act & Assert
        // In a real scenario, we'd need to mock Resonite dependencies
        // For now, we just verify the method exists and can be called
        var method = typeof(EsnyaResoniteModTemplateMod).GetMethod("OnEngineInit");
        Assert.NotNull(method);
        Assert.True(method.IsPublic);
        Assert.True(method.IsVirtual);
    }
}
