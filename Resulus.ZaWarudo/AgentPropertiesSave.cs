using System.Reflection;
using TaleWorlds.MountAndBlade;
using System.Linq;
using System;
namespace Regulus.ZaWarudo
{
    internal class AgentPropertiesSave
	{
		private AgentDrivenProperties save = new AgentDrivenProperties();

		private AgentDrivenProperties newSave = new AgentDrivenProperties();

		public Agent agent { get; private set; }

		public bool isNewSaveBroken { get; private set; }

		public AgentPropertiesSave(Agent agent)
		{
			this.agent = agent;
			saveProperties();
		}

		public void OnSaveBroken()
		{
			isNewSaveBroken = true;
		}

		private void saveProperties()
		{
			
			foreach (PropertyInfo item in from p in agent.AgentDrivenProperties.GetType().GetProperties()
										  where p.CanRead
										  select p)
			{
				item.SetValue(save, item.GetValue(agent.AgentDrivenProperties));
			}
		}

		public void saveNewProperties()
		{
			foreach (PropertyInfo item in from p in agent.AgentDrivenProperties.GetType().GetProperties()
										  where p.CanRead
										  select p)
			{
				item.SetValue(newSave, item.GetValue(agent.AgentDrivenProperties));
			}
			Agent obj = agent;
			obj.OnAgentWieldedItemChange = (Action)Delegate.Combine(obj.OnAgentWieldedItemChange, new Action(OnSaveBroken));
			Agent obj2 = agent;
			obj2.OnAgentMountedStateChanged = (Action)Delegate.Combine(obj2.OnAgentMountedStateChanged, new Action(OnSaveBroken));
			isNewSaveBroken = false;
		}

		private void restoreNewProperties()
		{
			foreach (PropertyInfo item in from p in save.GetType().GetProperties()
										  where p.CanRead
										  select p)
			{
				item.SetValue(agent.AgentDrivenProperties, item.GetValue(newSave));
			}
			agent.UpdateCustomDrivenProperties();
			isNewSaveBroken = false;
		}

		public void tick()
		{
			if (isNewSaveBroken)
			{
				restoreNewProperties();
			}
		}

		public void restoreProperties()
		{
			foreach (PropertyInfo item in from p in save.GetType().GetProperties()
										  where p.CanRead
										  select p)
			{
				item.SetValue(agent.AgentDrivenProperties, item.GetValue(save));
			}
			agent.UpdateCustomDrivenProperties();
			Agent obj = agent;
			obj.OnAgentWieldedItemChange = (Action)Delegate.Remove(obj.OnAgentWieldedItemChange, new Action(OnSaveBroken));
			Agent obj2 = agent;
			obj2.OnAgentMountedStateChanged = (Action)Delegate.Remove(obj2.OnAgentMountedStateChanged, new Action(OnSaveBroken));
		}
	}
}