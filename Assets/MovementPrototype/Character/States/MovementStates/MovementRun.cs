using UnityEngine;

namespace Assets.MovementPrototype.Character.States.MovementStates
{
    public class MovementRun : MovementBase
    {
        float moveSpeed = 6f;

        public MovementRun(CharFsm fsm) : base(fsm)
        {
            Name = "MOVEMENT/RUN";
        }

        public override void PreUpdate()
        {
            if (Character.input.run <= 0.25f)
            {
                Character.fsm.ChangeState("MOVEMENT/LOCK");
            }
            else
            {
                base.PreUpdate();
            }
        }

        public override void FixedUpdate()
        {
            if (Character.input.move.vector.magnitude > 0.25)
            {
                Character.LookForward(lookTurnRate * 2f);
            }
            MoveRun(moveSpeed);
        }

        public void MoveRun(float speed = 4f)
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