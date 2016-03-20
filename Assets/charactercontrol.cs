using UnityEngine;
using System.Collections;

public class charactercontrol : MonoBehaviour {

    public float moveSpeed;
    public GameObject target;
    public int controller = 1;
    Vector3 velocity;
    bool dashing;
    Rigidbody rb;
    ParticleSystem.EmissionModule bloodEmission;
    Animator bloodAnim;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        bloodEmission = transform.Find("Blood Fountain").Find("Particle System").GetComponent<ParticleSystem>().emission;
        //bloodEmission.enabled = false;
        bloodAnim = transform.Find("Blood Fountain").GetComponent<Animator>();
    }

    void FixedUpdate() {

        if (controller == 1)
            ControlP1();
        else
            ControlP2();
    }

    void ControlP1() {
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 dir = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.E)) {
            dashing = true;
            dir = transform.right;
            velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            dashing = true;
            dir = -transform.right;
            velocity = Vector3.zero;
        }
        Dash(dir);

        if (!dashing) {
            rb.MovePosition(transform.position + velocity * moveSpeed * Time.deltaTime);
            transform.LookAt(target.transform.position);
        }
    }

    void ControlP2() {
        velocity = new Vector3(Input.GetAxis("HorizontalP2"), 0, Input.GetAxis("VerticalP2"));

        Vector3 dir = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.Joystick1Button8)) {
            dashing = true;
            dir = transform.right;
            velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button9)) {
            dashing = true;
            dir = -transform.right;
            velocity = Vector3.zero;
        }
        Dash(dir);

        if (!dashing) {
            rb.MovePosition(transform.position + velocity * moveSpeed * Time.deltaTime);
            Vector3 faceDirection = Vector3.zero;
            if (Input.GetAxis("HorizontalP2RightStick") != 0) {
                faceDirection.x = Input.GetAxis("HorizontalP2RightStick");
            }
            if (Input.GetAxis("VerticalP2RightStick") != 0) {
                faceDirection.z = Input.GetAxis("VerticalP2RightStick");
            }
            if (faceDirection != Vector3.zero) {
                transform.forward = faceDirection;
            }
        }
    }

    void Dash(Vector3 direction) {
        if (dashing) {
            rb.AddForce(direction * 4f, ForceMode.Impulse);
            print("dashing");
        }
        else {
            //timeLeft = dashTime;
            StartCoroutine("DashDelay");
        }
        // DashTime();
    }

    float dashTime = 0.1f;
    IEnumerator DashDelay() {
        yield return new WaitForSeconds(dashTime);
        dashing = false;
    }

    //float timeLeft;
    //void DashTime() {
    //    if (dashing) {
    //        timeLeft -= Time.deltaTime;
    //        if (timeLeft < 0) {
    //            dashing = false;
    //        }
    //    }
    //}

    void OnTriggerEnter(Collider col) {
        if (col.name == "Sword") {
            Vector3 pos = transform.position * 1.1f;
            pos.z = transform.position.z - 0.5f;
            DecalPainter.Instance.Paint(pos, Color.gray, 10);
            bloodAnim.SetTrigger("Bleed");
        }
    }
}