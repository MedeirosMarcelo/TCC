using UnityEngine;

public class CIdle : CState
{
    public CIdle(CController character) : base(character)
    {
        Name = "IDLE";
    }

    public override void Enter(StateTransitionEventArgs args)
    {
        Character.ChangeVelocity(Vector3.zero);
    }

    public override void Exit()
    {
        Character.ChangeVelocity(Vector3.zero);
    }

    public override void PreUpdate()
    {
        if (Character.input.dash)
        {
            ChangeState("DASH");
        }
        else if (Character.input.move.vector.magnitude > Character.input.deadZone)
        {
            ChangeState("WALK");
        }
    }

    public override void Update()
    {
        Character.Look();
    }
}
