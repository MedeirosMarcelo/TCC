namespace Assets.Scripts.Character.States.Lock
{
    public class LockSwords : AnimatedState
    {
        public LockSwords(CharacterFsm fsm) : base(fsm)
        {
            Name = "LOCK/LOCKSWORDS";
            nextState = "LOCK/PUSHAWAY";
            totalTime = 0.5f;
            Animation = "LockSwords";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }
    }
}