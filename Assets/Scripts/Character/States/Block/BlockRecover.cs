namespace Assets.Scripts.Character.States.Block
{
    public abstract class BlockRecover : CharacterState
    {
        public TimerBehaviour timer { get; protected set; }
        public BlockRecover(CharacterFsm fsm) : base(fsm)
        {
            canPlayerMove = true;
            moveSpeed = 0.5f;
            turnRate = 0.25f;

            timer = new TimerBehaviour(this);
        }
    }
}
