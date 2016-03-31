using UnityEngine;
using System;
using System.Collections;

public class CWalk : CState {
    public CWalk(CController character) : base(character) {
        Name = "WALK";
    }

    public override void PreUpdate() {
        if (Character.input.dash) {
            ChangeState("DASH");
            return;
        }
        if (Character.input.move.vector.magnitude > 0.2f) {
            return;
        }
        if (Character.rbody.velocity.magnitude < 0.2f) {
            ChangeState("IDLE");
            return;
        }
    }

    public float moveSpeed = 4f;
    float maxAcceleration = 2f;
    Vector3 velocity;

    public override void Update() {
        // Calculate how fast we should be moving
        var inputVelocity = Character.input.move.vector * moveSpeed;
        // Calcualte the delta velocity
        var velocityChange = inputVelocity - velocity;
        velocityChange.y = 0;
        // Limit acceleration
        if (velocityChange.magnitude > maxAcceleration) {
            velocityChange = velocityChange.normalized * maxAcceleration;
        }
        velocity += velocityChange;
        Character.Move(Character.transform.position + (velocity * Time.fixedDeltaTime));

        Character.Look();
    }

    public override void Enter(StateTransitionEventArgs args) {
        velocity = Vector3.zero;
    }

    public override void Exit() {
        velocity = Vector3.zero;
    }
}