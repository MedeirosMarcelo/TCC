public class CharFsm : BaseFsm
{
    public CharController Character { get; protected set; }
    public CharFsm(CharController character) : base()
    {
        Character = character;
        StateLoader<CharFsm> loader = new StateLoader<CharFsm>();
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.DashStates");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.HoldAttackStates");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.HoldBlockStates");
        Fsm.Start("MOVEMENT");
    }
}