using UnityEngine;

namespace Assets.MovementPrototype.Character.States.MovementStates
{
    public class MovementLock : MovementBase
    {
        float moveSpeed = 3.5f;

        public MovementLock(CharFsm fsm) : base(fsm)
        {
            Name = "MOVEMENT/LOCK";
        }

        public override void PreUpdate()
        {
            if (Character.input.run >= 0.25f)
            {
                Character.fsm.ChangeState("MOVEMENT/RUN");
            }
            else
            {
                base.PreUpdate();
            }
        }

        public override void FixedUpdate()
        {
            Character.Look(lookTurnRate, lockTurnRate);
            Move(moveSpeed);
        }

        public void Move(float speed = 4f)
        {
            if (Character.input.move.vector.magnitude > 0.25)
            {
                if (Input.move.isActive)
                {
                    // Calculate how fast we should be moving
                    var inputVelocity = Input.move.vector * speed;
                    // Calculate the delta velocity
                    var velocityChange = inputVelocity - velocity;
                    velocityChange.y = 0;
                    // Limit acceleration
                    if (velocityChange.magnitude > maxAcceleration)
                    {
                        velocityChange = velocityChange.normalized * maxAcceleration;
                    }
                    velocity += velocityChange;
                    Character.Move(Transform.position + (velocity * Time.fixedDeltaTime));
                }
            }
        }
    }
}