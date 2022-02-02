using Regulus.Utility;
using TaleWorlds.MountAndBlade;
using Regulus.ZaWarudo.Extersion;
using TaleWorlds.Engine;
using TaleWorlds.DotNet;
using TaleWorlds.Library;
using TaleWorlds.InputSystem;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Regulus.ZaWarudo
{

    internal class RunningStatsu :  Regulus.Utility.IStatus
    {


		readonly System.Collections.Generic.Dictionary<Mission.Missile, ZaWarudoMissile> _Missiles;        

        public event System.Action DoneEvent;
        public RunningStatsu()
        {
			_Missiles = new Dictionary<Mission.Missile, ZaWarudoMissile>();
			


		}

        void IStatus.Enter()
        {			
			Main.Enable = true;
			

			var mainAgent = Mission.Current.MainAgent;
			


			foreach (var agent in Mission.Current.Agents)
			{
				if (agent == mainAgent)
					continue;
				agent.SetMaximumSpeedLimit(0.01f, false);
				agent.SetController(Agent.ControllerType.None);
				agent.MovementFlags &= ~Agent.MovementControlFlag.MoveMask;
			}
			

		}

		void IStatus.Leave()
        {
			Main.Enable = false;
			if (Mission.Current == null)
				return;

			
			foreach (Agent agent in Mission.Current.Agents.ToArray())
			{
				if (agent == Mission.Current.MainAgent)
					continue;

				agent.SetMaximumSpeedLimit(-1f, false);
				agent.SetController(Agent.ControllerType.AI);
			}
            
			Main.agentToDice.Clear();



			foreach (var missile2 in _Missiles.Values)
			{
				var missile = missile2.missile;
				
				SpawnedItemEntity spawnedItem = missile2.spawnedItem;
				
				if (spawnedItem.GameEntity != null)
				{
					addMissileToMission(missile2);
					spawnedItem.GameEntity.Remove(81);					
					spawnedItem.OnSpawnedItemEntityRemoved();
				}
			}
			
		}
		public void addMissileToMission(ZaWarudoMissile missileItem)
		{
			
			Vec3 position = missileItem.position;
			Vec3 velocity = missileItem.velocity;
			MatrixFrame matrixFrame = missileItem.matrixFrame;
			matrixFrame.rotation.RotateAboutSide((float)System.Math.PI / 2f);
			var missile = missileItem.missile;
			Mission.Current.AddCustomMissile(missile.ShooterAgent, missile.Weapon, position, velocity, matrixFrame.rotation, velocity.Length, 1f, ((MBMissile)missile).GetHasRigidBody(), missile.MissionObjectToIgnore, getFreeMissileIndex());
		}
		void IStatus.Update()
        {
			if (Input.IsKeyPressed(InputKey.Q))
			{
				DoneEvent();
				return;
			}
			if (Mission.Current == null)
				return;			
			foreach (Agent agent in Mission.Current.Agents.ToArray())
            {
				if(agent.Velocity.Length >= 0.7f)
                {
					agent.MovementFlags |= Agent.MovementControlFlag.Backward;
					
                }
				else
                {
					agent.MovementFlags &= ~Agent.MovementControlFlag.MoveMask;
				}
			}
			
			foreach (var item2 in Mission.Current.Missiles.ToList())
			{
				_StopMissile(item2);
			}
		}

        private void _StopMissile(Mission.Missile item)
        {
			if (_Missiles.ContainsKey(item))
				return;
			item.Stop();
			var spawnedItem = (SpawnedItemEntity)Mission.Current.ActiveMissionObjects.ToList().Last();

			_Missiles.Add(item , new ZaWarudoMissile(item , spawnedItem));

		}

        public int getFreeMissileIndex()
		{
			int num = 0;
			foreach (var missile in _Missiles.Values)
			{
				if (((MBMissile)missile.missile).Index > num)
				{
					num = ((MBMissile)missile.missile).Index;
				}
			}
			foreach (var missile2 in Mission.Current.Missiles)
			{
				if (((MBMissile)missile2).Index > num)
				{
					num = ((MBMissile)missile2).Index;
				}
			}
			return num + 1;
		}
	}
}