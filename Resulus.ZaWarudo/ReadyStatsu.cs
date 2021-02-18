﻿using Regulus.Utility;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using Regulus.ZaWarudo.Extersion;
using System.Linq;
using static TaleWorlds.MountAndBlade.Mission;

namespace Regulus.ZaWarudo
{
    namespace Extersion
    {

    }
    internal class ReadyStatsu : Regulus.Utility.IStatus
    {
        public event System.Action<System.Collections.Generic.IEnumerable<ZaWarudoMissile>> DoneEvent;

        readonly System.Collections.Generic.List<ZaWarudoMissile> _Missiles;
        public ReadyStatsu()
        {
            _Missiles = new System.Collections.Generic.List<ZaWarudoMissile>();
        }

        void IStatus.Enter()
        {
            
        }

        void IStatus.Leave()
        {
            
        }

        void IStatus.Update()
        {
            if (!Input.IsKeyPressed( InputKey.Q))
            {                
                return;
            }
            var mainAgent = Mission.Current.MainAgent;
            foreach (var agent in Mission.Current.Agents)
            {
                if (agent == mainAgent)
                    continue;
                agent.SetMaximumSpeedLimit(0.01f, false);
                agent.SetController(Agent.ControllerType.None);
                agent.MovementFlags &= ~Agent.MovementControlFlag.MoveMask;


                //agent.MovementInputVector = TaleWorlds.Library.Vec2.Zero;                
                /*if (agent.MountAgent != null)
                {
                    agent.MountAgent.SetMaximumSpeedLimit(0.01f, false);
                    agent.MountAgent.SetController(Agent.ControllerType.None);
                    agent.MountAgent.MovementFlags = (Agent.MovementControlFlag)0;
                    agent.MountAgent.SetMovementDirection(ref zero);
                }*/
                /*if (agent.MountAgent == null && (!agent.IsMount || agent.RiderAgent != null) )
                {
                    
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -2);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -3);
                }else if(horsemanWillStop(agent))
                {                    
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -33);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -17);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -3);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -2);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -9);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -5);
                }
                else
                {
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -33);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -17);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -2);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -9);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags & -5);
                    agent.MovementFlags = (Agent.MovementControlFlag)((uint)agent.MovementFlags | 2);
                }*/


            }
            foreach (Mission.Missile item in Mission.Current.Missiles.ToList())
            {
                stopMissile(item);
            }
            DoneEvent(_Missiles);
        }
        public void stopMissile(Mission.Missile missile)
        {
            missile.Stop();
            /*missile.Entity.SetMobility(TaleWorlds.Engine.GameEntity.Mobility.stationary);            
            var globalFrame = missile.Entity.GetGlobalFrame();
            globalFrame.rotation.RotateAboutSide((float)(System.Math.PI / 2f));            
            var zero = TaleWorlds.Library.Vec3.Zero;
            Mission.Current.HandleMissileCollisionReaction(((MBMissile)missile).Index, MissileCollisionReaction.Stick , globalFrame, missile.ShooterAgent, null, false, System.Convert.ToSByte(-1), null, zero, zero, -1);
            Mission.Current.RemoveMissileAsClient(missile.Index);
            HarmonyLib.Traverse.Create(Mission.Current).Method("RemoveMissileAtIndex", missile.Index).GetValue();
            */



            var spawnedItem = (SpawnedItemEntity)Mission.Current.ActiveMissionObjects.ToList().Last();
            _Missiles.Add(new ZaWarudoMissile(missile, spawnedItem));
        }
        public bool horsemanWillStop(Agent agent)
        {
            
            var velocity = agent.Velocity;
            return velocity.Length <= 0.7f;
        }
    }
}