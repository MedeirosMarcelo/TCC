using UnityEngine;
using UnityEngine.Assertions;
using Assets.Scripts.Game;
using Assets.Scripts.Common;

namespace Assets.Scripts.Humanoid
{
    using DecalPainter = Blood.DecalPainter;
    using WeaponTrail = Xft.XWeaponTrail;

    public class HumanoidController : MonoBehaviour, ITargetable
    {
        [Header("Config:")]
        public Animator bloodAnimator;
        public Transform sword;

        // Movement
        public Rigidbody Rigidbody { get; protected set; }
        public NavMeshAgent NavAgent { get; protected set; }

        // A-E-S-T-H-E-T-I-C-S 
        public Animator Animator { get; protected set; }
        public AudioSource Audio { get; protected set; }

        // Colliders
        public CapsuleCollider Hitbox { get; protected set; }
        public CapsuleCollider TooCloseBox { get; protected set; }
        public CapsuleCollider AttackCollider { get; protected set; }

        // Trail
        public WeaponTrail SwordTrail { get; protected set; }

        // Itargetable
        public Team Team { get; set; }
        public bool IsDead { get { return (Health <= 0); } }
        public Transform Transform { get { return transform; } }

        // Internal State
        public virtual bool Ended { get { throw new System.NotImplementedException(); } }
        public ITargetable Target;
        public int Health { get; protected set; }
        public Vector3 Velocity { get; set; }
        protected int StartingHealth;

        // Spawn
        public Vector3 SpawnPosition { get; protected set; }
        public Quaternion SpawnRotation { get; protected set; }

        protected virtual void Awake()
        {
            Assert.IsFalse(bloodAnimator == null);
            Assert.IsFalse(sword == null);
            Assert.IsTrue(StartingHealth > 0);

            Health = StartingHealth;
            SpawnPosition = Transform.position;
            SpawnRotation = Transform.rotation;
            Velocity = Vector3.zero;

            // Movements
            Rigidbody = GetComponent<Rigidbody>();
            Assert.IsFalse(Rigidbody == null);
            NavAgent = GetComponent<NavMeshAgent>();
            Assert.IsFalse(NavAgent == null);

            // A-E-S-T-H-E-T-I-C-S 
            Animator = GetComponent<Animator>();
            Assert.IsFalse(Animator == null);
            Audio = GetComponent<AudioSource>();
            Assert.IsFalse(Audio == null);

            // Colliders
            Hitbox = Transform.Find("Hitbox").GetComponent<CapsuleCollider>();
            Assert.IsFalse(Hitbox == null);
            TooCloseBox = Transform.Find("TooCloseBox").GetComponent<CapsuleCollider>();
            //Assert.IsFalse(Hitbox == null);
            AttackCollider = sword.Find("Attack Collider").GetComponent<CapsuleCollider>();
            Assert.IsFalse(AttackCollider == null);

            // Sword Trail
            SwordTrail = sword.Find("X-WeaponTrail").GetComponent<WeaponTrail>();
            Assert.IsFalse(SwordTrail == null);
            SwordTrail.Init();
            SwordTrail.Deactivate();
        }
        // Movement
        public void Move(Vector3 position)
        {
            Velocity = (position - Rigidbody.position) / Time.fixedDeltaTime;
            Rigidbody.MovePosition(position);

            var localVelocity = Transform.InverseTransformDirection(Velocity);
            Animator.SetFloat("ForwardSpeed", localVelocity.z);
            Animator.SetFloat("RightSpeed", localVelocity.x);
        }
        public void Stop()
        {
            Rigidbody.MovePosition(Transform.position);
            Animator.SetFloat("ForwardSpeed", 0f);
            Animator.SetFloat("RightSpeed", 0f);
        }
        public void Forward(Vector3 forward)
        {
            if (forward != Vector3.zero)
            {
                transform.forward = forward;
            }
        }
        public void NavmeshMove(Vector3 destination, bool updateRotation = true)
        {
            Rigidbody.isKinematic = true;
            NavAgent.Resume();
            NavAgent.updateRotation = updateRotation;
            destination.y = transform.position.y;
            NavAgent.SetDestination(destination);
            NavmeshAnimateWalk();
       }
        public void NavmeshStop()
        {
            NavAgent.Stop();
            Animator.SetFloat("ForwardSpeed", 0f);
            Animator.SetFloat("RightSpeed", 0f);
            Rigidbody.isKinematic = false;
        }
        public void NavmeshAnimateWalk()
        {
            var localVelocity = Transform.InverseTransformDirection(NavAgent.velocity);
            Animator.SetFloat("ForwardSpeed", localVelocity.z);
            Animator.SetFloat("RightSpeed", localVelocity.x);
        }

        // Round stuff
        public virtual void Win()
        {
            // Movement
            NavmeshStop();
            Rigidbody.isKinematic = true;
            //Colliders
            Hitbox.enabled = false;
            TooCloseBox.enabled = false;
            AttackCollider.enabled = false;
        }
        public virtual void Die()
        {
            Health = 0;
            // Movement
            NavmeshStop();
            Rigidbody.isKinematic = true;
            //Colliders
            Hitbox.enabled = false;
            TooCloseBox.enabled = false;
            AttackCollider.enabled = false;
        }

        public virtual void PreRound()
        {
            if (Team.Leader.Lives > 0)
            {
                Health = StartingHealth;
                // Movement
                Rigidbody.isKinematic = false;
                //Colliders
                Hitbox.enabled = true;
                TooCloseBox.enabled = true;
                AttackCollider.enabled = false;
            }
        }
        public virtual void Round()
        {
        }
        public virtual void PostRound()
        {
        }

        // Health Stuff
        public virtual void ReceiveDamage(int damage)
        {
            Health -= damage;
            Invoke("Bleed", 0.1f);
            if (Health <= 0)
            {
                Die();
            }
            AudioManager.Play(ClipType.Hit, Audio);
        }
        public void PlayFootsteps()
        {
            AudioManager.Play(ClipType.Footsteps, Audio);
            //AnimatorStateInfo currentState = Animator.GetCurrentAnimatorStateInfo(0);
            //UnityEngine.Debug.Log("STEP " + " " + currentState.normalizedTime + " " + currentState.shortNameHash + " " + Animator.StringToHash("MoveLow"));
            //if (currentState.normalizedTime > 0.2f && currentState.normalizedTime < 0.25f)
            //{
            //    AudioManager.Play(ClipType.Footsteps, Audio);
            //}
            //x = 1/maxFrames
            // 1 - 65
            // x - 1
        }
        private void Bleed()
        {
            Vector3 pos = transform.position * 1.1f;
            pos.z = transform.position.z - 0.5f;
            pos.y = transform.position.y + 0.5f;
            DecalPainter.Instance.Paint(pos, Color.gray, 10);
        }
    }
}