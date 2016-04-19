using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockHighRecover : BlockBase 
    {
        public BlockHighRecover(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
        }
    }
}
