using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockHighWindUp : BlockState
    {
        const float speed = 1f;
        public BlockHighWindUp(CFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/WINDUP";
            nextState = "BLOCK/HIGH/SWING";
            totalTime = 0.1f;
        }
    }
}
