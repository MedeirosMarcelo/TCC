using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Character.States.Attack
{

    public abstract class BaseSwing : CharacterState, IAttack
    {
        public AttackDirection Direction { get; protected set; }
        public bool IsHeavy { get; protected set; }
        public int Damage { get; protected set; }

        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        List<ITargetable> haveHitted = new List<ITargetable>();

        const float speed = 2.5f;

        public SwordStance nextStance { get; protected set; }
        public BaseSwing(CharacterFsm fsm) : base(fsm)
        {
            timer = new TimerBehaviour(this);
            animation = new AnimationBehaviour(this, Character.animator);
            turnRate = 0f;
            nextStance = SwordStance.Right;
        }

        // tiago pirando com diretivas de compilador em 3... 2... 1...
#if UNITY_EDITOR
        private void DrawSphereCast(Vector3 origin, float radius, Vector3 direction, float distance, params object[] args)
        {
            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.GetComponent<Collider>().enabled = false;
            Vector3 destination = origin + direction.normalized * distance;
            Vector3 midPoint = (origin + destination) / 2f;
            capsule.transform.position = midPoint;
            capsule.transform.localScale = new Vector3(radius, Vector3.Distance(origin, destination) / 2f, radius);
            capsule.transform.rotation = Character.sword.rotation;
            Object.Destroy(capsule, 2f);
        }
#endif
        public bool GetCollisionPoint(out RaycastHit hitInfo)
        {
            Ray ray = new Ray(Character.sword.position, Character.sword.up);
            float radius = Character.AttackCollider.radius * Character.sword.lossyScale.y;
            float distance = Character.AttackCollider.height * Character.sword.lossyScale.z;
#if UNITY_EDITOR
            Debug.DrawLine(Character.sword.position, Character.sword.position + Character.sword.up, Color.yellow, 2f);
            DrawSphereCast(Character.sword.position, radius, Character.sword.up, distance + radius * 2f);
#endif
            RaycastHit[] hits = Physics.SphereCastAll(Character.sword.position, radius, Character.sword.up, distance + radius * 2f, LayerMask.GetMask("Hitbox", "Sword"));
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.transform.root.gameObject != Character.gameObject)
                {
                    hitInfo = hit;
                    Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up * 2, Color.magenta, 2f);
                    Debug.Log("HIT POINT: " + hitInfo.point.ToString());
                    return true;
                }
            }
            hitInfo = new RaycastHit();
            return false;
        }
        public bool CanHit(ITargetable obj)
        {
            if (haveHitted.Contains(obj))
            {
                return false;
            }
            else
            {
                haveHitted.Add(obj);
                return true;
            }
        }
        public void Blocked()
        {
            Fsm.ChangeState("STAGGER");
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            haveHitted.Clear();
            Character.SwordTrail.Activate();
            Character.AttackCollider.enabled = true;
            AudioManager.Play(ClipType.Attack, Character.audioSource);
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
