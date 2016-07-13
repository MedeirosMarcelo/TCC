namespace Assets.Scripts.Character.States
{
    public class End : CharacterState
    {
        public End(CharacterFsm fsm) : base(fsm)
        {
            Name = "END";
            turnRate = 0f;
        }
    }
}