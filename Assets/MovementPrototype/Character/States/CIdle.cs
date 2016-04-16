using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CIdle : CState
    {
        public CIdle(CFsm fsm) : base(fsm, fsm.Character)
        {
            Name = "IDLE";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Character.ChangeVelocity(Vector3.zero);
            Character.animator.Play("Idle");
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }

        public override void PreUpdate()
        {
            if (Input.buffer.NextEventIs<InputEvent.Attack>())
            {
                var evt = Input.buffer.Pop<InputEvent.Attack>();
                Fsm.ChangeState("ATTACK", 0f, evt);
            }
            else if (Input.buffer.NextEventIs<InputEvent.BlockMid>())
            {
                Input.buffer.Pop<InputEvent.BlockMid>();
                Fsm.ChangeState("BLOCK/WINDUP");
            }
            else if (Input.buffer.NextEventIs<InputEvent.Dash>())
            {
                var evt = Input.buffer.Pop<InputEvent.Dash>();
                Fsm.ChangeState("DASH", 0f, evt);
            }
        }

        const float minVelocity = 0.25f;
        float moveSpeed = 4f;
        float maxAcceleration = 2f;
        Vector3 velocity;

        public override void FixedUpdate()
        {
            if (Input.move.isActive)
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
            }
            Character.Look();
            //Character.Run();
        }
    }
}