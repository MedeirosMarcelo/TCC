namespace Assets.Scripts.Character.States.Block
{
    public class BlockHighRecover : AnimatedState
    {
        public BlockHighRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.1f;
            canPlayerMove = true;
            moveSpeed = 0.5f;
            turnRate = 0.25f;
            Animation = "BlockHighRecover";
        }
    }
}
