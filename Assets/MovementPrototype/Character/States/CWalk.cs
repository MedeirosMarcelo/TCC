using UnityEngine;
using System;
using System.Collections;

namespace Assets.MovementPrototype.Character.States
{
    public class CWalk : CState
    {
        public CWalk(CFsm fsm) : base(fsm)
        {
            Name = "WALK";
        }

        public override void PreUpdate()
        {
            if (Input.dash)
            {
                Fsm.ChangeState("DASH");
                return;
            }
            if (Input.move.vector.magnitude > Input.deadZone)
            {
                return;
            }
            if (Rigidbody.velocity.magnitude < Input.deadZone)
            {
                Fsm.ChangeState("IDLE");
                return;
            }
        }

        public float moveSpeed = 4f;
        float maxAcceleration = 2f;
        Vector3 velocity;

        public override void Update()
        {
            // Calculate how fast we should be moving
            var inputVelocity = Input.move.vector * moveSpeed;
            // Calcualte the delta velocity
            var velocityChange = inputVelocity - velocity;
            velocityChange.y = 0;
            // Limit acceleration
            if (velocityChange.magnitude > maxAcceleration)
            {
                velocityChange = velocityChange.normalized * maxAcceleration;
            }
            velocity += velocityChange;
            Character.Move(Transform.position + (velocity * Time.fixedDeltaTime));

            Character.Look();
        }

        public override void Enter(StateTransitionEventArgs args)
        {
            velocity = Vector3.zero;
        }

        public override void Exit()
        {
            velocity = Vector3.zero;
        }
    }
}