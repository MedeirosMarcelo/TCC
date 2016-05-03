namespace Assets.MovementPrototype.Character.States
{
    public class CharHitStun : AnimatedState
    {
        public CharHitStun(CharFsm fsm) : base(fsm)
        {
            Name = "HITSTUN";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            Animation = "Hit Stun";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Character.SwordTrail.Deactivate();
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }
    }
}