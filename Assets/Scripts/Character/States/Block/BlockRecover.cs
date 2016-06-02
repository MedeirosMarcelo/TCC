namespace Assets.Scripts.Character.States.Block
{
    public class BlockRecover : CharacterState
    {
        public BlockRecover(CharacterFsm fsm) : base(fsm)
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
