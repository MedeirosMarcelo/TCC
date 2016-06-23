using UnityEngine;
using Assets.Scripts.Fuzzy;
using Assets.Scripts.Fuzzy.Operators;
using Assets.Scripts.Common;

namespace Assets.Scripts.Minion
{
    public abstract class MinionState : BaseState
    {
        public MinionController Minion { get; protected set; }
        public Rigidbody Rigidbody { get; protected set; }
        public Transform Transform { get; protected set; }
        public Animator Animator { get; private set; }
        public ITargetable Target
        {
            get { return Minion.Target; }
            protected set { Minion.Target = value; }
        }
        // Fuzzy Variables
        public Variable Distance { get; protected set; }
        public Variable Threat { get; protected set; }
        public Variable Stress { get; protected set; }

        // How much stamina this state drains (on Enter)
        [System.Obsolete]
        protected float staminaCost;

        public MinionState(MinionFsm fsm)
        {
            Fsm = fsm;
            Minion = fsm.Minion;
            Rigidbody = Minion.Rigidbody;
            Transform = Minion.transform;
            Animator = Minion.Animator;

            Distance = fsm.Distance;
            Threat = fsm.Threat;
            Stress = fsm.Stress;
        }

        public override void OnTriggerEnter(Collider collider)
        {
            Debug.Log("OnTriggerEnter");
            if (collider.CompareTag("MinionAttackCollider") || collider.CompareTag("CharacterAttackCollider"))
            {
                Minion.ReceiveDamage(1);
            }
            // otherwise defer to base
            base.OnTriggerEnter(collider);
        }

        public override void PreUpdate()
        {
            if (Target == null || Target.IsDead)
            {
                var closest = Minion.ClosestTarget(); ;
                if (closest == null)
                {
                    Fsm.ChangeState("END");
                }
                else
                {
                    Target = closest;
                }
            }
            base.PreUpdate();
        }
        public void NextState()
        {
            var closest = Minion.ClosestTarget();
            Threat.Value = (Transform.position - closest.Transform.position).xz().magnitude;

            if (Threat["danger"] > Mamdami.And(Threat["vigilant"],
                                               Threat["safe"]))
            {
                Danger();
            }
            else if (Threat["vigilant"] > Mamdami.And(Threat["danger"],
                                                      Threat["safe"]))
            {
                Vigilant();
            }
            else
            {
                Safe();
            }
            Stress.Value = Mathf.Clamp(Stress.Value, -2f, 2f);
        }
        public void Danger()
        {
            // stress
            Stress.Value += Random.Range(0.2f, 0.4f);
            // target
            if (Distance["close"] > Mamdami.And(Distance["mid"], Distance["far"]))
            {
                TargetClosest();
            }
            // state
            if (Stress["stressed"] == 1f)
            {
                Fsm.ChangeState("RETREAT");
            }
            else if (Stress["relaxed"] == 1f)
            {
                Fsm.ChangeState("ATTACK/WINDUP");
            }
            else
            {
                Fsm.ChangeState((Random.value > 0.5f) ? "CIRCLE" : "ATTACK/WINDUP");
            }
        }
        public void Vigilant()
        {
            // stress
            Stress.Value += Random.Range(-0.1f, 0.1f);
            // target
            if (Distance["far"] > Mamdami.And(Distance["close"], Distance["mid"]))
            {
                TargetClosest();
            }
            // state
            if (Stress["stressed"] == 1f)
            {
                Fsm.ChangeState("RETREAT");
            }
            else if (Stress["relaxed"] == 1f)
            {
                Fsm.ChangeState("ADVANCE");
            }
            else
            {
                Fsm.ChangeState((Random.value > 0.5f) ? "CIRCLE" : "IDLE");
            }
        }
        public void Safe()
        {
            // stress
            Stress.Value += Random.Range(-0.3f, -0.1f);
            // target
            TargetClosestLeader();
            // state
            if (Stress["stressed"] == 1f)
            {
                Fsm.ChangeState("IDLE");
            }
            else if (Stress["relaxed"] == 1f)
            {
                Fsm.ChangeState((Random.value > 0.5f) ? "ADVANCE" : "IDLE");
            }
            else
            {
                Fsm.ChangeState((Random.value > 0.5f) ? "CIRCLE" : "IDLE");
            }
        }
        public void TargetClosest()
        {
            var closest = Minion.ClosestTarget();
            if (closest != null && closest != Target)
            {
                Target = closest;
            }
        }
        public void TargetClosestLeader()
        {
            var closest = Minion.ClosestTargetLeader();
            if (closest != null && closest != Target)
            {
                Target = closest;
            }
        }

        public void Look()
        {
            var forward = Vector3.RotateTowards(
                Transform.forward,
                (Target.Transform.transform.position - Transform.position).xz().normalized,
                Mathf.Deg2Rad * Minion.NavAgent.angularSpeed * Time.fixedDeltaTime,
                0f);
            Minion.Forward(forward);
        }
    }
}