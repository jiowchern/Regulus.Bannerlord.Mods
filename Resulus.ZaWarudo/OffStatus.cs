using Regulus.Utility;

namespace Regulus.ZaWarudo
{
    internal class OffStatus : Regulus.Utility.IStatus
    {
        public OffStatus()
        {
        }
        public event System.Action DoneEvent;

        void IStatus.Enter()
        {
            
        }

        void IStatus.Leave()
        {
            
        }

        void IStatus.Update()
        {
            if (Main.IsReady())
                DoneEvent();
        }
    }
}