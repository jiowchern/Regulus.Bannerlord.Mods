using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.Mission;

namespace Regulus.ZaWarudo
{
    namespace Extersion
    {

        public static class MissileExtersion
        {
            public static void Stop(this Mission.Missile missile)
            {
                missile.Entity.SetMobility(TaleWorlds.Engine.GameEntity.Mobility.stationary);
                var globalFrame = missile.Entity.GetGlobalFrame();
                globalFrame.rotation.RotateAboutSide((float)(System.Math.PI / 2f));
                var zero = TaleWorlds.Library.Vec3.Zero;
                Mission.Current.HandleMissileCollisionReaction(((MBMissile)missile).Index, MissileCollisionReaction.Stick, globalFrame, missile.ShooterAgent, null, false, System.Convert.ToSByte(-1), null, zero, zero, -1);
                Mission.Current.RemoveMissileAsClient(missile.Index);
                HarmonyLib.Traverse.Create(Mission.Current).Method("RemoveMissileAtIndex", missile.Index).GetValue();
            }
        }
        public static class AgentExtersion
        {
            public static void SetController(this Agent agent, Agent.ControllerType type)
            {
                HarmonyLib.Traverse.Create(agent).Method("SetController", type).GetValue();
            }
        }
        
    }
}