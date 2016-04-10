using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CBlock : CTimedState
    {
        public CBlock(CFsm fsm) : base(fsm)
        {
            Name = "BLOCK";
            nextState = "IDLE";
            totalTime = 0.5f;
        }

        public override void Enter(StateTransitionArgs args)
        {
            base.Enter(args);
            Character.swordAnimator.SetTrigger("Block Mid");
        }
    }
}