using UnityEngine;
using UnityEngine.Assertions;

public class MinionController : MonoBehaviour {

    public Transform Target;

    public int Health { get; private set; }
    public Rigidbody Rbody { get; private set; }
    public MinionFsm Fsm { get; private set; }
    public Animator Animator { get; private set; }
    public MeshRenderer Mesh { get; private set; }
    public CapsuleCollider SwordCollider { get; private set; }
    public NavMeshAgent NavAgent { get; private set; } 

#if UNITY_EDITOR
    int id = 0;
    static int currentId = 0;
#endif

    public void Awake()
    {
        Assert.IsNotNull(Target);

        Health = 2;
        Rbody = GetComponent<Rigidbody>();
        Assert.IsNotNull(Rbody);
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

    public void Update()
    {
        NavAgent.SetDestination(Target.position);
    }

    public void FixedUpdate()
    {
        //Fsm.PreUpdate();
        //Fsm.FixedUpdate();
    }
    public void Move(Vector3 position)
    {
        Rbody.MovePosition(position);
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
            //fsm.ChangeState("DEATH");
        }
    }

#if UNITY_EDITOR
    void OnGUI()
    {
        //string text = Fsm.Debug;
        //GUI.Label(new Rect((id - 1) * (Screen.width / 2), 0, Screen.width / 2, Screen.height), text);
    }
#endif
}