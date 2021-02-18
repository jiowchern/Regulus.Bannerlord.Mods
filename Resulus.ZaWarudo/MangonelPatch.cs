using TaleWorlds.MountAndBlade;
using HarmonyLib;

namespace Regulus.ZaWarudo
{
    [HarmonyPatch(typeof(Mangonel))]
    internal class MangonelPatch
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
