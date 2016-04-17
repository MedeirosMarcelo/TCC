using UnityEngine;

public abstract class AttackSwing : AnimatedState
{
    public int Damage { get; protected set; }
    public float Speed { get; protected set; }
    public AttackSwing(CFsm fsm) : base(fsm)
    {
        Speed = 2f;
    }
    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        Character.SwordTrail.Activate();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Character.Look(0.25f, 0.15f);
        Character.Move(Transform.position + ((Transform.forward * Speed) * Time.fixedDeltaTime));
    }
}
