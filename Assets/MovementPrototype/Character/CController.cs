using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum PlayerIndex : int
{
    One = 1,
    Two = 2,
}

public class CController : MonoBehaviour
{
    public PlayerIndex joystick = PlayerIndex.One;
    public bool canControl = true;
    public int health = 3;

    public GameObject opponent;
    public Animator bloodAnimator;
    public Material baseMaterial;
    public Material dodgeMaterial;
    public GameObject blockSpark;
    public BaseInput input { get; private set; }
    public Rigidbody rbody { get; private set; }
    public CFsm fsm { get; private set; }
    public Animator animator { get; private set; }

    MeshRenderer mesh;

    public void Awake()
    {
        input = new GamePadInput(joystick);
        rbody = GetComponent<Rigidbody>();
        fsm = new CFsm(this);
        animator = GetComponent<Animator>();
        mesh = transform.Find("Model").GetComponent<MeshRenderer>();
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

    float maxTurnSpeed = Mathf.PI / 30;
    public void Look(float lookTurnRate = 1f, float lockTurnRate = 1f)
    {

        if (input.run > 0.25f || opponent == null)
        {
            if (input.move.vector.magnitude > 0.25)
            {
                transform.forward = Vector3.RotateTowards(
                    transform.forward,
                    input.move.vector,
                    maxTurnSpeed * lookTurnRate,
                    1f);
            }
        }
        else
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
                (opponent.transform.position - transform.position).normalized,
                maxTurnSpeed * lockTurnRate,
                1f);
 
            }
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
        GUI.Label(new Rect(((int)id - 1) * (Screen.width / 2), 0, Screen.width / 2, Screen.height), text);
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
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
        mesh.material = baseMaterial;
    }

    public void ApplyDodgeMaterial()
    {
        mesh.material = dodgeMaterial;
    }

    public void ShowBlockSpark(Vector3 position)
    {
        Vector3 pos = new Vector3(position.x, position.y + 0.6f, position.z + 0.4f);
        Instantiate(blockSpark, pos, blockSpark.transform.rotation);
    }
}
