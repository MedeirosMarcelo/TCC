using UnityEngine.Assertions;

public class CharFsm : BaseFsm
{
    public CharController Character { get; protected set; }
    private string startStateName = "MOVEMENT";

    public CharFsm(CharController character) : base()
    {
        Character = character;
        StateLoader<CharFsm> loader = new StateLoader<CharFsm>();
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.DashStates");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.HoldAttackStates");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.HoldBlockStates");
        Assert.IsTrue(dict.ContainsKey(startStateName), "Unknown start state: " + startStateName);
        Current = dict[startStateName];
    }
}