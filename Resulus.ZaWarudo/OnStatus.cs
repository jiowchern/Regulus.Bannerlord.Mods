using Regulus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{
    internal class OnStatus : Regulus.Utility.IStatus
    {
        readonly Regulus.Utility.StatusMachine _Machine;
        private readonly AgentPropertiesSave _Propertys;
        public OnStatus()
        {
            _Machine = new StatusMachine();
            _Propertys = new AgentPropertiesSave(Agent.Main);
        }
        public event System.Action DoneEvent;
        void IStatus.Enter()
        {
            AgentPropertiesSave agentPropertiesSave = _Propertys;
            var item = Agent.Main;
            
            item.AgentDrivenProperties.MaxSpeedMultiplier *= 1.2f;
            item.AgentDrivenProperties.MountSpeed *= 1.2f;
            item.AgentDrivenProperties.MountChargeDamage *= 2f;
            item.AgentDrivenProperties.MountManeuver *= 2f;
            item.AgentDrivenProperties.CombatMaxSpeedMultiplier *= 2f;
            item.AgentDrivenProperties.SwingSpeedMultiplier *= 3f;
            item.AgentDrivenProperties.HandlingMultiplier *= 2f;
            item.AgentDrivenProperties.ThrustOrRangedReadySpeedMultiplier *= 2f;
            item.AgentDrivenProperties.ShieldBashStunDurationMultiplier *= 2f;
            item.AgentDrivenProperties.KickStunDurationMultiplier *= 2f;
            item.AgentDrivenProperties.AttributeShieldMissileCollisionBodySizeAdder *= 2f;
            item.AgentDrivenProperties.BipedalRangedReadySpeedMultiplier *= 3f;
            item.AgentDrivenProperties.BipedalRangedReloadSpeedMultiplier *= 3f;
            item.AgentDrivenProperties.ReloadSpeed *= 3f;
            item.AgentDrivenProperties.WeaponBestAccuracyWaitTime = 0f;
            item.AgentDrivenProperties.WeaponMaxMovementAccuracyPenalty = 0f;
            item.AgentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty = 0f;
            item.AgentDrivenProperties.WeaponInaccuracy = 0f;
            item.AgentDrivenProperties.LongestRangedWeaponInaccuracy = 0f;
            item.AgentDrivenProperties.ReloadMovementPenaltyFactor = 0f;
            item.AgentDrivenProperties.WeaponRotationalAccuracyPenaltyInRadians = 0f;
            item.AgentDrivenProperties.AttributeCourage *= 2f;
            item.AgentDrivenProperties.AttributeRiding *= 2f;
            item.AgentDrivenProperties.AttributeHorseArchery *= 2f;
            item.AgentDrivenProperties.AttributeShield *= 2f;
            item.AgentDrivenProperties.AiShootFreq *= 2f;
            item.AgentDrivenProperties.AiWaitBeforeShootFactor = 0f;
            //item.SetMorale(item.GetMorale() + 50f);
            item.UpdateCustomDrivenProperties();
            agentPropertiesSave.saveNewProperties();


            _ToReady();
        }

        private void _ToReady()
        {
            var status = new ReadyStatsu();
            status.DoneEvent += _ToRunningStatus;
            _Machine.Push(status);
        }

        private void _ToRunningStatus()
        {
            var status = new RunningStatsu();
            status.DoneEvent += _ToReady;
            _Machine.Push(status);
        }

        void IStatus.Leave()
        {
            _Machine.Termination();

            if(Mission.Current!=null)
            {
                //_Propertys.restoreProperties();
            }
            
        }

        void IStatus.Update()
        {
            if (!Main.IsReady())
                DoneEvent();

            _Machine.Update();
            _Propertys.tick();
        }
    }
}