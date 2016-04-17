using UnityEngine;

public abstract class AttackWindUp : AnimatedState
{
    public float Speed { get; protected set; }
    public AttackWindUp(CFsm fsm) : base(fsm)
    {
        Speed = 1f;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Character.Look(0.25f, 0.15f);
        Character.Move(Transform.position + ((Transform.forward * Speed) * Time.fixedDeltaTime));
    }
}
