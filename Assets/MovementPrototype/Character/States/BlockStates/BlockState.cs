using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public abstract class BlockState : CState
    {
        string previousState;

        public BlockState(CFsm fsm) : base(fsm, fsm.Character)
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