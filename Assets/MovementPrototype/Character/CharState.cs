using UnityEngine;
using Swing = Assets.MovementPrototype.Character.States.HoldAttackStates.BaseSwing;

public abstract class CharState : BaseState
{
    public CharController Character { get; protected set; }
    public BaseInput Input { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }
    public Transform Transform { get; protected set; }
    public GameObject Target { get { return Character.target; } protected set { Character.target = value; } }

    // Timed state
    protected float elapsed;
    protected float totalTime;
    protected string nextState;

    // Movement state
    protected bool canPlayerMove;
    protected float moveSpeed;
    protected float forwardSpeedModifier;
    protected float backwardSpeedModifier;
    protected float sideSpeedModifier;
    protected float maxAcceleration;
    protected float turnRate;
    protected float lockedTurnModifier;
    protected float defenseAngle;

    public CharState(CharFsm fsm)
    {
        Fsm = fsm;
        Character = fsm.Character;
        Input = Character.input;
        Rigidbody = Character.rbody;
        Transform = Character.transform;

        //Movement
        canPlayerMove = false;
        moveSpeed = 3.5f;
        forwardSpeedModifier = 1f;
        backwardSpeedModifier = 0.5f;
        sideSpeedModifier = 0.75f;
        maxAcceleration = 2f;
        turnRate = 2f;
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
        base.OnTriggerEnter(collider);
        if (Character.collidedWith != collider.gameObject)
        {
            if (collider.name == "Attack Collider")
            {
                var otherCharacter = collider.transform.parent.parent.GetComponent<CharController>();
                if (!ReferenceEquals(Character, otherCharacter))
                {
                    var attackerState = otherCharacter.fsm.Current as Swing;
                    if (attackerState != null)
                    {
                        Character.collidedWith = collider.gameObject;
                        Character.ResetCollision();
                        Character.bloodAnimator.SetTrigger("Bleed");
                        Character.ReceiveDamage(attackerState.Damage);
                    }
                }
            }
        }
        // otherwise defer to base
        base.OnTriggerEnter(collider);
    }

    public void Look()
    {
        const float turnUnit = Mathf.PI / 30; // 12'
        if (turnRate > 0f)
        {
            if (Input.run > 0.25f || Character.debugDisableLockToTarget)
            {
                if (Input.move.isActive || Character.debugDisableLockToTarget)
                {
                    var forward = Vector3.RotateTowards(
                        Transform.forward,
                        Input.move.vector.normalized,
                        turnUnit * turnRate,
                        1f);
                    Character.Forward(forward);
                }
            }
            else if (lockedTurnModifier > 0f)
            {
                var vec = Input.look.vector;
                if (vec.magnitude > 0.25)
                {
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
                        (Target.transform.position - Transform.position).normalized,
                        turnUnit * turnRate * lockedTurnModifier,
                        1f);
                    Character.Forward(forward);
                }
            }
        }
    }
    void PlayerMove()
    {
        if (canPlayerMove && Input.move.isActive)
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
    }
    Vector3 GetInputVelovity()
    {
        var velocity = Transform.InverseTransformDirection(Input.move.vector);
        // Now velocity is relative to the character so X is side to side and Z is front/back
        velocity.x *= moveSpeed * sideSpeedModifier;
        velocity.z *= moveSpeed *
            ((Mathf.Sign(velocity.z) > 0) ? forwardSpeedModifier : backwardSpeedModifier);
        // We must return a velocity vector relative to the world
        return Transform.TransformDirection(velocity);

    }
}