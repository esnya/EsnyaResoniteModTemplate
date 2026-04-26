using System.Reflection;
using HarmonyLib;
using ResoniteModLoader;
#if USE_RESONITE_HOT_RELOAD_LIB
using ResoniteHotReloadLib;
#endif

namespace EsnyaResoniteModTemplate;

/// <summary>
/// ResoniteModLoader entry point for Name Placeholder.
/// </summary>
public sealed class TemplateMod : ResoniteMod
{
    private const string ModNamespace = "com.nekometer.esnya";
    private static readonly Assembly Assembly = typeof(TemplateMod).Assembly;
    private static readonly string HarmonyId = $"{ModNamespace}.{Assembly.GetName().Name}";
    private static readonly Harmony Harmony = new(HarmonyId);
    private static readonly Lock PatchStateLock = new();

    private static ModConfiguration? config;
    private static bool patchesApplied;

    [AutoRegisterConfigKey]
    private static readonly ModConfigurationKey<bool> EnabledKey = new(
        "Enabled",
        "Enable Name Placeholder patches.",
        computeDefault: () => true);

    /// <inheritdoc />
    public override string Name =>
        Assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? Assembly.GetName().Name ?? string.Empty;

    /// <inheritdoc />
    public override string Author =>
        Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? string.Empty;

    /// <inheritdoc />
    public override string Version =>
        Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(static metadata => metadata.Key == "ModVersion")
            ?.Value
        ?? (Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? string.Empty)
            .Split('+')[0];

    /// <inheritdoc />
    public override string Link =>
        Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(static metadata => metadata.Key == "RepositoryUrl")
            ?.Value ?? string.Empty;

    /// <inheritdoc />
    public override void OnEngineInit()
    {
        Initialize(this);
    }

#if USE_RESONITE_HOT_RELOAD_LIB
    /// <summary>
    /// Removes Harmony patches before a hot reload cycle.
    /// </summary>
    public static void BeforeHotReload()
    {
        SetPatchesApplied(shouldPatch: false);
    }

    /// <summary>
    /// Reinitializes the mod after a hot reload cycle.
    /// </summary>
    /// <param name="mod">The reloaded mod instance.</param>
    public static void OnHotReload(ResoniteMod mod)
    {
        Initialize(mod);
    }
#endif

    private static void Initialize(ResoniteMod mod)
    {
        ArgumentNullException.ThrowIfNull(mod);

        config = mod.GetConfiguration();
        config?.OnThisConfigurationChanged += HandleConfigurationChanged;

        RefreshPatchState();

#if USE_RESONITE_HOT_RELOAD_LIB
        HotReloader.RegisterForHotReload(mod);
#endif
    }

    private static void HandleConfigurationChanged(ConfigurationChangedEvent _)
    {
        RefreshPatchState();
    }

    private static void RefreshPatchState()
    {
        SetPatchesApplied(PatchState.ShouldApplyPatches(GetConfigValue(EnabledKey, fallback: true)));
    }

    private static void SetPatchesApplied(bool shouldPatch)
    {
        lock (PatchStateLock)
        {
            if (patchesApplied == shouldPatch)
            {
                return;
            }

            if (shouldPatch)
            {
                Harmony.PatchAll(Assembly);
            }
            else
            {
                Harmony.UnpatchAll(HarmonyId);
            }

            patchesApplied = shouldPatch;
        }

        DebugFunc(() => $"[Name Placeholder] Harmony patches {(shouldPatch ? "applied" : "removed")}.");
    }

    private static T GetConfigValue<T>(ModConfigurationKey<T> key, T fallback)
    {
        ArgumentNullException.ThrowIfNull(key);

        return config is null ? fallback : config.TryGetValue(key, out T? value) ? value! : fallback;
    }
}
