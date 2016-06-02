namespace Assets.Scripts.Character.States.Block
{
    public class BlockMidRecover : AnimatedState
    {
        public BlockMidRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.1f;
            canPlayerMove = true;
            moveSpeed = 0.5f;
            turnRate = 0.25f;
            Animation = "BlockMidRecover";
        }
    }
}
