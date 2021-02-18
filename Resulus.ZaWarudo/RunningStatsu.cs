using Regulus.Utility;
using TaleWorlds.MountAndBlade;
using Regulus.ZaWarudo.Extersion;
using TaleWorlds.Engine;
using TaleWorlds.DotNet;
using TaleWorlds.Library;
using TaleWorlds.InputSystem;
using System.Linq;
using System.Collections.Generic;

namespace Regulus.ZaWarudo
{

    internal class RunningStatsu :  Regulus.Utility.IStatus
    {
        private readonly ZaWarudoMissile[] _Missiles;
        

        public event System.Action DoneEvent;
        public RunningStatsu(ZaWarudoMissile[] missiles)
        {
            this._Missiles = missiles;
			

		}

        void IStatus.Enter()
        {			
			Main.Enable = true;   
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



			foreach (var missile2 in _Missiles)
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
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
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
			
			/*foreach (var item2 in Mission.Current.Missiles.ToList())
			{
				stopMissile(item2);
			}*/
		}

		public int getFreeMissileIndex()
		{
			int num = 0;
			foreach (var missile in _Missiles)
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