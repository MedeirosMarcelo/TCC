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
        public List<GameObject> lifeCounters;

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

        // Internal State
        public SwordStance Stance { get; set; }
        public int Lives { get; private set; }
        public bool Ended { get; private set; }
        public bool Defeated { get; private set; }
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
            base.Awake();
            StartingHealth = 2;
            Lives = 3;
            Ended = false;
            Defeated = false;
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

#if UNITY_EDITOR
            currentId += 1;
            guiId = currentId;
#endif
            // Fsm must be last, states will access input, rbody ...
            Fsm = new CharacterFsm(this);
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
            Defeated = true;
            Lives--;
        }
        public override void Reset()
        {
            base.Reset();
            if (Lives > 0)
            {
                /*
                for (int i = 0; i < lifeCounters.Count; i++)
                {
                    lifeCounters[i].SetActive(Lives >= i + 1);
                }*/
                if (Defeated)
                {
                    Fsm.ChangeState("STAND");
                }
                else
                {
                    Fsm.ChangeState("MOVEMENT");
                }
                Ended = false;
                Defeated = false;
            }
        }
        public void End()
        {
            Ended = true;
            game.CheckEndRound();
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
            /*
            var currentSword = lifeCounters[lifeCounters.Count - (Lives + 1)];
            currentSword.transform.SetParent(null);
            Debug.Log("Current=" + currentSword.name);
            */
        }
        public void GrabSword()
        {
            /*
            var nextSword = lifeCounters[lifeCounters.Count - (Lives)];
            var grip = sword.transform.parent;
            nextSword.transform.position = grip.position;
            nextSword.transform.rotation = grip.rotation;
            nextSword.transform.SetParent(grip);
            Debug.Log("Next=" + nextSword);
            */
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
