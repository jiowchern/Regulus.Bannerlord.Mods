using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{
    namespace Extersion
    {
        public static class AgentExtersion
        {
            public static void SetController(this Agent agent, Agent.ControllerType type)
            {
                HarmonyLib.Traverse.Create(agent).Method("SetController", type).GetValue();
            }
        }
        
    }
}