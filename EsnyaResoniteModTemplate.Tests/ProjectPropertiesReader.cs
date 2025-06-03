using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace EsnyaResoniteModTemplate.Tests;

/// <summary>
/// Helper class to read properties from MSBuild project files.
/// </summary>
internal static class ProjectPropertiesReader
{
    /// <summary>
    /// Reads a property value from Directory.Build.props file.
    /// </summary>
    /// <param name="propertyName">The name of the property to read.</param>
    /// <returns>The property value, or null if not found.</returns>
    public static string? GetPropertyFromDirectoryBuildProps(string propertyName)
    {
        try
        {
            // Get the solution directory by going up from the test project directory
            var testDirectory = Path.GetDirectoryName(
                typeof(ProjectPropertiesReader).Assembly.Location
            );
            var solutionDirectory =
                Directory.GetParent(testDirectory!)?.Parent?.Parent?.FullName
                ?? throw new InvalidOperationException("Could not determine solution directory");

            var directoryBuildPropsPath = Path.Combine(solutionDirectory, "Directory.Build.props");

            if (!File.Exists(directoryBuildPropsPath))
            {
                throw new FileNotFoundException(
                    $"Directory.Build.props not found at: {directoryBuildPropsPath}"
                );
            }

            var doc = XDocument.Load(directoryBuildPropsPath);

            // Find the property in any PropertyGroup
            var propertyElement = doc.Descendants(propertyName).FirstOrDefault();
            var value = propertyElement?.Value;

            return value;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Failed to read property '{propertyName}' from Directory.Build.props: {ex.Message}",
                ex
            );
        }
    }

    /// <summary>
    /// Gets the expected repository URL from Directory.Build.props.
    /// </summary>
    public static string ExpectedRepositoryUrl =>
        GetPropertyFromDirectoryBuildProps("RepositoryUrl")
        ?? throw new InvalidOperationException(
            "RepositoryUrl property not found in Directory.Build.props"
        );

    /// <summary>
    /// Gets the expected author from Directory.Build.props.
    /// </summary>
    public static string ExpectedAuthor =>
        GetPropertyFromDirectoryBuildProps("Authors")
        ?? throw new InvalidOperationException(
            "Authors property not found in Directory.Build.props"
        );

    /// <summary>
    /// Gets the expected version from Directory.Build.props.
    /// </summary>
    public static string ExpectedVersion =>
        GetPropertyFromDirectoryBuildProps("Version")
        ?? throw new InvalidOperationException(
            "Version property not found in Directory.Build.props"
        );

    /// <summary>
    /// Gets the expected assembly title (project name) from Directory.Build.props or defaults to project name.
    /// </summary>
    public static string ExpectedAssemblyTitle => "EsnyaResoniteModTemplate";
}
