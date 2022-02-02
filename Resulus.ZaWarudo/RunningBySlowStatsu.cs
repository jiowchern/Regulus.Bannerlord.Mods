using Regulus.Utility;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{
    internal class RunningBySlowStatsu : Regulus.Utility.IStatus
    {
        public event System.Action DoneEvent;

        public RunningBySlowStatsu()
        {
        }

        void IStatus.Enter()
        {
            var factor = 0.001f;
            Mission.Current.Scene.SlowMotionFactor = factor;
            Mission.Current.Scene.SlowMotionMode = true;

            Mission.Current.SetFastForwardingFromUI(true);
        }

        void IStatus.Leave()
        {
            Mission.Current.SetFastForwardingFromUI(false);
            Mission.Current.Scene.SlowMotionMode = false;
        }

        void IStatus.Update()
        {
            if (Input.IsKeyPressed(InputKey.Q))
            {
                DoneEvent();
                return;
            }
        }
    }
}