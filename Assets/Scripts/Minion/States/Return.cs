namespace Assets.Scripts.Minion.States
{
    public class Return : MinionState
    {
        public Return(MinionFsm fsm) : base(fsm)
        {
            Name = "RETURN";
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
            if (Minion.NavmeshReachedDestination())
            {
                Fsm.ChangeState("END");
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Animator.CrossFade("MoveLow", 0.3f, -1, 0);
            Minion.NavmeshMove(Minion.SpawnPosition, updateRotation: false);
        }
    }
}