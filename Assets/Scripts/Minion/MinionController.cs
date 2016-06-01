using UnityEngine;
using UnityEngine.Assertions;
using Assets.Scripts.Game;
using System;

namespace Assets.Scripts.Minion
{
    public class MinionController : MonoBehaviour, ITargetable
    {
        public ITargetable Target;

        public bool Active { get; set; }
        public int Health { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public MinionFsm Fsm { get; private set; }
        public Animator Animator { get; private set; }
        public MeshRenderer Mesh { get; private set; }
        public CapsuleCollider SwordCollider { get; private set; }
        public NavMeshAgent NavAgent { get; private set; }

        // Itargetable
        public Team Team { get; set; }
        public bool IsDead { get { return (Health <= 0); } }
        public Transform Transform { get { return transform; } }

#if UNITY_EDITOR
        int id = 0;
        static int currentId = 0;
#endif

        public void Awake()
        {
            Health = 2;
            Rigidbody = GetComponent<Rigidbody>();
            Assert.IsNotNull(Rigidbody);
            Animator = GetComponent<Animator>();
            Assert.IsNotNull(Animator);
            Mesh = transform.Find("Model").GetComponent<MeshRenderer>();
            Assert.IsNotNull(Mesh);
            SwordCollider = transform.Find("Sword").GetComponent<CapsuleCollider>();
            Assert.IsNotNull(SwordCollider);
            NavAgent = GetComponent<NavMeshAgent>();
            Assert.IsNotNull(NavAgent);

            // Fsm must be last
            Fsm = new MinionFsm(this);

#if UNITY_EDITOR
            currentId += 1;
            id = currentId;
#endif
        }

        public void FixedUpdate()
        {
            Fsm.PreUpdate();
            Fsm.FixedUpdate();
        }
        public void Move(Vector3 position)
        {
            Rigidbody.MovePosition(position);
        }
        public void Forward(Vector3 forward)
        {
            transform.forward = forward;
        }
        void OnTriggerEnter(Collider collider)
        {
            Fsm.OnTriggerEnter(collider);
        }
        public void ReceiveDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Fsm.ChangeState("DEATH");
            }
        }

        public void SetDestination(Vector3 destination, bool updateRotation = true)
        {
            Rigidbody.isKinematic = true;
            NavAgent.Resume();
            NavAgent.updateRotation = updateRotation;
            destination.y = transform.position.y;
            NavAgent.SetDestination(destination);
        }
        public void Stop()
        {
            Rigidbody.isKinematic = false;
            NavAgent.Stop();
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
        void OnGUI()
        {
            string text = Fsm.DebugString;
            GUI.Label(new Rect(0f, (id - 1) * (Screen.height / 20), Screen.width , Screen.height / 20), text);
        }
#endif
    }
}