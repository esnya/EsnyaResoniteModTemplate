# Name Placeholder

A [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) mod for [Resonite](https://resonite.com/).

Code identity: `EsnyaResoniteModTemplate`

## Installation

1. Install the [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader).
2. Download `EsnyaResoniteModTemplate.dll` from GitHub Releases.
3. Place `EsnyaResoniteModTemplate.dll` into your `rml_mods` directory.
4. Launch Resonite.

## Mod Settings

- `Enabled`: enables the template Harmony patch lifecycle. Defaults to `true`.

## Development

### Requirements

- .NET 10 SDK
- A Resonite install, or fallback assemblies under `./Resonite`
- Optional: [ResoniteHotReloadLib](https://github.com/Nytra/ResoniteHotReloadLib) if you want hot reload

### Build

```sh
dotnet build .\EsnyaResoniteModTemplate.slnx -c Debug -p:ResonitePath="C:\Program Files (x86)\Steam\steamapps\common\Resonite"
```

### Test

```sh
dotnet test .\EsnyaResoniteModTemplate.slnx -c Debug -p:ResonitePath="C:\Program Files (x86)\Steam\steamapps\common\Resonite"
```

### Copy to `rml_mods`

```sh
dotnet build .\EsnyaResoniteModTemplate.slnx -c Debug -p:CopyToMods=true -p:ResonitePath="C:\Program Files (x86)\Steam\steamapps\common\Resonite"
```

### Hot Reload

Hot reload is opt-in. If `ResoniteHotReloadLib.dll` and `ResoniteHotReloadLibCore.dll` are present, enable it with:

```sh
dotnet build .\EsnyaResoniteModTemplate.slnx -c Debug -p:EnableHotReloadLibs=true -p:CopyToMods=true -p:ResonitePath="C:\Program Files (x86)\Steam\steamapps\common\Resonite"
```

## Versioning And Release

- Release version is derived from Git tags through `MinVer`.
- Push a tag in the form `vX.Y.Z` to create a GitHub Release for that version automatically.
- Non-tag builds keep using CI checks, but their build version is a `MinVer`-calculated pre-release version instead of a fixed repository version.
