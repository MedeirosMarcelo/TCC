using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockMidWindUp : BlockBase
    {
        const float speed = 1f;
        public BlockMidWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/WINDUP";
            nextState = "BLOCK/MID/SWING";
            totalTime = 0.1f;
        }
    }
}
