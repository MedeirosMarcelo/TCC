namespace Assets.MovementPrototype.Character.States.HoldBlockStates
{
    public class BlockHighRecover : CharState
    {
        public BlockHighRecover(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            canPlayerMove = true;
            moveSpeed = 0.5f;
            turnRate = 0.25f;
        }
    }
}
