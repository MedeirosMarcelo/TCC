namespace Assets.Scripts.Minion.States
{
    public class Return : MinionState
    {
        TimerBehaviour timer;
        public Return(MinionFsm fsm) : base(fsm)
        {
            Name = "RETURN";

            timer = new TimerBehaviour(this);
            timer.TotalTime = 5f;
            timer.OnFinish = () => Fsm.ChangeState("END");
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
            Minion.NavmeshAnimateWalk();
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Animator.CrossFade("MoveLow", 0.3f, -1, 0);
            Minion.NavmeshMove(Minion.SpawnPosition, updateRotation: false);
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.NavmeshStop();
        }

    }
}