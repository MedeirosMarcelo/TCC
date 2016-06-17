using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using Assets.Scripts.Game;
using Assets.Scripts.Common;

namespace Assets.Scripts.Character
{
    using PlayerIndex = Game.PlayerIndex;
    using BaseInput = Input.BaseInput;
    using InputEvent = Input.InputEvent;
    using GamePadInput = Input.GamePadInput;
    using GameManager = Game.GameManager;
    using WeaponTrail = Xft.XWeaponTrail;
    using DecalPainter = Blood.DecalPainter;
    using System.Collections.Generic;

    public enum SwordStance
    {
        Left,
        Right,
        High
    }

    public class CharacterController : MonoBehaviour, ITargetable
    {
        [Header("Config:")]
        public Animator bloodAnimator;
        public Transform sword;
        public List<GameObject> lifeCounters;
        [Header("Prefabs:")]
        public GameObject sparkPrefab;
        public GameObject swordPrefab;
        // Proprieties
        public BaseInput input { get; private set; }
        public Rigidbody rbody { get; private set; }
        public AudioSource audioSource { get; private set; }
        public GameManager game { get; private set; }
        public CharacterFsm fsm { get; private set; }
        public Animator animator { get; private set; }
        public WeaponTrail SwordTrail { get; private set; }
        public Collider PushCollider { get; private set; }
        public SkinnedMeshRenderer Head { get; private set; }
        public SkinnedMeshRenderer Eyes { get; private set; }
        public SkinnedMeshRenderer Body { get; private set; }
        public CapsuleCollider AttackCollider { get; private set; }
        public CapsuleCollider BlockMidCollider { get; private set; }
        public CapsuleCollider BlockHighCollider { get; private set; }
        // Internal State
        public ITargetable Target { get; set; }
        public SwordStance Stance { get; set; }
        public Vector3 Velocity { get; set; }
        public int Health { get; private set; }
        public int Lives { get; private set; }
        public bool Ended { get; private set; }
        public PlayerIndex Id
        {
            get { return input.id; }
            set { input.id = value; }
        }
        private bool canControl;
        public bool CanControl
        {
            get
            {
                return canControl;
            }
            set
            {
                canControl = value;
                if (!canControl)
                {
                    input.buffer.Pop<InputEvent>();
                    rbody.velocity = Vector3.zero;
                    rbody.angularVelocity = Vector3.zero;
                    Velocity = Vector3.zero;
                    rbody.Sleep();
                }
            }
        }


        // Itargetable Properties
        public Team Team { get; set; }
        public bool IsDead { get { return (Health <= 0); } }
        public Transform Transform { get { return transform; } }

        // Spawn
        public Vector3 SpawnPosition { get; private set; }
        public Quaternion SpawnRotation { get; private set; }

        // Editor Variables
#if UNITY_EDITOR
        private int guiId = 0;
        private static int currentId = 0;
#endif

