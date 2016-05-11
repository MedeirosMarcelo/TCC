using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.Utility;

public class MinionController : MonoBehaviour
{
    public Transform Target;
    public int Health { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
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
            //fsm.ChangeState("DEATH");
        }
    }

    // Target follow stuff
    Vector3 destination;
    public Vector3 Destination
    {
        get { return destination; }
        set
        {
            destination = value;
            print("SetDestination = " + destination);
            NavAgent.SetDestination(destination);
        }
    }


    float targetRange = 0.4f;
    float destinationRange = 0.3f;
    bool targetReached
    {
        get { return InRange(Target.position, targetRange); }
    }
    bool destinationReached
    {
        get { return InRange(destination, destinationRange); }
    }

    bool InRange(Vector3 target, float range)
    {
        var diff = (target - transform.position);
        diff.y = 0f;
        return (diff.magnitude < destinationRange);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (destination != null)
        {
            Gizmos.color = Color.blue;
            var start = transform.position;
            var end = destination;
            start.y = 1f;
            end.y = 1f;
            Gizmos.DrawLine(start, end);
        }
    }
    void OnGUI()
    {
        string text = Fsm.Debug;
        GUI.Label(new Rect((id - 1) * (Screen.width / 2), 0, Screen.width / 2, Screen.height), text);
    }
#endif
}