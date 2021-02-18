using HarmonyLib;

namespace Regulus.ZaWarudo
{
    [HarmonyPatch(typeof(TaleWorlds.MountAndBlade.FormationMovementComponent))]
    internal class FormationMovementComponentPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("AdjustSpeedLimit")]
        private static bool TickPreFix(TaleWorlds.MountAndBlade.FormationMovementComponent __instance)
        {
            if (Main.Enable)
            {
                return false;
            }
            return true;
        }
    }

}
