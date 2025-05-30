# Agents Instructions

The CI workflow uses static checks that do not require Resonite assemblies.

- Formatting is enforced with `csharpier`.
- Before committing, run `dotnet csharpier --check .` to verify formatting.
- TODO: Explore additional static checks such as `dotnet format` with stub assemblies.
