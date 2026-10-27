using PCL.Core.App.Region;

namespace PCL;

internal static class RestrictedFeaturePolicy
{
    // [本地修改] 已禁用区域功能限制：不再读取 Windows 区域信号，所有功能一律放行。
    // 回退方式：git checkout upstream/dev -- "Plain Craft Launcher 2/Modules/Minecraft/RestrictedFeaturePolicy.cs"
    private const bool RegionRestrictionEnabled = false;

    internal static RegionAssessment Assess() => RegionPolicy.Evaluate(RegionPolicy.ReadWindowsSignals());

    internal static bool IsRestricted => RegionRestrictionEnabled && Assess().IsRestricted;

    internal static bool Check(string feature, bool showHint = true) => true;

    internal static void ThrowIfRestricted(string feature)
    {
        if (Check(feature, false)) return;
        throw new ModBase.CancelledException();
    }
}
