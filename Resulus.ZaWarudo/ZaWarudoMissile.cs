using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.Mission;

namespace Regulus.ZaWarudo
{
    public class ZaWarudoMissile
    {
        public Missile missile;

        public SpawnedItemEntity spawnedItem;

        public MissionWeapon missionWeapon;

        public TaleWorlds.Library.Vec3 position;

        public TaleWorlds.Library.Vec3 velocity;

        public TaleWorlds.Library.MatrixFrame matrixFrame;

        public ZaWarudoMissile(Missile missile, SpawnedItemEntity spawnedItem)
        {
            //IL_0018: Unknown result type (might be due to invalid IL or missing references)
            //IL_001d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0024: Unknown result type (might be due to invalid IL or missing references)
            //IL_0029: Unknown result type (might be due to invalid IL or missing references)
            //IL_0035: Unknown result type (might be due to invalid IL or missing references)
            //IL_003a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0054: Unknown result type (might be due to invalid IL or missing references)
            //IL_0059: Unknown result type (might be due to invalid IL or missing references)
            this.missile = missile;
            this.spawnedItem = spawnedItem;
            position = ((MBMissile)missile).GetPosition();
            velocity = ((MBMissile)missile).GetVelocity();
            matrixFrame = missile.Entity.GetGlobalFrame();
            if (missile.ShooterAgent != null)
            {
                missionWeapon = missile.ShooterAgent.WieldedWeapon;
            }
        }
    }
}