using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{
    [HarmonyPatch(typeof(SiegeTower))]
    internal class SiegeTowerPatch
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
