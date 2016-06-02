namespace Assets.Scripts.Character.States.Block
{
    public class BlockMidWindUp : AnimatedState
    {
        public BlockMidWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/WINDUP";
            nextState = "BLOCK/MID/SWING";
            totalTime = 0.001f;
            canPlayerMove = true;
            moveSpeed = 0.75f;
            turnRate = 0.25f;
            Animation = "BlockMidWindup";
        }
    }
}
