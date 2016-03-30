using UnityEngine;
using System;
using System.Collections;

public class CWalk : CState {
    public CWalk(CFsm fsm) : base(fsm, CStateEnum.Walk) {
    }

    public override void PreUpdate() {
        if (input.dash) {
            fsm.ChangeState(CStateEnum.Dash);
            return;
        }
        if (input.move.vector.magnitude > 0.2f) {
            return;
        }
        if (velocity.magnitude < 0.2f) {
            fsm.ChangeState(CStateEnum.Idle);
        }
    }

    public float moveSpeed = 4f;
    float maxAcceleration = 2f;
    Vector3 velocity;

    public override void Update() {
        // Calculate how fast we should be moving
        var inputVelocity = input.move.vector * moveSpeed;
        // Calcualte the delta velocity
        var velocityChange = inputVelocity - velocity;
        velocityChange.y = 0;
        // Limit acceleration
        if (velocityChange.magnitude > maxAcceleration) {
            velocityChange = velocityChange.normalized * maxAcceleration;
        }
        velocity += velocityChange;
        rbody.MovePosition(position + (velocity * Time.fixedDeltaTime));

        character.Look();
    }

    public override void Enter() {
        velocity = Vector3.zero;
    }

    public override void Exit() {
        velocity = Vector3.zero;
    }
}