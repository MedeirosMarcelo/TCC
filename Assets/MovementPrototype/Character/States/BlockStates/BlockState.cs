using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public abstract class BlockState : CharState
    {
        string previousState;

        public BlockState(CharFsm fsm) : base(fsm)
        {
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            previousState = lastStateName;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (previousState == "LOCK")
            {
                Character.Look();
            }
        }
    }
}