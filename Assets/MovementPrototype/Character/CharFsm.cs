public class CharFsm : BaseFsm
{
    public CharController Character { get; protected set; }
    public CharFsm(CharController character) : base()
    {
        Character = character;
        StateLoader<CharFsm> loader = new StateLoader<CharFsm>();
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.DashStates");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.BlockStates");
        //loader.LoadStates(this, "Assets.MovementPrototype.Character.States.AttackStates");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.HoldAttackStates");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.MovementStates");
        Current = dict["MOVEMENT/LOCK"];
    }

    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        Current = dict["MOVEMENT/LOCK"];
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
    }
}