using UnityEngine;
using Assets.Scripts.Game;
using Assets.Scripts.Common;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Minion
{
    public class MinionController : Humanoid.HumanoidController
    {
        public override bool Ended { get { return (Fsm.Current.Name == "END"); } }
        public MinionFsm Fsm { get; private set; }

        protected override void Awake()
        {
            StartingHealth = 1;
            base.Awake();
            Fsm = new MinionFsm(this);
            Fsm.ChangeState("END");
        }
        void Start()
        {
            Target = RandomTarget();
        }
        void FixedUpdate()
        {
            Fsm.PreUpdate();
            Fsm.FixedUpdate();
        }
        void OnTriggerEnter(Collider collider)
        {
            Fsm.OnTriggerEnter(collider);
        }
        void OnCollisionEnter(Collision collision)
        {
            Fsm.OnCollisionEnter(collision);
        }
        // Win/Die
        public override void Win()
        {
            base.Die();
            Fsm.ChangeState("WIN");
        }
        public override void Die()
        {
            base.Die();
            Fsm.ChangeState("DEFEAT");
        }
        //Round
        public override void PreRound()
        {
            bool defeated = IsDead;
            base.PreRound();

            if (Team.Leader.Lives > 0)
            {
                if (defeated)
                {
                    Fsm.ChangeState("STAND");
                }
                else
                {
                    Fsm.ChangeState("RETURN");
                }
            }
        }
        public override void Round()
        {
            base.Round();
            if (Team.Leader.Lives > 0)
            {
                Fsm.ChangeState("IDLE");
            }
        }
        public override void PostRound()
        {
            base.PostRound();
            NavmeshStop();
            if (!IsDead && !Team.Leader.IsDead)
            {
                Fsm.ChangeState("WIN");
            }
        }

        // Minion Target
        public ITargetable ClosestTarget()
        {
            float minDistance = float.PositiveInfinity;
            ITargetable closest = null;
            foreach (var team in Team.OtherTeams)
            {
                foreach (var target in team.Targets)
                {
                    if (!target.IsDead)
                    {
                        var distance = (target.Transform.position - Transform.position).magnitude;
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closest = target;
                        }
                    }
                }
            }
            return closest;
        }
        public ITargetable ClosestTargetLeader()
        {
            float minDistance = float.PositiveInfinity;
            ITargetable closest = null;
            foreach (var team in Team.OtherTeams)
            {
                var target = team.Leader;
                if (!target.IsDead)
                {
                    var distance = (target.Transform.position - Transform.position).magnitude;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closest = target;
                    }
                }
            }
            return closest;
        }
        public ITargetable RandomTarget()
        {
            var validTargets = new List<ITargetable>();
            foreach (var team in Team.OtherTeams)
            {
                validTargets.AddRange(team.Targets.Where(target => !target.IsDead));
            }
            return (validTargets.Count > 0) ? validTargets[Random.Range(0, validTargets.Count)]
                                            : null;
        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (NavAgent && !IsDead)
            {
                Gizmos.color = Color.blue;
                var start = transform.position;
                var end = NavAgent.destination;
                start.y = 1f;
                end.y = 1f;
                Gizmos.DrawLine(start, end);
            }
        }
#endif
    }
}