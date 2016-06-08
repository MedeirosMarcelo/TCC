namespace Assets.Scripts.Character.States.Block
{
    public abstract class BlockWindUp : CharacterState 
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        public BlockWindUp(CharacterFsm fsm) : base(fsm)
        {
            canPlayerMove = true;
            moveSpeed = 0.75f;
            turnRate = 0.25f;

            timer = new TimerBehaviour(this);
            animation = new AnimationBehaviour(this, Character.animator);
        }
    }
}
