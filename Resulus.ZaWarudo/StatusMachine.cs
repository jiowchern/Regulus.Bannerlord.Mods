namespace Regulus.Utility
{
    public class StatusMachine
    {
        readonly System.Collections.Concurrent.ConcurrentQueue<IStatus> _StandBys;



        public IStatus Current { get; private set; }

        public StatusMachine()
        {
            _StandBys = new System.Collections.Concurrent.ConcurrentQueue<IStatus>();
        }

        public void Push(IStatus new_stage)
        {
            _StandBys.Enqueue(new_stage);
        }

        public bool Update()
        {
            _SetCurrent();
            _UpdateCurrent();

            return Current != null;
        }

        private void _SetCurrent()
        {
            IStatus stage;
            if (_StandBys.TryDequeue(out stage))
            {
                if (Current != null)
                {
                    Current.Leave();
                }

                stage.Enter();
                Current = stage;
            }
        }

        private void _UpdateCurrent()
        {
            if (Current != null)
            {
                Current.Update();
            }
        }

        public void Termination()
        {


            if (Current != null)
            {
                Current.Leave();
                Current = null;
            }
        }

        public void Empty()
        {
            Push(new EmptyStage());
        }
    }
}
