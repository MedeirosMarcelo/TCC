namespace Assets.Scripts.Character.States.Block
{
    public class BlockWindUp : CharacterState
    {
        public BlockWindUp(CharacterFsm fsm) : base(fsm)
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
