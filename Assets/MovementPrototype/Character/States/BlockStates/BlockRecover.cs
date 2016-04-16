using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockRecover : BlockState 
    {
        public BlockRecover(CFsm fsm) : base(fsm)
        {
            Name = "BLOCK/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.3f;
        }
    }
}
