using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockHighWindUp : BlockBase
    {
        public BlockHighWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/WINDUP";
            nextState = "BLOCK/HIGH/SWING";
            totalTime = 0.1f;
        }
    }
}
