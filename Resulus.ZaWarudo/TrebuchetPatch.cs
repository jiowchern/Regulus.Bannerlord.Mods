using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{
    [HarmonyPatch(typeof(Trebuchet))]
    internal class TrebuchetPatch
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
