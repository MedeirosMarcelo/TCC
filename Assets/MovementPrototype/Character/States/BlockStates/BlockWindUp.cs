using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockWindUp : BlockState
    {
        const float speed = 1f;
        public BlockWindUp(CFsm fsm) : base(fsm)
        {
            Name = "BLOCK/WINDUP";
            nextState = "BLOCK/SWING";
            totalTime = 0.1f;
        }
    }
}