        public void Awake()
        {
            Velocity = Vector3.zero;
            Health = 2;
            Lives = 3;
            Ended = false;
            input = new GamePadInput();
            Stance = SwordStance.Right;
            Target = this; // guarantee target will not be null
            SpawnPosition = Transform.position;
            SpawnRotation = Transform.rotation;

            game = GameObject.Find("GameManager").GetComponent<GameManager>();
            Assert.IsFalse(game == null);
            rbody = GetComponent<Rigidbody>();
            Assert.IsFalse(rbody == null);
            animator = GetComponent<Animator>();
            Assert.IsFalse(animator == null);
            PushCollider = transform.Find("PushCollider").GetComponent<Collider>();
            Assert.IsFalse(PushCollider == null);

            Head = transform.Find("Model").Find("head").GetComponent<SkinnedMeshRenderer>();
            Assert.IsFalse(Head == null);
            Eyes = transform.Find("Model").Find("eyes").GetComponent<SkinnedMeshRenderer>();
            Assert.IsFalse(Eyes == null);
            Body = transform.Find("Model").Find("body").GetComponent<SkinnedMeshRenderer>();
            Assert.IsFalse(Body == null);

            audioSource = GetComponent<AudioSource>();
            Assert.IsFalse(audioSource == null);
            AttackCollider = sword.Find("Attack Collider").GetComponent<CapsuleCollider>();
            Assert.IsFalse(AttackCollider == null);
            BlockMidCollider = sword.Find("Block Mid Collider").GetComponent<CapsuleCollider>();
            Assert.IsFalse(BlockMidCollider == null);
            BlockHighCollider = sword.Find("Block High Collider").GetComponent<CapsuleCollider>();
            Assert.IsFalse(BlockHighCollider == null);
            SwordTrail = sword.Find("X-WeaponTrail").GetComponent<Xft.XWeaponTrail>();
            Assert.IsFalse(SwordTrail == null);
            // Trail init
            SwordTrail.Init();
            SwordTrail.Deactivate();

#if UNITY_EDITOR
            currentId += 1;
            guiId = currentId;
#endif
            // Fsm must be last, states will access input, rbody ...
            fsm = new CharacterFsm(this);
        }
        void Start()
        {
            ChangeTarget(transform.forward);
        }
        public void Update()
        {
            if (canControl)
            {
                input.Update();
            }
        }
        public void FixedUpdate()
        {
            fsm.PreUpdate();
            fsm.FixedUpdate();
            input.FixedUpdate();
        }
        public void Move(Vector3 position)
        {
            Velocity = (position - rbody.position) / Time.fixedDeltaTime;
            rbody.MovePosition(position);

            var localVelocity = Transform.InverseTransformDirection(Velocity);
            animator.SetFloat("ForwardSpeed", localVelocity.z);
            animator.SetFloat("RightSpeed", localVelocity.x);
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
            fsm.OnTriggerEnter(collider);
        }
        public void ReceiveDamage(int damage)
        {
            Health -= damage;
            StartCoroutine("DelayBlood");
            AudioManager.Play(ClipType.Hit, audioSource);
            if (Health <= 0)
            {
                Die();
            }
        }
        public void Win()
        {
            rbody.isKinematic = true;
            transform.Find("Hitbox").gameObject.SetActive(false);
            transform.Find("PushCollider").gameObject.SetActive(false);
            AttackCollider.enabled = false;
            fsm.ChangeState("WIN");
        }
        public void Die()
        {
            Health = 0;
            Lives--;
            rbody.isKinematic = true;
            transform.Find("Hitbox").gameObject.SetActive(false);
            transform.Find("PushCollider").gameObject.SetActive(false);
            AttackCollider.enabled = false;
            Team.Defeat();
            fsm.ChangeState("DEFEAT");
        }
        public void End()
        {
            Ended = true;
            game.CheckEndRound();
        }
        public void Reset()
        {
            if (Lives > 0)
            {
                Ended = false;
                Health = 2;
                rbody.isKinematic = false;

                Transform.position = SpawnPosition;
                Transform.rotation = SpawnRotation;

                transform.Find("Hitbox").gameObject.SetActive(true);
                transform.Find("PushCollider").gameObject.SetActive(true);

                for (int i = 0; i < lifeCounters.Count; i++)
                {
                    lifeCounters[i].SetActive(Lives >= i + 1);
                }

                fsm.ChangeState("MOVEMENT");
            }
        }

        public void ShowBlockSpark(Vector3 position)
        {
            Vector3 pos = new Vector3(position.x, position.y + 0.6f, position.z + 0.4f);
            Instantiate(sparkPrefab, pos, sparkPrefab.transform.rotation);
        }
        IEnumerator DelayBlood()
        {
            yield return new WaitForSeconds(0.1f);
            Paint();
        }
        public void Paint()
        {
            Vector3 pos = transform.position * 1.1f;
            pos.z = transform.position.z - 0.5f;
            pos.y = transform.position.y + 0.5f;
            DecalPainter.Instance.Paint(pos, Color.gray, 10);
        }
        public void ChangeTarget(Vector3 direction)
        {
            const float maxAngle = 45f;
            const float maxDistance = 7.5f;
            ITargetable nextTarget = null;
            float closestAngle = maxAngle;

            foreach (var team in Team.OtherTeams)
            {
                foreach (var target in team.Targets)
                {
                    if (!target.IsDead)
                    {
                        var angle = Vector3.Angle(direction, target.Transform.position - Transform.position);
                        var distance = Vector3.Distance(Transform.position, target.Transform.position);
                        if ((angle <= maxAngle && distance < maxDistance))
                        {
                            if ((angle < closestAngle))
                            {
                                closestAngle = angle;
                                nextTarget = target;
                            }
                        }
                    }
                }
            }
            if (nextTarget != null)
            {
                Target = nextTarget;
            }
        }
        public void PlantSword()
        {
            Vector3 pos = transform.position;
            pos.y += 0.5f;
            Instantiate(swordPrefab, pos, swordPrefab.transform.rotation);
            // TODO: disable swords.
            // transform.Find("Model").Find("Swords").Find("Sword " + Lives).gameObject.SetActive(false);
        }

        public void PlayFootsteps()
        {
            AudioManager.Play(ClipType.Footsteps, audioSource);
        }
#if UNITY_EDITOR
        void OnGUI()
        {
            string text = "";
            //text +=  input.Debug + "\n";
            text += fsm.DebugString;
            text += "\n" + "ForwardSpeed = " + animator.GetFloat("ForwardSpeed").ToString("N2") + " RightSpeed = " + animator.GetFloat("RightSpeed").ToString("N2");
            text += "\n";
            foreach (var minion in Team.Minions)
            {
                text += "\n" + minion.Fsm.DebugString;
            }
            GUI.contentColor = Color.black;
            GUI.Label(new Rect((((guiId - 1) % 4)) * (Screen.width / 4), 0, Screen.width / 4, Screen.height), text);
        }
#endif
    }
}
