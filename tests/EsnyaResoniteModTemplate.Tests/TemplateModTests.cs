using System.Reflection;

namespace EsnyaResoniteModTemplate.Tests;

public sealed class TemplateModTests
{
    [Fact]
    public void PublicMetadataMatchesAssemblyProperties()
    {
        TemplateMod mod = new();
        Assembly assembly = typeof(TemplateMod).Assembly;

        Assert.Equal("Name Placeholder", mod.Name);
        Assert.Equal("esnya", mod.Author);
        Assert.False(string.IsNullOrWhiteSpace(mod.Version));
        Assert.Equal("https://github.com/esnya/NamePlaceholder", mod.Link);
        Assert.NotNull(assembly.GetCustomAttribute<AssemblyTitleAttribute>());
        Assert.NotNull(assembly.GetCustomAttribute<AssemblyCompanyAttribute>());
        Assert.NotNull(assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>());
        Assert.Contains(
            assembly.GetCustomAttributes<AssemblyMetadataAttribute>(),
            static metadata => metadata.Key == "RepositoryUrl" && metadata.Value == "https://github.com/esnya/NamePlaceholder");
    }

    [Fact]
    public void EnabledFalseDoesNotApplyPatches()
    {
        Assert.False(PatchState.ShouldApplyPatches(enabled: false));
    }

    [Fact]
    public void EnabledTrueAppliesPatches()
    {
        Assert.True(PatchState.ShouldApplyPatches(enabled: true));
    }
}
