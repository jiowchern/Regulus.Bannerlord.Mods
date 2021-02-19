using Regulus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Regulus.ZaWarudo
{
    internal class OnStatus : Regulus.Utility.IStatus
    {
        readonly Regulus.Utility.StatusMachine _Machine;
        public OnStatus()
        {
            _Machine = new StatusMachine();
        }
        public event System.Action DoneEvent;
        void IStatus.Enter()
        {
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
        }

        void IStatus.Update()
        {
            if (!Main.IsReady())
                DoneEvent();

            _Machine.Update();
        }
    }
}