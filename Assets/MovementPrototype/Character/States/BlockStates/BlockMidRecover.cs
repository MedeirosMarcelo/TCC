using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockMidRecover : BlockState 
    {
        public BlockMidRecover(CFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
        }
    }
}
