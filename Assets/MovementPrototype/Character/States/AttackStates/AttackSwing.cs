using UnityEngine;

public abstract class AttackSwing : AnimatedState
{
    public int Damage { get; protected set; }
    public float Speed { get; protected set; }
    public SwordStance nextStance { get; protected set; }
    public AttackSwing(CharFsm fsm) : base(fsm)
    {
        Speed = 2f;
        nextStance = SwordStance.Right;
    }
    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        Character.SwordTrail.Activate();
        Character.AttackCollider.enabled = true;
    }

    public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
        Character.AttackCollider.enabled = false;
        Character.Stance = nextStance;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Character.Look(0.25f, 0.15f);
        Character.Move(Transform.position + ((Transform.forward * Speed) * Time.fixedDeltaTime));
    }
}
