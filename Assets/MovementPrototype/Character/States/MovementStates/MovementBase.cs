using UnityEngine;

public abstract class MovementBase : CharState
{
    public float maxAcceleration = 2f;
    public float lookTurnRate = 1f;
    public float lockTurnRate = 1f;
    public Vector3 velocity;

    public MovementBase(CharFsm fsm) : base(fsm)
    {
    }

    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        Character.ChangeVelocity(Vector3.zero);
        Character.animator.Play("Idle");
        Character.MovementState = Name;
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
    }

    public override void PreUpdate()
    {
        if (Input.buffer.NextEventIs<InputEvent.Attack>())
        {
            var evt = Input.buffer.Pop<InputEvent.Attack>();
            Fsm.ChangeState("HATTACK", 0f, evt);
        }
        else if (Input.buffer.NextEventIs<InputEvent.Lunge>())
        {
            var evt = Input.buffer.Pop<InputEvent.Lunge>();
            Fsm.ChangeState("LUNGE/LIGHT/WINDUP", 0f, evt);
        }
        else if (Input.buffer.NextEventIs<InputEvent.Block>())
        {
            var evt = Input.buffer.Pop<InputEvent.Block>();
            Fsm.ChangeState("BLOCK", 0f, evt);
        }
        else if (Input.buffer.NextEventIs<InputEvent.Dash>())
        {
            var evt = Input.buffer.Pop<InputEvent.Dash>();
            Fsm.ChangeState("DASH", 0f, evt);
        }
    }
}