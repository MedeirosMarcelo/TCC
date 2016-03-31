using UnityEngine;
using System.Collections;

public class SwordControl1 : MonoBehaviour {

    public int controller = 1;
    public bool autoAttack;
    BoxCollider boxCollider;
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        /*
        if (transform.parent != null) {
            controller = transform.parent.GetComponent<charactercontrol>().controller;
            boxCollider.enabled = false;
        }
        */
    }

    void Update() {

        if (controller == 1)
            ControlP1();
        else
            ControlP2();

        if (autoAttack) {
            AutoAttack();
        }
    }

    void ControlP1() {

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (Input.GetAxis("Horizontal") < -0.2f) {
                animator.SetTrigger("Slash Left");
            }
            else if (Input.GetAxis("Horizontal") > 0.2f) {
                animator.SetTrigger("Slash Right");
            }
            else if (Input.GetAxis("Vertical") < -0.2f) {
                animator.SetTrigger("Slash Up");
            }
            else {
                animator.SetTrigger("Slash Down");
            }
        }


        if (Input.GetKeyDown(KeyCode.R)) {
            animator.SetTrigger("Block Up");
            // boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.F)) {
            animator.SetTrigger("Block Mid");
            //   boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1)) {
            animator.SetTrigger("Slash Down Left");
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            animator.SetTrigger("Slash Up");
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3)) {
            animator.SetTrigger("Slash Down Right");
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4)) {
            animator.SetTrigger("Slash Left");
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5)) {
            animator.SetTrigger("Slash Up");
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6)) {
            animator.SetTrigger("Slash Right");
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7)) {
            animator.SetTrigger("Slash Up Left");
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8)) {
            animator.SetTrigger("Slash Down");
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9)) {
            animator.SetTrigger("Slash Up Right");
            boxCollider.enabled = true;
        }
    }

    //void ControlP2() {
    //    if (Input.GetKeyDown(KeyCode.Joystick1Button4)) {
    //        animator.SetTrigger("Slash Left");
    //        boxCollider.enabled = true;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Joystick1Button5)) {
    //        animator.SetTrigger("Slash Right");
    //        boxCollider.enabled = true;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Joystick1Button2)) {
    //        animator.SetTrigger("Slash Up Left");
    //        boxCollider.enabled = true;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Joystick1Button0)) {
    //        animator.SetTrigger("Slash Up Right");
    //        boxCollider.enabled = true;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Joystick1Button1)) {
    //        animator.SetTrigger("Slash Down");
    //        boxCollider.enabled = true;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Joystick1Button3)) {
    //        animator.SetTrigger("Slash Up");
    //        boxCollider.enabled = true;
    //    }
    //}

    void ControlP2() {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5)) {
            if (Input.GetAxis("HorizontalP2") < -0.2f) {
                animator.SetTrigger("Slash Left");
            }
            else if (Input.GetAxis("HorizontalP2") > 0.2f) {
                animator.SetTrigger("Slash Right");
            }
            else if (Input.GetAxis("VerticalP2") < -0.2f) {
                animator.SetTrigger("Slash Up");
            }
            else {
                animator.SetTrigger("Slash Down");
            }
        }
    }

    bool attacking;
    void AutoAttack() {
        if (!attacking)
            StartCoroutine("DelayAttack");
    }

    IEnumerator DelayAttack() {
        attacking = true;
        animator.SetTrigger("Slash Right");
        boxCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    public void ResetMove() {
        animator.SetFloat("Sword Move", 0);
        //  boxCollider.enabled = false;
    }
}
