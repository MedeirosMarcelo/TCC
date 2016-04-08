public class DashFsm : BaseFsm, IState
{
    public DashFsm(CFsm fsm) : base(fsm)
    {
        var loader = new StateLoader<DashFsm>();
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.DashStates");
        Current = dict["ACCEL"];
    }

    public override void Enter(StateTransitionArgs args)
    {
        Current = dict["ACCEL"];
        Current.Enter(args);
    }
    public override void Exit(StateTransitionArgs args)
    {
        Current.Exit(args);
    }

    public override void PreUpdate()
    {
        if (Current.Name == "ENDED")
        {
            Fsm.ChangeState("IDLE");
        }
        else
        {
            base.PreUpdate();
        }
    }
}
