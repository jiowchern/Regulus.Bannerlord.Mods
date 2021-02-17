using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace Regulus.ZaWarudo
{
    public class Main : TaleWorlds.MountAndBlade.MBSubModuleBase
    {

        readonly Regulus.Utility.StatusMachine _Machine;
        public Main()
        {
            _Machine = new Regulus.Utility.StatusMachine();
        }
        protected override void OnSubModuleLoad()
        {

            new HarmonyLib.Harmony("regulus.zawarudo");

            _ToOff();   
        }

        private void _ToOff()
        {
            var status = new OffStatus();
            status.DoneEvent += _ToOn;
            _Machine.Push(status);
        }

        private void _ToOn()
        {
            var status = new OnStatus();
            status.DoneEvent += _ToOff;
            _Machine.Push(status);
        }

        protected override void OnApplicationTick(float dt)
        {

            _Machine.Update();
        

            
        }

        public  static  bool IsReady()
        {
            if (Game.Current== null || Game.Current.CurrentState != Game.State.Running || Mission.Current == null || !(Mission.Current.Scene != null) || Mission.Current.MainAgent == null || Mission.Current.Time == 0f)
            {
                return false;
            }
            return true;
        }

        public override void OnGameEnd(TaleWorlds.Core.Game game)
        {
            
        }
    }
}
