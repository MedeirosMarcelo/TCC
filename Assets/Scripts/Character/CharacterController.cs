using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using Assets.Scripts.Game;
using Assets.Scripts.Common;

namespace Assets.Scripts.Character
{
    using PlayerIndex = Game.PlayerIndex;
    using BaseInput = Input.BaseInput;
    using InputEvent = Input.InputEvent;
    using GamePadInput = Input.GamePadInput;
    using GameManager = Game.GameManager;

    public enum SwordStance
    {
        Left,
        Right,
        High
    }

    public class CharacterController : Humanoid.HumanoidController
    {
        [Header("Config:")]
        public List<GameObject> HandSwords;
        public List<GameObject> BackSwords;

        [Header("Prefabs:")]
        public GameObject sparkPrefab;
        public GameObject swordPrefab;

        // Proprieties
        public BaseInput input { get; private set; }
        public CharacterFsm Fsm { get; private set; }
        public GameManager game { get; private set; }

        public SkinnedMeshRenderer Head { get; private set; }
        public SkinnedMeshRenderer Eyes { get; private set; }
        public SkinnedMeshRenderer Body { get; private set; }

        public CapsuleCollider BlockMidCollider { get; private set; }
        public CapsuleCollider BlockHighCollider { get; private set; }

        public GameObject Dashdust { get; private set; }

        // Internal State
        public override bool Ended { get { return (Fsm.Current.Name == "END"); } }
        public SwordStance Stance { get; set; }
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
                    Rigidbody.velocity = Vector3.zero;
                    Rigidbody.angularVelocity = Vector3.zero;
                    Velocity = Vector3.zero;
                    Rigidbody.Sleep();
                }
            }
        }

        // Editor Variables
#if UNITY_EDITOR
        private int guiId = 0;
        private static int currentId = 0;
#endif

        protected override void Awake()
        {
            StartingHealth = 2;
            base.Awake();

            Lives = 3;
            input = new GamePadInput();
            Stance = SwordStance.Right;
            Target = this; // guarantee target will not be null

            game = GameObject.Find("GameManager").GetComponent<GameManager>();
            Assert.IsFalse(game == null);

            Head = transform.Find("Model").Find("head").GetComponent<SkinnedMeshRenderer>();
            Assert.IsFalse(Head == null);
            Eyes = transform.Find("Model").Find("eyes").GetComponent<SkinnedMeshRenderer>();
            Assert.IsFalse(Eyes == null);
            Body = transform.Find("Model").Find("body").GetComponent<SkinnedMeshRenderer>();
            Assert.IsFalse(Body == null);

            BlockMidCollider = sword.Find("Block Mid Collider").GetComponent<CapsuleCollider>();
            Assert.IsFalse(BlockMidCollider == null);
            BlockHighCollider = sword.Find("Block High Collider").GetComponent<CapsuleCollider>();
            Assert.IsFalse(BlockHighCollider == null);

            Dashdust = transform.Find("Dashdust").gameObject;
            Assert.IsFalse(Dashdust == null);

            HandSwords[1].SetActive(false);
            HandSwords[2].SetActive(false);

#if UNITY_EDITOR
            currentId += 1;
            guiId = currentId;
#endif
            // Fsm must be last, states will access input, rbody ...
            Fsm = new CharacterFsm(this);
            Fsm.ChangeState("END");
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
            Fsm.PreUpdate();
            Fsm.FixedUpdate();
            input.FixedUpdate();
        }
        void OnTriggerEnter(Collider collider)
        {
            Fsm.OnTriggerEnter(collider);
        }
        void OnCollisionEnter(Collision collision)
        {
            Fsm.OnCollisionEnter(collision);
        }
        // Round
        public override void Win()
        {
            base.Win();
            Fsm.ChangeState("WIN");
        }
        public override void Die()
        {
            base.Die();
            Team.Defeat();
            Fsm.ChangeState("DEFEAT");
            Lives--;
        }

        //Round
        public override void PreRound()
        {
            Debug.Log("PreRound");
            bool defeated = IsDead;
            base.PreRound();

            if (Lives > 0)
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
            Debug.Log("StartRound");
            base.Round();
            if (Lives > 0)
            {
                CanControl = true;
                Fsm.ChangeState("MOVEMENT");
            }
        }
        public override void PostRound()
        {
            Debug.Log("EndRound");
            base.PostRound();
            CanControl = false;
            if (!IsDead)
            {
                Fsm.ChangeState("WIN");
            }
        }

        public void ShowBlockSpark(Vector3 position)
        {
            Vector3 pos = new Vector3(position.x, position.y + 0.6f, position.z + 0.4f);
            Instantiate(sparkPrefab, pos, sparkPrefab.transform.rotation);
        }
        // Target
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
        // Change Sword
        public void DropSword()
        {
            Debug.Log("Drop");
            var handSword = HandSwords[HandSwords.Count - (Lives + 1)];
            handSword.transform.SetParent(null);
            Debug.Log("Hand=" + handSword.name);
            Invoke("GrabSword", 2f);
        }
        public void GrabSword()
        {
            Debug.Log("Grab");
            var handSword = HandSwords[HandSwords.Count - Lives]; // 1,2
            var backSword = BackSwords[BackSwords.Count - Lives]; // 0,1
            handSword.SetActive(true);
            backSword.SetActive(false);
            Debug.Log("Hand=" + handSword + " Back=" + backSword);
        }

        public void ShowDashdust(float yAxis) {
            //Dashdust.SetActive(true);
            //Dashdust.transform.eulerAngles = new Vector3(Dashdust.transform.eulerAngles.x, yAxis, Dashdust.transform.eulerAngles.z);
            //UnityEngine.Debug.Log("DASH " + yAxis);
        }

        public void HideDashdust()
        {
            //Dashdust.SetActive(false);
            //UnityEngine.Debug.Log("HIDE DASH ");
        }

#if UNITY_EDITOR
        void OnGUI()
        {
            string text = "";
            //text +=  input.Debug + "\n";
            text += Fsm.DebugString;
            text += "\n" + "ForwardSpeed = " + Animator.GetFloat("ForwardSpeed").ToString("N2") + " RightSpeed = " + Animator.GetFloat("RightSpeed").ToString("N2");
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
