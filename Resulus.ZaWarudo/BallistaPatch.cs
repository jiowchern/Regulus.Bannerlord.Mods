namespace Regulus.ZaWarudo
{
    using HarmonyLib;
    using TaleWorlds.MountAndBlade;

    [HarmonyPatch(typeof(Ballista))]
    internal class BallistaPatch
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
