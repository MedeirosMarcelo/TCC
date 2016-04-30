namespace Assets.MovementPrototype.Character.States.HoldBlockStates
{
    public class BlockMidRecover : CharState
    {
        public BlockMidRecover(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            canPlayerMove = true;
            moveSpeed = 0.5f;
            turnRate = 0.25f;
        }
    }
}
