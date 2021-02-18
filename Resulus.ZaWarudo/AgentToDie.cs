using System.Linq;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.Agent;

namespace Regulus.ZaWarudo
{
    public class AgentToDie
	{
		public Agent agent;

		public Blow blow;

		public KillInfo killInfo;

		public AgentToDie(Agent agent, Blow blow, KillInfo killInfo)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			this.agent = agent;
			this.blow = blow;
			this.killInfo = killInfo;
		}

		public void die()
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			if (agent == null)
				return;
			var mission = Mission.Current;
			if (mission == null)
				return;
			if(!mission.Agents.Any(a=>a == agent))
				return;
			agent.Die(blow, killInfo);
		}
	}
}