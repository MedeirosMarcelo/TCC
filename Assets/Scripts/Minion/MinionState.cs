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
        public Variable Stamina { get; protected set; }
        public Variable Bravery { get; protected set; }
        public Variable Distance { get; protected set; }

        // How much stamina this state drains per FixedUpdate
        protected float staminaCost;

        public MinionState(MinionFsm fsm)
        {
            Fsm = fsm;
            Minion = fsm.Minion;
            Rigidbody = Minion.Rigidbody;
            Transform = Minion.transform;
            Animator = Minion.Animator;
            Stamina = fsm.Stamina;
            Bravery = fsm.Bravery;
            Distance = fsm.Distance;
        }

        public override void PreUpdate()
        {
            UpdateTarget();
            if (Target.IsDead)
            {
                // If updated target is dead we are out of targets
                Fsm.ChangeState("END");
            }
            else
            {
                Distance.Value = (Transform.position - Target.Transform.position).xz().magnitude;
                // After updating basics update behaviours
                base.PreUpdate();
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Stamina.Value = Mathf.Clamp(Stamina.Value - staminaCost, 0f, 1f);
        }

        public override void OnTriggerEnter(Collider collider)
        {
            if (collider.name == "Sword")
            {
                Minion.ReceiveDamage(1);
            }
            // otherwise defer to base
            base.OnTriggerEnter(collider);
        }

        void UpdateTarget()
        {

            if (Target != null && !Target.IsDead)
            {
                // If current target is valid ther is no need to update it
                return;
            }

            float minDistance = float.PositiveInfinity;
            ITargetable closer = null;
            foreach (var team in Minion.Team.OtherTeams)
            {
                foreach (var target in team.Targets)
                {
                    if (!target.IsDead)
                    {
                        var distance = (target.Transform.position - Transform.position).magnitude;
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closer = target;
                        }
                    }
                }
            }
            if (closer != null)
            {
                // Update target only if found one
                Target = closer;
            }
        }

        public void NextState()
        {
            var advance = Mamdami.And(Distance["far"], Stamina["high"]);
            var circle = Mamdami.And(Distance["mid"], Stamina["high"]);
            var attack = Mamdami.And(Distance["close"], Stamina["high"]);
            //Debug.Log("Distance:" + Distance.ToString() + " Stamina:" + Stamina.ToString());
            //Debug.Log("advance,circle,attack = " + advance + "," + circle + "," + attack);

            if (advance > 0.5f)
            {
                Fsm.ChangeState("ADVANCE");
                return;
            }
            if (circle > 0.5f)
            {
                Fsm.ChangeState("CIRCLE");
                return;
            }
            if (attack > 0.5f)
            {
                Fsm.ChangeState("ATTACK/WINDUP");
                return;
            }
            Fsm.ChangeState("IDLE");
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