using UnityEngine;
using Assets.Scripts.Common;

namespace Assets.Scripts.Character
{
    using GameManager = Game.GameManager;
    using BaseInput = Input.BaseInput;

    public abstract class CharacterState : BaseState
    {
        public CharacterController Character { get; protected set; }
        public GameManager Game { get; protected set; }
        public BaseInput Input { get; protected set; }
        public Rigidbody Rigidbody { get; protected set; }
        public Transform Transform { get; protected set; }
        public ITargetable Target { get { return Character.Target; } protected set { Character.Target = value; } }

        // Timed state
        protected float elapsed;
        protected float totalTime;
        protected string nextState;

        // Movement state
        protected bool canPlayerMove;
        protected float moveSpeed;
        protected float runSpeedModifier;
        protected float forwardSpeedModifier;
        protected float backwardSpeedModifier;
        protected float sideSpeedModifier;
        protected float maxAcceleration;
        protected float runMaxAcceleration;
        protected float turnRate;
        protected float runTurnModifier;
        protected float lockedTurnModifier;
        protected float defenseAngle;

        const float runThreshold = 0.35f;

        public CharacterState(CharacterFsm fsm)
        {
            Fsm = fsm;
            Character = fsm.Character;
            Input = Character.input;
            Rigidbody = Character.rbody;
            Transform = Character.transform;

            //Movement
            canPlayerMove = false;
            moveSpeed = 2.5f;
            runSpeedModifier = 1.25f;
            forwardSpeedModifier = 0.7f;
            backwardSpeedModifier = 0.35f;
            sideSpeedModifier = 0.75f;
            maxAcceleration = 2f;
            runMaxAcceleration = 2f;
            turnRate = 2f;
            runTurnModifier = 0.25f;
            lockedTurnModifier = 0.5f;
            defenseAngle = 90f;
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (totalTime > 0f && elapsed >= totalTime)
            {
                Fsm.ChangeState(nextState, totalTime - elapsed);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            elapsed += Time.fixedDeltaTime;
            Look();
            PlayerMove();
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            elapsed = additionalDeltaTime;
        }
        public override void OnTriggerEnter(Collider collider)
        {
            IAttack attack;
            if (collider.IsAttack(out attack))
            {

                if (attack != null && attack.CanHit(Character))
                {
                    Character.ReceiveDamage(attack.Damage);
                    Character.bloodAnimator.SetTrigger("Bleed");
                    Fsm.ChangeState("HITSTUN");
                }
            }
            else if ((collider.tag == "Push") && Fsm.Current.Name != "LOCK/LOCKSWORDS" && Fsm.Current.Name != "LOCK/PUSHAWAY")
            {
                Fsm.ChangeState("LOCK/LOCKSWORDS", 0, collider);
            }
            // otherwise defer to base
            base.OnTriggerEnter(collider);
        }

        public virtual void Look()
        {
            const float turnUnit = Mathf.PI / 30; // 12'
            if (turnRate > 0f)
            {
                if (Input.run > runThreshold)
                {
                    if (Input.move.isActive)
                    {
                        // Running
                        var forward = Vector3.RotateTowards(
                            Transform.forward,
                            Input.move.vector.normalized,
                            turnUnit * turnRate * runTurnModifier,
                            1f);
                        Character.Forward(forward);
                    }
                }
                else if (lockedTurnModifier > 0f)
                {

                    if (Input.look.isActive)
                    {
                        var vec = Input.look.vector;
                        var forward = Vector3.RotateTowards(
                            Transform.forward,
                            vec,
                            turnUnit * turnRate,
                            1f);
                        Character.Forward(forward);
                        Character.ChangeTarget(vec);
                    }
                    else
                    {
                        var forward = Vector3.RotateTowards(
                            Transform.forward,
                            (Target.Transform.position - Transform.position).normalized,
                            turnUnit * turnRate * lockedTurnModifier,
                            1f);
                        forward.y = 0f;
                        Character.Forward(forward);
                    }
                }
            }
        }

        void PlayerMove()
        {
            if (canPlayerMove)
            {
                if (Input.move.isActive)
                {
                    // Calculate the delta velocity
                    var acceleration = GetInputVelovity() - Rigidbody.velocity;
                    acceleration.y = 0;


                    // Limit acceleration
                    if (acceleration.magnitude > maxAcceleration)
                    {
                        acceleration = acceleration.normalized * maxAcceleration;
                    }
                    Rigidbody.velocity += acceleration;
                    Character.Move(Transform.position + (Rigidbody.velocity * Time.fixedDeltaTime));
                }
                else
                {
                    // If input is none stop character;
                    Character.Move(Transform.position);
                }
            }
        }

        Vector3 GetInputVelovity()
        {
            var velocity = Transform.InverseTransformDirection(Input.move.vector);
            // Now velocity is relative to the character so X is side to side and Z is front/back
            velocity.x *= moveSpeed * sideSpeedModifier;
            velocity.z *= moveSpeed *
                ((Mathf.Sign(velocity.z) > 0) ? forwardSpeedModifier : backwardSpeedModifier);

            if (Input.run > runThreshold)
            {
                velocity.z *= runSpeedModifier;
            }

            // We must return a velocity vector relative to the world
            return Transform.TransformDirection(velocity);
        }
    }
}