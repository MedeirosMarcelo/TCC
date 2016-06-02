using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Character.States.Attack
{
    public abstract class BaseSwing : AnimatedState
    {
        List<CharacterController> haveHitted = new List<CharacterController>();

        const float speed = 2.5f;
        public int Damage { get; protected set; }
        public SwordStance nextStance { get; protected set; }
        public BaseSwing(CharacterFsm fsm) : base(fsm)
        {
            turnRate = 0f;
            nextStance = SwordStance.Right;
        }

        // tiago pirando com diretivas de compilador em 3... 2... 1...
#if UNITY_EDITOR
        private void DrawSphereCast(Vector3 origin, float radius, Vector3 direction, float distance, params object[] args)
        {
            UnityEngine.Debug.Log("DRAWING");
            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.GetComponent<Collider>().enabled = false;
            Vector3 destination = origin + direction.normalized * distance;
            Vector3 midPoint = (origin + destination) / 2f;
            capsule.transform.position = midPoint;
            capsule.transform.localScale = new Vector3(radius, Vector3.Distance(origin, destination) / 2f, radius);
            capsule.transform.rotation = Character.swordHilt.rotation;
            //capsule.transform.rotation = Quaternion.LookRotation();
            GameObject.Destroy(capsule, 2f);

        }
#endif
        public bool GetCollisionPoint(out RaycastHit hitInfo)
        {
            Ray ray = new Ray(Character.swordHilt.position, Character.swordHilt.up);
            float radius = Character.AttackCollider.radius * Character.swordHilt.lossyScale.y;
            float distance = Character.AttackCollider.height * Character.swordHilt.lossyScale.z;
#if UNITY_EDITOR
            UnityEngine.Debug.DrawLine(Character.swordHilt.position, Character.swordHilt.position + Character.swordHilt.up, Color.yellow, 2f);
            DrawSphereCast(Character.swordHilt.position, radius, Character.swordHilt.up, distance + radius * 2f);
#endif
            RaycastHit[] hits = Physics.SphereCastAll(Character.swordHilt.position, radius, Character.swordHilt.up, distance + radius * 2f, LayerMask.GetMask("Hitbox", "Sword"));
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != Character.gameObject)
                {
                    hitInfo = hit;
                    UnityEngine.Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up, Color.magenta, 2f);
                    UnityEngine.Debug.Log("HIT POINT: " + hitInfo.point.ToString());
                    return true;
                }
            }
            hitInfo = new RaycastHit();
            return false;
        }
        public bool CanHit(CharacterController obj)
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
