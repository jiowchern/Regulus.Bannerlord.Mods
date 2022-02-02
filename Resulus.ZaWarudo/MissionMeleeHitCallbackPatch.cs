using HarmonyLib;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{


    class A
    {

    }
    class B: A
    {

    
    }
    class C : B { }
    [HarmonyPatch(typeof(Mission), "MeleeHitCallback")]
    class MissionMeleeHitCallbackPatch
    {
       
        //private void GetAttackCollisionResults(Agent attackerAgent, Agent victimAgent, GameEntity hitObject, float momentumRemaining, ref AttackCollisionData attackCollisionData, in MissionWeapon attackerWeapon, bool crushedThrough, bool cancelDamage, bool crushedThroughWithoutAgentCollision, out WeaponComponentData shieldOnBack)
        public static bool Prefix(ref AttackCollisionData collisionData, Agent attacker, Agent victim, GameEntity realHitEntity, ref float inOutMomentumRemaining, ref MeleeCollisionReaction colReaction, CrushThroughState crushThroughState, Vec3 blowDir, Vec3 swingDir, ref object hitParticleResultData, bool crushedThroughWithoutAgentCollision)
        {

            if (victim == null)
                return true;
            
            if (!victim.IsMainAgent )
            {
                return true;
            }

            _RegisterBlow(attacker);

            colReaction = MeleeCollisionReaction.Bounced;

            /*if (attacker.MountAgent != null)
            {
                _RegisterBlow(attacker.MountAgent);
                
            }*/

            return false;
        }

        private static void _RegisterBlow(Agent attacker)
        {
            var blow = new Blow(attacker.Index);
            if (attacker.MountAgent == null)
                blow.BlowFlag = BlowFlags.KnockBack | BlowFlags.KnockDown;
            else
                blow.BlowFlag = BlowFlags.CanDismount;

            blow.InflictedDamage = 1;
            blow.DamageCalculated = true;
            attacker.RegisterBlow(blow);            
        }
    }
}