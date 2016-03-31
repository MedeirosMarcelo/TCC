using UnityEngine;
using System;
using System.Collections;

namespace Assets.MovementPrototype.Character.States
{
    public class CWalk : CState
    {
        public CWalk(BaseFsm fsm) : base(fsm)
        {
            Name = "WALK";
        }

        public override void PreUpdate()
        {
            if (FsmPlayer.Character.input.dash)
            {
                ChangeState("DASH");
                return;
            }
            if (FsmPlayer.Character.input.move.vector.magnitude > 0.2f)
            {
                return;
            }
            if (FsmPlayer.Character.rbody.velocity.magnitude < 0.2f)
            {
                ChangeState("IDLE");
                return;
            }
        }

        public float moveSpeed = 4f;
        float maxAcceleration = 2f;
        Vector3 velocity;

        public override void Update()
        {
            // Calculate how fast we should be moving
            var inputVelocity = FsmPlayer.Character.input.move.vector * moveSpeed;
            // Calcualte the delta velocity
            var velocityChange = inputVelocity - velocity;
            velocityChange.y = 0;
            // Limit acceleration
            if (velocityChange.magnitude > maxAcceleration)
            {
                velocityChange = velocityChange.normalized * maxAcceleration;
            }
            velocity += velocityChange;
            FsmPlayer.Character.Move(FsmPlayer.Character.transform.position + (velocity * Time.fixedDeltaTime));

            FsmPlayer.Character.Look();
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