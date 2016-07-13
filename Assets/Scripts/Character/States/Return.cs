namespace Assets.Scripts.Character.States
{
    public class Return : CharacterState
    {
        public Return(CharacterFsm fsm) : base(fsm)
        {
            Name = "RETURN";
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
            Character.NavmeshAnimateWalk();

            if (Character.NavmeshReachedDestination())
            {
                Fsm.ChangeState("END");
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.Animator.CrossFade("MoveLow", 0.3f, -1, 0);
            Character.NavmeshMove(Character.SpawnPosition, updateRotation: false);
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.NavmeshStop();
        }
    }
}