using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {

    public int controller = 1;
    BoxCollider boxCollider;
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        boxCollider.enabled = false;
    }

	void Update () {

        if (controller == 1)
            ControlP1();
        else
            ControlP2();
	}

    void ControlP1() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            animator.SetInteger("Sword Move", 4);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) {
            animator.SetInteger("Sword Move", 6);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1)) {
            animator.SetInteger("Sword Move", 1);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            animator.SetInteger("Sword Move", 2);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3)) {
            animator.SetInteger("Sword Move", 3);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4)) {
            animator.SetInteger("Sword Move", 4);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5)) {
            animator.SetInteger("Sword Move", 5);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6)) {
            animator.SetInteger("Sword Move", 6);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7)) {
            animator.SetInteger("Sword Move", 7);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8)) {
            animator.SetInteger("Sword Move", 8);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9)) {
            animator.SetInteger("Sword Move", 9);
            boxCollider.enabled = true;
        }
    }

    void ControlP2() {
        if (Input.GetKeyDown(KeyCode.Joystick1Button4)) {
            animator.SetInteger("Sword Move", 4);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button5)) {
            animator.SetInteger("Sword Move", 6);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2)) {
            animator.SetInteger("Sword Move", 7);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button0)) {
            animator.SetInteger("Sword Move", 9);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button1)) {
            animator.SetInteger("Sword Move", 8);
            boxCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button3)) {
            animator.SetInteger("Sword Move", 5);
            boxCollider.enabled = true;
        }
    }

    public void ResetMove(){
        animator.SetInteger("Sword Move", 0);
        boxCollider.enabled = false;
    }
}
