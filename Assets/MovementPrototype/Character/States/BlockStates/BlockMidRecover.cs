using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockMidRecover : BlockBase 
    {
        public BlockMidRecover(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
        }
    }
}
