using UnityEngine;

namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public abstract class BaseSwing : AnimatedState
    {
        const float speed = 2.5f;
        public int Damage { get; protected set; }
        public SwordStance nextStance { get; protected set; }
        public BaseSwing(CharFsm fsm) : base(fsm)
        {
            turnRate = 0f;
            nextStance = SwordStance.Right;
        }
        public bool GetCollisionPoint(out RaycastHit hitInfo)
        {
            Ray ray = new Ray(Character.swordHilt.position, Character.swordHilt.up);
            return Physics.SphereCast(ray, Character.AttackCollider.radius * Character.swordHilt.localScale.y, out hitInfo, LayerMask.GetMask("Character", "Sword"));
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.SwordTrail.Activate();
            Character.AttackCollider.enabled = true;
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.AttackCollider.enabled = false;
            Character.Stance = nextStance;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));
        }
    }
}
