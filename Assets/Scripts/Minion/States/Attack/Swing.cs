using Assets.Scripts.Common;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Minion.States.Attack
{
    public class Swing : MinionState, IAttack
    {
        List<ITargetable> haveHitted = new List<ITargetable>();
        public AttackDirection Direction { get; protected set; }
        public bool IsHeavy { get; protected set; }
        public int Damage { get; protected set; }

        TimerBehaviour timer;
        AnimationBehaviour animation;

        public Swing(MinionFsm fsm) : base(fsm)
        {
            Name = "ATTACK/SWING";
            staminaCost = 0.5f; // uses 5% of stamina each attack/swing
            timer = new TimerBehaviour(this);
            timer.TotalTime = 0.15f;
            timer.OnFinish = () => Fsm.ChangeState("ATTACK/RECOVER");

            animation = new AnimationBehaviour(this, Animator);
            animation.PlayTime = 0.15f;
            animation.Name = "AttackHorizontalSwing";

            Damage = 1;
            Direction = AttackDirection.Horizontal;
            IsHeavy = false;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            UpdateDestination();
            Look();
        }
        void UpdateDestination()
        {
            Minion.UpdateDestination(Target.Transform.position, updateRotation: false);
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            haveHitted.Clear();
            Minion.AttackCollider.enabled = true;
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.AttackCollider.enabled = false;
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
#if UNITY_EDITOR
        private void DrawSphereCast(Vector3 origin, float radius, Vector3 direction, float distance, params object[] args)
        {
            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.GetComponent<Collider>().enabled = false;
            Vector3 destination = origin + direction.normalized * distance;
            Vector3 midPoint = (origin + destination) / 2f;
            capsule.transform.position = midPoint;
            capsule.transform.localScale = new Vector3(radius, Vector3.Distance(origin, destination) / 2f, radius);
            capsule.transform.rotation = Minion.AttackCollider.transform.rotation;
            Object.Destroy(capsule, 2f);
        }
#endif
        public bool GetCollisionPoint(out RaycastHit hitInfo)
        {
            var collider = Minion.AttackCollider;
            Ray ray = new Ray(collider.transform.position, collider.transform.up);
            float radius = collider.radius * collider.transform.lossyScale.y;
            float distance = collider.height * collider.transform.lossyScale.z;
#if UNITY_EDITOR
            Debug.DrawLine(collider.transform.position, collider.transform.position + collider.transform.up, Color.yellow, 2f);
            DrawSphereCast(collider.transform.position, radius, collider.transform.up, distance + radius * 2f);
#endif
            RaycastHit[] hits = Physics.SphereCastAll(collider.transform.position, radius, collider.transform.up, distance + radius * 2f, LayerMask.GetMask("Hitbox", "Sword"));
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.transform.root.gameObject != Minion.gameObject)
                {
                    hitInfo = hit;
                    if (hitInfo.point == Vector3.zero)
                    {
                        continue;
                    }
                    Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up * 2, Color.magenta, 2f);
                    Debug.Log("HIT POINT: " + hitInfo.point.ToString());
                    Debug.Log("HIT: " + hit.collider.transform.root.gameObject.name);
                    return true;
                }
            }
            hitInfo = new RaycastHit();
            return false;
        }
        public void Blocked()
        {
        }
    }
}

