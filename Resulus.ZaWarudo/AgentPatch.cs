using HarmonyLib;
using Regulus.ZaWarudo.Extersion;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{


    [HarmonyPatch(typeof(Agent))]
    internal class AgentPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Mount")]
        public static bool MountPreFix(Agent mountAgent)
        {
            if (Main.Enable)
            {
                if(mountAgent != Mission.Current.MainAgent)
                {
                    mountAgent.SetController(Agent.ControllerType.None);
                }
                
            }
            return true;
        }

        //[HarmonyPrefix]
        //[HarmonyPatch("TickAsAI")]
        public static bool TickAsAIPreFix(Agent __instance,float dt)
        {
            if (Main.Enable)
            {
                if (__instance != Mission.Current.MainAgent)
                {
                    return false;
                }

            }
            return true;
        }

        //[HarmonyPrefix]
        //[HarmonyPatch("Die")]
        public static bool DiePreFix(Agent __instance, Blow b, Agent.KillInfo overrideKillInfo =  Agent.KillInfo.Invalid)
        {
            
            if (Main.Enable)            
            {
                if(!Main.agentToDice.ContainsKey(__instance))
                {
                    Main.agentToDice.Add(__instance,new AgentToDie(__instance, b, overrideKillInfo));
                }
                
                
                return false;
            }
            return true;
        }
    }

}
