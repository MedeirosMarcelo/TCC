using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public enum PlayerIndex : int
{
    One = 1,
    Two = 2,
}
public enum SwordStance
{
    Left,
    Right,
    High
}


public class CharController : MonoBehaviour
{
    public PlayerIndex joystick = PlayerIndex.One;
    public bool canControl = true;
    public int health = 3;

    public GameObject target;
    public Animator bloodAnimator;
    public Material baseMaterial;
    public Material dodgeMaterial;
    public GameObject blockSpark;

    public BaseInput input { get; private set; }
    public Rigidbody rbody { get; private set; }
    public CharFsm fsm { get; private set; }
    public Animator animator { get; private set; }
    public Xft.XWeaponTrail SwordTrail { get; private set; }
    public MeshRenderer Mesh { get; private set; }
    public CapsuleCollider AttackCollider { get; private set; }
    public BoxCollider BlockMidCollider { get; private set; }
    public BoxCollider BlockHighCollider { get; private set; }
    // Dash state data
    public Vector3 DashVelocity { get; set; }
    public string MovementState { get; set; }
    public SwordStance Stance { get; set; }
    float maxTurnSpeed = Mathf.PI / 30;

    public void Awake()
    {
        input = new GamePadInput(joystick);
        fsm = new CharFsm(this);

        Stance = SwordStance.Right;
        rbody = GetComponent<Rigidbody>();
        Assert.IsNotNull(rbody);
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);
        Mesh = transform.Find("Model").GetComponent<MeshRenderer>();
        Assert.IsNotNull(Mesh);

        // Colliders
        Assert.IsNotNull(AttackCollider);
        BlockMidCollider = transform.Find("Sword").Find("Block Mid Collider").GetComponent<BoxCollider>();
        Assert.IsNotNull(BlockMidCollider);
        BlockHighCollider = transform.Find("Sword").Find("Block High Collider").GetComponent<BoxCollider>();
        Assert.IsNotNull(BlockHighCollider);

        // Trail init
        SwordTrail = transform.Find("Sword").Find("X-WeaponTrail").GetComponent<Xft.XWeaponTrail>();
        Assert.IsNotNull(SwordTrail);
        AttackCollider = transform.Find("Sword").Find("Attack Collider").GetComponent<CapsuleCollider>();
        SwordTrail.Init();
        SwordTrail.Deactivate();

        currentId += 1;
        id = currentId;
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

    public void ChangeVelocity(Vector3 velocity)
    {
        rbody.velocity = velocity;
    }

    public void Move(Vector3 position)
    {
        rbody.MovePosition(position);
    }

    public void LookForward(float lookTurnRate = 1f)
    {
        transform.forward = Vector3.RotateTowards(
                transform.forward,
                input.move.vector,
                maxTurnSpeed * lookTurnRate,
                1f);
    }

    public void Look(float lookTurnRate = 1f, float lockTurnRate = 1f)
    {
        var vec = input.look.vector;
        if (vec.magnitude > 0.25)
        {
            transform.forward = Vector3.RotateTowards(
                transform.forward,
                vec,
                maxTurnSpeed * lookTurnRate,
                1f);
        }
        else
        {
            transform.forward = Vector3.RotateTowards(
                transform.forward,
                (target.transform.position - transform.position).normalized,
                maxTurnSpeed * lockTurnRate,
                1f);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        fsm.OnTriggerEnter(collider);
    }

    int id = 0;
    static int currentId = 0;
    void OnGUI()
    {
        string text = input.Debug + "\n" + fsm.Debug;
        GUI.Label(new Rect((id - 1) * (Screen.width / 2), 0, Screen.width / 2, Screen.height), text);
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        StartCoroutine("DelayBlood");
        if (health <= 0)
        {
            fsm.ChangeState("DEATH");
            StartCoroutine("RestartLevel");
        }
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
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
}
