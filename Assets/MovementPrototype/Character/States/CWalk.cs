using UnityEngine;
using System;
using System.Collections;

namespace Assets.MovementPrototype.Character.States {
    public class CWalk : CState {
        public CWalk(CFsm fsm) : base(fsm) {
            Name = "WALK";
        }

        public override void PreUpdate() {
            if (Input.buffer.NextEventIs<InputEvent.Attack>()) {
                Input.buffer.Pop<InputEvent.Attack>();
                Fsm.ChangeState("ATTACK");
                return;
            }
            if (Input.buffer.NextEventIs<InputEvent.Block>()) {
                Input.buffer.Pop<InputEvent.Block>();
                Fsm.ChangeState("BLOCK");
                return;
            }
            if (Input.buffer.NextEventIs<InputEvent.Dash>()) {
                var evt = Input.buffer.Pop<InputEvent.Dash>();
                Debug.Log("evt.move=" + evt.move.vector);
                Fsm.ChangeState(new DashTransitionArgs(Name, "DASH", 0f, evt));
                return;
            }
            if (Input.move.isActive) {
                return;
            }
            if (Rigidbody.velocity.magnitude < minVelocity) {
                Fsm.ChangeState("IDLE");
                return;
            }
        }

        static readonly float minVelocity = 0.25f;
        float moveSpeed = 4f;
        float maxAcceleration = 2f;
        Vector3 velocity;

        public override void Update() {
            // Calculate how fast we should be moving
            var inputVelocity = Input.move.vector * moveSpeed;
            // Calcualte the delta velocity
            var velocityChange = inputVelocity - velocity;
            velocityChange.y = 0;
            // Limit acceleration
            if (velocityChange.magnitude > maxAcceleration) {
                velocityChange = velocityChange.normalized * maxAcceleration;
            }
            velocity += velocityChange;
            Character.Move(Transform.position + (velocity * Time.fixedDeltaTime));

            Character.Look();
        }

        public override void Enter(StateTransitionArgs args) {
            velocity = Vector3.zero;
        }

        public override void Exit(StateTransitionArgs args) {
            velocity = Vector3.zero;
        }
    }
}