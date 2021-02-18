using Regulus.ZaWarudo.Extersion;
using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{
    [HarmonyPatch(typeof(TaleWorlds.MountAndBlade.UsableMissionObject))]
	internal class UsableMissionObjectPatch
	{
		[HarmonyPrefix]
		[HarmonyPatch("OnUse")]
		public static bool OnUsePreFix()
		{
			
			if (Main.Enable)
			{
				foreach (Agent agent in Mission.Current.Agents)
				{
					if (agent == Mission.Current.MainAgent)
						continue;
					if (agent.Controller == Agent.ControllerType.None)
					{
						agent.SetController(Agent.ControllerType.AI);						
					}
				}
			}
			return true;
		}

		[HarmonyPostfix]
		[HarmonyPatch("OnUse")]
		public static void OnUsePostFix()
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Invalid comparison between Unknown and I4
			if (!Main.Enable)
			{
				return;
			}
			foreach (Agent agent in Mission.Current.Agents)
			{
				if (agent == Mission.Current.MainAgent)
					continue;
				if (agent.Controller ==  Agent.ControllerType.AI)
				{
					agent.SetController(Agent.ControllerType.None);
				}
			}
		}
	}

}
