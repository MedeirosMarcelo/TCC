using UnityEngine;
using System.Collections;

public class charactercontrol3 : MonoBehaviour {

    public float moveSpeed;
    public float turnSpeed = 1f;
    public GameObject target;
    public GameObject sword;
    public int controller = 1;
    Vector3 velocity;
    bool dashing;
    Rigidbody rb;
    ParticleSystem.EmissionModule bloodEmission;
    Animator bloodAnim;
    SwordControl2 swordControl;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        bloodEmission = transform.Find("Blood Fountain").Find("Particle System").GetComponent<ParticleSystem>().emission;
        //bloodEmission.enabled = false;
        bloodAnim = transform.Find("Blood Fountain").GetComponent<Animator>();
        if (sword != null) {
            swordControl = sword.GetComponent<SwordControl2>();
        }
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
            if (target != null) {
                transform.LookAt(target.transform.position);
            }
        }
    }

    void ControlP2() {
        velocity = new Vector3(Input.GetAxis("HorizontalP2"), 0, Input.GetAxis("VerticalP2"));

        Vector3 dir = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.Joystick1Button18)) {
            dashing = true;
            dir = transform.right;
            velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button19)) {
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
                //if (!swordControl.attacking) {
                    transform.forward = Vector3.Lerp(transform.forward, faceDirection, 0.1f * turnSpeed);
              //  }
            }
        }
    }

    void Dash(Vector3 direction) {
        if (dashing) {
            rb.AddForce(direction * 4f, ForceMode.Impulse);
            DashTime();
        }
        else {
            timeLeft = dashTime;
        }
    }

    float dashTime = 0.1f;
    float timeLeft;
    void DashTime() {
        if (dashing) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                dashing = false;
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.name == "Sword") {
            int c = col.transform.parent.GetComponent<SwordController>().controller;
            if (c != controller) {
                bloodAnim.SetTrigger("Bleed");
                StartCoroutine("DelayBlood");
            }
        }
    }

    IEnumerator DelayBlood() {
        yield return new WaitForSeconds(0.25f);
        Paint();
    }

    void Paint() {
        Vector3 pos = transform.position * 1.1f;
        pos.z = transform.position.z - 0.5f;
        pos.y = transform.position.y + 0.1f;
        DecalPainter.Instance.Paint(pos, Color.gray, 10);
    }
}