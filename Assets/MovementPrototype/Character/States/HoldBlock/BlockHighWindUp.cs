namespace Assets.MovementPrototype.Character.States.HoldBlockStates
{
    public class BlockHighWindUp : CharState
    {
        public BlockHighWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/WINDUP";
            nextState = "BLOCK/HIGH/SWING";
            totalTime = 0.1f;
            canPlayerMove = true;
            moveSpeed = 0.75f;
            turnRate = 0.25f;
        }
    }
}
