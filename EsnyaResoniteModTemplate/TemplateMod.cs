using System.Linq;
using System.Reflection;
using HarmonyLib;
using ResoniteModLoader;
#if DEBUG
using ResoniteHotReloadLib;
#endif

namespace EsnyaResoniteModTemplate;

/// <summary>Entry point for the mod.</summary>
public class TemplateMod : ResoniteMod
{
    private static readonly Assembly Assembly = typeof(TemplateMod).Assembly;
    private static readonly string HarmonyId = $"com.nekometer.esnya.{Assembly.GetName().Name}";
    private static readonly Harmony Harmony = new(HarmonyId);

    /// <inheritdoc />
    public override string Name =>
        Assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? string.Empty;

    /// <inheritdoc />
    public override string Author =>
        Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? string.Empty;

    /// <inheritdoc />
    public override string Version =>
        (
            Assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion ?? string.Empty
        ).Split('+')[0];

    /// <inheritdoc />
    public override string Link =>
        Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(meta => meta.Key == "RepositoryUrl")
            ?.Value ?? string.Empty;

    /// <inheritdoc />
    public override void OnEngineInit()
    {
        Harmony.PatchAll();
#if DEBUG
        HotReloader.RegisterForHotReload(this);
#endif
    }

#if DEBUG
    /// <summary>Removes Harmony patches before hot reload.</summary>
    public static void BeforeHotReload() => Harmony.UnpatchAll(HarmonyId);

    /// <summary>Reapplies Harmony patches after hot reload.</summary>
    /// <param name="mod">The reloaded mod.</param>
    public static void OnHotReload(ResoniteMod mod)
    {
        _ = mod;
        Harmony.PatchAll();
    }
#endif
}
