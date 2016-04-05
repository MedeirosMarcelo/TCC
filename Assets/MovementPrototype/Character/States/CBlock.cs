using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CBlock : CState
    {
        public CBlock(CFsm fsm) : base(fsm)
        {
            Name = "BLOCK";
        }

        public override void Enter(StateTransitionArgs args)
        {
            Character.swordAnimator.SetTrigger("Block Mid");
        }

        public override void Exit(StateTransitionArgs args)
        {
        }

        public override void PreUpdate()
        {
        }

        public override void Update()
        {
        }
    }
}