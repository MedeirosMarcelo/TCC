namespace Assets.Scripts.Character.States
{
    public class HitStun : AnimatedState
    {
        public HitStun(CharacterFsm fsm) : base(fsm)
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