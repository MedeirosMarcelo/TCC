public abstract class BlockBase : CharState
{
    bool Locked { get; set; }

    public BlockBase(CharFsm fsm) : base(fsm)
    {
        Locked = (Character.MovementState == "LOCK");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Locked)
        {
            Character.Look();
        }
    }
}