using UnityEngine;
using UnityEngine.Assertions;
using Assets.Scripts.Game;
using Assets.Scripts.Common;

namespace Assets.Scripts.Minion
{
    using System.Collections.Generic;
    using System.Linq;
    using DecalPainter = Blood.DecalPainter;
    public class MinionController : MonoBehaviour, ITargetable
    {
        [Header("Config:")]
        public Animator bloodAnimator;
        public Transform sword;

        public Rigidbody Rigidbody { get; private set; }
        public NavMeshAgent NavAgent { get; private set; }
        public Animator Animator { get; private set; }
        public AudioSource Audio { get; private set; }
        public Collider Hitbox { get; private set; }
        public Collider PushCollider { get; private set; }
        public CapsuleCollider AttackCollider { get; private set; }
        public MinionFsm Fsm { get; private set; }

        // Itargetable
        public Team Team { get; set; }
        public bool IsDead { get { return (Health <= 0); } }
        public Transform Transform { get { return transform; } }

        // Internal State
        public ITargetable Target;
        public int Health { get; private set; }
        public Vector3 Velocity { get; private set; }

        // Spawn
        public Vector3 SpawnPosition { get; private set; }
        public Quaternion SpawnRotation { get; private set; }

        public void Awake()
        {
            Assert.IsFalse(bloodAnimator == null);
            Assert.IsFalse(sword == null);

            Health = 1;
            SpawnPosition = Transform.position;
            SpawnRotation = Transform.rotation;

            Rigidbody = GetComponent<Rigidbody>();
            Assert.IsFalse(Rigidbody == null);
            NavAgent = GetComponent<NavMeshAgent>();
            Assert.IsFalse(NavAgent == null);
            Animator = GetComponent<Animator>();
            Assert.IsFalse(Animator == null);
            Audio = GetComponent<AudioSource>();
            Assert.IsFalse(Audio == null);

            // Colliders
            Hitbox = Transform.Find("Hitbox").GetComponent<Collider>();
            Assert.IsFalse(Hitbox == null);
            PushCollider = Transform.Find("PushCollider").GetComponent<Collider>();
            Assert.IsFalse(PushCollider == null);
            AttackCollider = sword.Find("Attack Collider").GetComponent<CapsuleCollider>();
            Assert.IsFalse(AttackCollider == null);

            Fsm = new MinionFsm(this);
        }
        void Start()
        {
            Target = RandomTarget();
        }

        public void FixedUpdate()
        {
            Fsm.PreUpdate();
            Fsm.FixedUpdate();
        }
        public void Move(Vector3 position)
        {
            Velocity = (position - Rigidbody.position) / Time.fixedDeltaTime;
            Rigidbody.MovePosition(position);

            var localVelocity = Transform.InverseTransformDirection(Velocity);
            Animator.SetFloat("ForwardSpeed", localVelocity.z);
            Animator.SetFloat("RightSpeed", localVelocity.x);
        }
        public void Forward(Vector3 forward)
        {
            if (forward != Vector3.zero)
            {
                transform.forward = forward;
            }
        }
        void OnTriggerEnter(Collider collider)
        {
            Fsm.OnTriggerEnter(collider);
        }
        public void ReceiveDamage(int damage)
        {
            Health -= damage;
            Invoke("Bleed", 0.1f);
            if (Health <= 0)
            {
                Die();
            }
        }
        public void Win()
        {
            Stop();
            Rigidbody.isKinematic = true;
            Hitbox.gameObject.SetActive(false);
            PushCollider.gameObject.SetActive(false);
            AttackCollider.enabled = false;
            Fsm.ChangeState("WIN");
        }
        public void Die()
        {
            Health = 0;
            Stop();
            Rigidbody.isKinematic = true;
            Hitbox.gameObject.SetActive(false);
            PushCollider.gameObject.SetActive(false);
            AttackCollider.enabled = false;
            Fsm.ChangeState("DEFEAT");
        }
        public void Reset()
        {
            if (Team.Leader.Lives > 0)
            {
                Health = 2;
                Rigidbody.isKinematic = false;
                Transform.position = SpawnPosition;
                Transform.rotation = SpawnRotation;
                Hitbox.gameObject.SetActive(true);
                PushCollider.gameObject.SetActive(true);
                Fsm.ChangeState("IDLE");
            }
        }
        // NavAgent
        public void UpdateDestination(Vector3 destination, bool updateRotation = true)
        {
            Rigidbody.isKinematic = true;
            NavAgent.Resume();
            NavAgent.updateRotation = updateRotation;
            destination.y = transform.position.y;
            NavAgent.SetDestination(destination);

            var localVelocity = Transform.InverseTransformDirection(NavAgent.velocity);
            Animator.SetFloat("ForwardSpeed", localVelocity.z);
            Animator.SetFloat("RightSpeed", localVelocity.x);
        }
        public void Stop()
        {
            Rigidbody.isKinematic = false;
            NavAgent.Stop();
            Animator.SetFloat("ForwardSpeed", 0f);
            Animator.SetFloat("RightSpeed", 0f);
        }
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
        void Bleed()
        {
            Vector3 pos = transform.position * 1.1f;
            pos.z = transform.position.z - 0.5f;
            pos.y = transform.position.y + 0.5f;
            DecalPainter.Instance.Paint(pos, Color.gray, 10);
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