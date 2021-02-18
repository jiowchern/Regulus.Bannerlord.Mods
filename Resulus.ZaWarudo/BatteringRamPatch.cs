using TaleWorlds.MountAndBlade;
using HarmonyLib;



namespace Regulus.ZaWarudo
{
    [HarmonyPatch(typeof(BatteringRam))]
    internal class BatteringRamPatch
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
