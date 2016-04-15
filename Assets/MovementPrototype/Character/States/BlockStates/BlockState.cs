using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public abstract class BlockState : CState
    {
        public BlockState(CFsm fsm) : base(fsm, fsm.Character)
        {
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Look();
        }
    }
}