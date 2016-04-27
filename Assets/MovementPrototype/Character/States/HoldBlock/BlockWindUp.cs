namespace Assets.MovementPrototype.Character.States.HoldBlockStates
{
    public class BlockWindUp : CharState
    {
        public BlockWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/WINDUP";
            nextState = "BLOCK/SWING";
            totalTime = 0.1f;
            canPlayerMove = true;
            moveSpeed = 0.75f;
            turnRate = 0.25f;
        }
    }
}
