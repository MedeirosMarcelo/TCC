using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using Assets.Scripts.Game;
using Assets.Scripts.Common;

namespace Assets.Scripts.Character
{
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
        public int Health { get; private set; }
        public int Lives { get; private set; }

        public bool debugDisableLockToTarget = false;

        public ITargetable target;
        public Animator bloodAnimator;
        public Material baseMaterial;
        public Material dodgeMaterial;
        public GameObject blockSpark;

        public PlayerIndex id
        {
            get { return input.id; }
            set { input.id = value; }
        }

        public BaseInput input { get; private set; }
        public Rigidbody rbody { get; private set; }
        public AudioSource audioSource { get; private set; }
        public GameManager game { get; private set; }
        public CharacterFsm fsm { get; private set; }
        public Animator animator { get; private set; }
        public WeaponTrail SwordTrail { get; private set; }
        public MeshRenderer Mesh { get; private set; }
        public CapsuleCollider AttackCollider { get; private set; }
        public BoxCollider BlockMidCollider { get; private set; }
        public BoxCollider BlockHighCollider { get; private set; }

        // Dash state data
        public Vector3 DashVelocity { get; set; }
        public string MovementState { get; set; }
        public SwordStance Stance;
        bool canControl;

        // Itargetable
        public Team Team { get; set; }
        public bool IsDead { get { return (Health <= 0); } }
        public Transform Transform { get { return transform; } }

        //???
        public Transform center;
        public Transform swordHilt;

        [Header("Prefabs:")]
        public GameObject swordPrefab;

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

        public void Awake()
        {
            Health = 2;
            Lives = 3;

            game = GameObject.Find("GameManager").GetComponent<GameManager>();
            //game.characterList.Add(this);

            center = transform.Find("Center");
            swordHilt = transform.Find("Sword");

            input = new GamePadInput();

            Stance = SwordStance.Right;
            rbody = GetComponent<Rigidbody>();
            Assert.IsNotNull(rbody);
            animator = GetComponent<Animator>();
            Assert.IsNotNull(animator);
            Mesh = transform.Find("Model").GetComponent<MeshRenderer>();
            Assert.IsNotNull(Mesh);
            audioSource = GetComponent<AudioSource>();
            Assert.IsNotNull(audioSource);

            // Colliders
            AttackCollider = transform.Find("Sword").Find("Attack Collider").GetComponent<CapsuleCollider>();
            Assert.IsNotNull(AttackCollider);
            BlockMidCollider = transform.Find("Sword").Find("Block Mid Collider").GetComponent<BoxCollider>();
            Assert.IsNotNull(BlockMidCollider);
            BlockHighCollider = transform.Find("Sword").Find("Block High Collider").GetComponent<BoxCollider>();
            Assert.IsNotNull(BlockHighCollider);

            // Trail init
            SwordTrail = transform.Find("Sword").Find("X-WeaponTrail").GetComponent<Xft.XWeaponTrail>();
            Assert.IsNotNull(SwordTrail);
            SwordTrail.Init();
            SwordTrail.Deactivate();

            currentId += 1;
            guiId = currentId;

            // Fsm must be last, states will access input, rbody ...
            fsm = new CharacterFsm(this);
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
            transform.forward = forward;
        }
        void OnTriggerEnter(Collider collider)
        {
            fsm.OnTriggerEnter(collider);
        }

        int guiId = 0;
        static int currentId = 0;
        void OnGUI()
        {
            string text = input.Debug + "\n" + fsm.DebugString;
            text += "\n" + "Velocity = " + rbody.velocity + " mod = " + rbody.velocity.magnitude.ToString("N2");
            GUI.Label(new Rect((guiId - 1) * (Screen.width / 4), 0, Screen.width / 4, Screen.height), text);
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
            Lives--;
            if (Lives > 0)
            {
               PlantSword();
            }
            game.CheckEndRound();
        }

        public void ApplyBaseMaterial()
        {
            Mesh.material = baseMaterial;
        }

        public void ApplyDodgeMaterial()
        {
            Mesh.material = dodgeMaterial;
        }

        public void ShowBlockSpark(Vector3 position)
        {
            Vector3 pos = new Vector3(position.x, position.y + 0.6f, position.z + 0.4f);
            Instantiate(blockSpark, pos, blockSpark.transform.rotation);
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

        public void PrintLog(string text)
        {
            print(text);
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
                target = nextTarget;
            }
        }

        void PlantSword()
        {
            Vector3 pos = transform.position;
            pos.y += 0.5f;
            Instantiate(swordPrefab, pos, swordPrefab.transform.rotation);
            transform.Find("Model").Find("Swords").Find("Sword " + Lives).gameObject.SetActive(false);
        }

        public GameObject collidedWith;
        public void ResetCollision()
        {
            StartCoroutine("ResetCol");
        }

        IEnumerator ResetCol()
        {
            yield return new WaitForSeconds(0.3f);
            collidedWith = null;
        }
    }
}
