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
        Start("MOVEMENT");
    }
    public override void ChangeState(string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
        UnityEngine.Debug.Log(string.Format("{0} >> {1}", Current.Name, nextStateName));
        base.ChangeState(nextStateName, additionalDeltaTime, args);
    }
}