using HarmonyLib;
using Regulus.ZaWarudo.Extersion;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{
    [HarmonyPatch(typeof(Mission), "MissileHitCallback")]
    class MissionMissileHitCallbackPatch
    {
        readonly static System.Collections.Concurrent.ConcurrentQueue<float> _AbsorbedDamages = new System.Collections.Concurrent.ConcurrentQueue<float>();
        static bool Prefix(Mission __instance, ref bool __result, out int hitParticleIndex, ref AttackCollisionData collisionData,
            Vec3 missileStartingPosition, Vec3 missilePosition, Vec3 missileAngularVelocity, Vec3 movementVelocity,
            MatrixFrame attachGlobalFrame, MatrixFrame affectedShieldGlobalFrame, int numDamagedAgents, Agent attacker, Agent victim,
            GameEntity hitEntity)
        {
            
            hitParticleIndex =-1;
            if (Agent.Main == null)
                return true;
            var mainAgent = Agent.Main;

            if (attacker == mainAgent)
            {
                if(collisionData.MissileTotalDamage < 5)
                {
                    
                    collisionData = AttackCollisionData.GetAttackCollisionDataForDebugPurpose(
                          collisionData.AttackBlockedWithShield,
                          collisionData.CorrectSideShieldBlock,
                          collisionData.IsAlternativeAttack,
                          collisionData.IsColliderAgent,
                          collisionData.CollidedWithShieldOnBack,
                          collisionData.IsMissile,
                          collisionData.MissileBlockedWithWeapon,
                          collisionData.MissileHasPhysics,
                          collisionData.EntityExists,
                          collisionData.ThrustTipHit,
                          collisionData.MissileGoneUnderWater,
                          collisionData.CollisionResult,
                          collisionData.AffectorWeaponSlotOrMissileIndex,
                          collisionData.StrikeType,
                          collisionData.DamageType,
                          collisionData.CollisionBoneIndex,
                          collisionData.VictimHitBodyPart, collisionData.AttackBoneIndex, collisionData.AttackDirection, collisionData.PhysicsMaterialIndex,
                          collisionData.CollisionHitResultFlags, collisionData.AttackProgress, collisionData.CollisionDistanceOnWeapon, collisionData.AttackerStunPeriod,
                          collisionData.DefenderStunPeriod, _GetRangeWeaponDamage(attacker , victim), collisionData.MissileStartingBaseSpeed, collisionData.ChargeVelocity, collisionData.FallSpeed,
                          collisionData.WeaponRotUp, collisionData.WeaponBlowDir, collisionData.CollisionGlobalPosition, collisionData.MissileVelocity,
                          collisionData.MissileStartingPosition, collisionData.VictimAgentCurVelocity, collisionData.CollisionGlobalNormal
                        );
                        
                }
                __result = true;
                return true;
            }

            if (victim != mainAgent)
                return true;
            
            if (victim == attacker)
            {
                return false;
            }
            FieldInfo missileField = AccessTools.Field(typeof(Mission), "_missiles");
            var missiles = (System.Collections.Generic.Dictionary<int, Mission.Missile>)missileField.GetValue(__instance);
            var missile = missiles[collisionData.AffectorWeaponSlotOrMissileIndex];

            var pos = missile.GetPosition();
            var velocity = missile.GetVelocity() ;
            var matrixFrame = missile.Entity.GetGlobalFrame();
            
            missile.Stop();
            var spawnedItem = (SpawnedItemEntity)Mission.Current.ActiveMissionObjects.ToList().Last();
            
            velocity *= -1f;
            matrixFrame.rotation.RotateAboutSide((float)System.Math.PI / 2f);

            //collisionData.AffectorWeaponSlotOrMissileIndex
            Mission.Current.AddCustomMissile(victim, missile.Weapon, pos, velocity, matrixFrame.rotation, velocity.Length, 1f, missile.GetHasRigidBody(), missile.MissionObjectToIgnore  );

            spawnedItem.GameEntity.Remove(81);
            spawnedItem.OnSpawnedItemEntityRemoved();
            _AbsorbedDamages.Enqueue(collisionData.MissileTotalDamage);
            __result = false;
            return false;
        }

        private static float _GetRangeWeaponDamage(Agent attacker, Agent victim)
        {
            float damage;
            if (_AbsorbedDamages.TryDequeue(out damage))
            {
                return damage;
            }
            return 999;
        }
    }
}