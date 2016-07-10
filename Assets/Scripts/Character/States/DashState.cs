using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Character.States
{
    using InputEvent = Input.InputEvent;
    public class DashState : CharacterState
    {
        private TimerBehaviour timer;
        private AnimationBehaviour animation;
        private Vector3 baseVelocity;

        private const float maxSpeed = 9f;
        private const float t_accel = 0.08f;
        private const float t_plateau = 0.08f;
        private const float t_deccel = 0.08f;
        private const float t_recover = 0.2f;

        public DashState(CharacterFsm fsm) : base(fsm)
        {
            Name = "DASH";
            turnRate = 0f;

            timer = new TimerBehaviour(this);
            timer.TotalTime = t_accel + t_plateau + t_deccel + t_recover;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");

            animation = new AnimationBehaviour(this, Character.Animator);
            animation.Name = "DashForward";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Assert.IsTrue(args.Length == 1);
            var evt = args[0] as InputEvent.Dash;
            Assert.IsNotNull(evt);
            SetDashAnimation(evt);
            baseVelocity = GetDashVelocity(evt);
            AudioManager.Play(ClipType.Dash, Character.Audio);
        }

        Vector3 GetDashVelocity(InputEvent.Dash evt)
        {
            Vector3 dashVelocity = evt.Move.vector.normalized;
            if (dashVelocity == Vector3.zero) dashVelocity = Character.transform.forward;
            return dashVelocity * maxSpeed;
        }

        void SetDashAnimation(InputEvent.Dash evt)
        {
            var dashVelocity = Transform.InverseTransformDirection(evt.Move.vector.normalized);
            if (Mathf.Abs(dashVelocity.z) > Mathf.Abs(dashVelocity.x))
            {
                if (dashVelocity.z > 0)
                {
                    animation.Name = "DashForward";
                }
                else
                {
                    animation.Name = "DashBack";
                }
            }
            else
            {
                if (dashVelocity.x > 0)
                {
                    animation.Name = "DashRight";
                }
                else
                {
                    animation.Name = "DashLeft";
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector3 velocity = baseVelocity * TrapezoidalFunction(timer.Elapsed);
            Character.Move(Transform.position + (velocity * Time.fixedDeltaTime));

            float recoverStart = t_accel + t_plateau + t_deccel;
            if (timer.Elapsed > recoverStart)
            {
                if (Input.buffer.NextEventIs<InputEvent.Attack>())
                {
                    var evt = Input.buffer.Pop<InputEvent.Attack>();
                    Fsm.ChangeState("ATTACK", 0f, evt);
                }
                else if (Input.buffer.NextEventIs<InputEvent.Block>())
                {
                    if (Character.Stance == SwordStance.High)
                    {
                        var evt = Input.buffer.Pop<InputEvent.Block>();
                        Fsm.ChangeState("BLOCK/HIGH/WINDUP", 0f, evt);
                    }
                    else
                    {
                        var evt = Input.buffer.Pop<InputEvent.Block>();
                        Fsm.ChangeState("BLOCK/MID/WINDUP", 0f, evt);
                    }
                }
            }
        }

        static private float TrapezoidalFunction(float time)
        {
            // This is the curve of dash velocity, a Trapezoidal Function
            // 1 |   .----.
            //   |  /      \
            //   | /        \
            //   +-------------> t
            //     a  b  c  d
            const float alpha = 0f;
            const float beta = alpha + t_accel;
            const float delta = beta + t_plateau;
            const float gama = delta + t_deccel;

            if (time < alpha) //  t <= a
            {
                return 0f;
            }
            else if (time < beta) // a < t < b
            {
                return (time - alpha) / (beta - alpha);
            }
            else if (time <= gama) // b <= t <= c
            {
                return 1f;
            }
            else if (time < delta) // c < t < d
            {
                return (delta - time) / (delta - gama);
            }
            else // t >= d
            {
                return 0f;
            }
        }
    }
}