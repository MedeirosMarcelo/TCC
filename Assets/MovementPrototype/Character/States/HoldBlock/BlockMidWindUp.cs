namespace Assets.MovementPrototype.Character.States.HoldBlockStates
{
    public class BlockMidWindUp : AnimatedState
    {
        public BlockMidWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/WINDUP";
            nextState = "BLOCK/MID/SWING";
            totalTime = 0.1f;
            canPlayerMove = true;
            moveSpeed = 0.75f;
            turnRate = 0.25f;
            Animation = "BlockMidWindup";
        }
    }
}
