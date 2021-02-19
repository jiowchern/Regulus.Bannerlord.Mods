using Regulus.Utility;
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
        public event System.Action DoneEvent;

        public ReadyStatsu()
        {

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
            DoneEvent();
        }
        
      
    }
}