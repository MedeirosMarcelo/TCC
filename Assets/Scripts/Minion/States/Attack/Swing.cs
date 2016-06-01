namespace Assets.Scripts.Minion.States.Attack
{
    public class Swing : MinionState
    {
        TimerBehaviour timer;
        AnimationBehaviour animation;
        public Swing(MinionFsm fsm) : base(fsm)
        {
            Name = "ATTACK/SWING";
            staminaCost = 0.5f; // uses 5% of stamina each attack/swing
            timer = new TimerBehaviour(this);
            timer.TotalTime = 0.15f;
            timer.OnFinish = () => Fsm.ChangeState("ATTACK/RECOVER");

            animation = new AnimationBehaviour(this, Animator);
            animation.AnimationTime = 0.15f;
            animation.Animation = "LeftSwing";
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            UpdateDestination();
            Look();
        }
        void UpdateDestination()
        {
            Minion.SetDestination(Target.Transform.position, updateRotation: false);
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.SwordCollider.enabled = true;
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.SwordCollider.enabled = false;
        }
    }
}

