using UnityEngine.Assertions;

public class MinionFsm : BaseFsm
{
    public MinionController Minion { get; protected set; }
    private string startStateName = "IDLE";

    public MinionFsm(MinionController minion) : base()
    {
        Minion = minion;
        var loader = new StateLoader<MinionFsm>();
        loader.LoadStates(this, "Assets.AIPrototype.States");
        Assert.IsTrue(dict.ContainsKey(startStateName), "Unknown start state: " + startStateName);
        Current = dict[startStateName];
        Current.Enter("", startStateName, 0f);
    }
    public override void ChangeState(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        UnityEngine.Debug.Log(string.Format("{0} >> {1}", lastStateName, nextStateName));
        base.ChangeState(lastStateName, nextStateName, additionalDeltaTime, args);
    }
}