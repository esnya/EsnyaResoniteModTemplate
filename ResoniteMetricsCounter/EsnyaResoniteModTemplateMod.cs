using System.Linq;
using System.Reflection;

using Elements.Core;

using HarmonyLib;

using ResoniteModLoader;



#if DEBUG
using ResoniteHotReloadLib;
#endif

namespace EsnyaResoniteModTemplate;

public partial class EsnyaResoniteModTemplate : ResoniteMod
{
    private static Assembly ModAssembly => typeof(EsnyaResoniteModTemplate).Assembly;

    public override string Name => ModAssembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
    public override string Author => ModAssembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company;
    public override string Version => ModAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    public override string Link => ModAssembly.GetCustomAttributes<AssemblyMetadataAttribute>().First(meta => meta.Key == "RepositoryUrl").Value;

    internal static string HarmonyId => $"com.nekometer.esnya.{ModAssembly.GetName()}";


    private static ModConfiguration? config;
    private static readonly Harmony harmony = new(HarmonyId);

    public override void OnEngineInit()
    {
        Init(this);

#if DEBUG
        HotReloader.RegisterForHotReload(this);
#endif
    }

    private static void Init(ResoniteMod modInstance)
    {
        harmony.PatchCategory(HarmonyId);
        config = modInstance?.GetConfiguration();
    }

#if DEBUG
    public static void BeforeHotReload()
    {
        try
        {
            harmony.UnpatchCategory(HarmonyId);
        }
        catch (System.Exception e)
        {
            Error(e);
        }
    }

    public static void OnHotReload(ResoniteMod modInstance)
    {
        Init(modInstance);
    }
#endif
}
