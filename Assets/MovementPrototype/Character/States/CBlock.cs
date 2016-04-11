using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CBlock : CState
    {
        public CBlock(CFsm fsm) : base(fsm)
        {
            Name = "BLOCK";
            nextState = "IDLE";
            totalTime = 0.5f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.swordAnimator.SetTrigger("Block Mid");
        }
    }
}