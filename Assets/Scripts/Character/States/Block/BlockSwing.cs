namespace Assets.Scripts.Character.States.Block
{
    public abstract class BlockSwing : CharacterState
    {
        private TimerBehaviour timer;
        private bool holding;
        private bool locked;
        protected new string nextState;
        public BlockSwing(CharacterFsm fsm) : base(fsm)
        {
            canPlayerMove = true;
            moveSpeed = 0.75f;
            turnRate = 0.25f;

            timer = new TimerBehaviour(this);
            timer.TotalTime = 0.3f;
            timer.OnFinish = () => locked = false;
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (holding)
            {
                if (Character.input.block == false)
                {
                    holding = false;
                }
            }
            else
            {
                if (!locked)
                {
                    Fsm.ChangeState(nextState);
                }
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.BlockMidCollider.enabled = true;
            holding = true;
            locked = true;
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.BlockMidCollider.enabled = false;
        }
    }
}
