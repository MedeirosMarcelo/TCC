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
        public SkinnedMeshRenderer Head { get; private set; }
        public SkinnedMeshRenderer Eyes { get; private set; }
        public SkinnedMeshRenderer Body { get; private set; }
        public CapsuleCollider AttackCollider { get; private set; }
        public CapsuleCollider BlockMidCollider { get; private set; }
        public CapsuleCollider BlockHighCollider { get; private set; }
        // Internal State
        public ITargetable Target { get; set; }
        public SwordStance Stance { get; set; }
        public int Health { get; private set; }
        public int Lives { get; private set; }
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
                    rbody.Sleep();
                }
            }
        }

        // Itargetable Proprieties
        public Team Team { get; set; }
        public bool IsDead { get { return (Health <= 0); } }
        public Transform Transform { get { return transform; } }
        // Dash Proprieties
        public Vector3 DashVelocity { get; set; }
        public string MovementState { get; set; }

        // Editor Variables
#if UNITY_EDITOR
        private int guiId = 0;
        private static int currentId = 0;
#endif

        public void Awake()
        {
            Health = 2;
            Lives = 3;
            input = new GamePadInput();
            Stance = SwordStance.Right;
            Target = this; // guarantee target will not be null

            game = GameObject.Find("GameManager").GetComponent<GameManager>();
            Assert.IsNotNull(game);
            rbody = GetComponent<Rigidbody>();
            Assert.IsNotNull(rbody);
            animator = GetComponent<Animator>();
            Assert.IsNotNull(animator);

            Head = transform.Find("Model").Find("head").GetComponent<SkinnedMeshRenderer>();
            Assert.IsNotNull(Head);
            Eyes = transform.Find("Model").Find("eyes").GetComponent<SkinnedMeshRenderer>();
            Assert.IsNotNull(Eyes);
            Body = transform.Find("Model").Find("body").GetComponent<SkinnedMeshRenderer>();
            Assert.IsNotNull(Body);

            audioSource = GetComponent<AudioSource>();
            Assert.IsNotNull(audioSource);
            AttackCollider = sword.Find("Attack Collider").GetComponent<CapsuleCollider>();
            Assert.IsNotNull(AttackCollider);
            BlockMidCollider = sword.Find("Block Mid Collider").GetComponent<CapsuleCollider>();
            Assert.IsNotNull(BlockMidCollider);
            BlockHighCollider = sword.Find("Block High Collider").GetComponent<CapsuleCollider>();
            Assert.IsNotNull(BlockHighCollider);
            SwordTrail = sword.Find("X-WeaponTrail").GetComponent<Xft.XWeaponTrail>();
            Assert.IsNotNull(SwordTrail);
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
            rbody.MovePosition(position);
            animator.SetFloat("Velocity", rbody.velocity.x);
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
                fsm.ChangeState("DEATH");
            }
        }
        public void Die()
        {
            Health = 0;
            Lives--;
            if (Lives > 0)
            {
                PlantSword();
            }
            game.CheckEndRound();
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
                        if (angle <= maxAngle && distance < maxDistance)
                        {
                            if (angle < closestAngle)
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
#if UNITY_EDITOR
        void OnGUI()
        {
            string text = "";
//            text +=  input.Debug + "\n";
            text += fsm.DebugString;
            text += "\n" + "Velocity = " + rbody.velocity + " mod = " + rbody.velocity.magnitude.ToString("N2");
            text += "\n";
            foreach (var minion in Team.Minions)
            {
                text += "\n" + minion.Fsm.DebugString;
            }
            GUI.contentColor = Color.black;
            GUI.Label(new Rect((guiId - 1) * (Screen.width / 4), 0, Screen.width / 4, Screen.height), text);
        }
#endif
    }
}
