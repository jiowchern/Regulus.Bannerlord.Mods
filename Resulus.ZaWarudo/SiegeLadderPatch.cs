using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{
    [HarmonyPatch(typeof(SiegeLadder))]
    internal class SiegeLadderPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnTick")]
        public static bool OnTickPreFix()
        {
            if (Main.Enable)
            {
                return false;
            }
            return true;
        }
    }

}
