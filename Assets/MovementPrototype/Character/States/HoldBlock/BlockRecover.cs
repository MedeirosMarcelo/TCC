namespace Assets.MovementPrototype.Character.States.HoldBlockStates
{
    public class BlockRecover : CharState
    {
        public BlockRecover(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            canPlayerMove = true;
            moveSpeed = 0.5f;
            turnRate = 0.25f;
        }
    }
}
