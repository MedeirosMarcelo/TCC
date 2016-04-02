using UnityEngine;
using System.Collections;

public enum CombatState {
    Idle,
    AttackingMid,
    AttackingUp,
    BlockingMid,
    BlockingUp
}

public class SwordControl2 : MonoBehaviour {

    public int controller = 1;
    public ControlScheme controlScheme = ControlScheme.LeftStick;
    public bool autoAttack;
    public CombatState state;
    public bool attacking;
    BoxCollider boxCollider;
    Animator animator;

    public enum ControlScheme {
        LeftStick,
        RightStick
    }

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

        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            animator.SetTrigger("Block Mid");
            state = CombatState.BlockingMid;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) {
            animator.SetTrigger("Block Up");
            state = CombatState.BlockingUp;
        }
        else {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (Input.GetAxis("Horizontal") < -0.2f) {
                    animator.SetTrigger("Slash Left");
                    state = CombatState.AttackingMid;
                }
                else if (Input.GetAxis("Horizontal") > 0.2f) {
                    animator.SetTrigger("Slash Right");
                    state = CombatState.AttackingMid;
                }
                else if (Input.GetAxis("Vertical") < -0.2f) {
                    animator.SetTrigger("Slash Up");
                    state = CombatState.AttackingUp;
                }
                else {
                    animator.SetTrigger("Slash Down");
                    state = CombatState.AttackingUp;
                }
                attacking = true;
            }
        }
    }

    void ControlP2() {

        if (Input.GetKeyDown(KeyCode.JoystickButton0)) {
            animator.SetTrigger("Block Mid");
            state = CombatState.BlockingMid;
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton3)) {
            animator.SetTrigger("Block Up");
            state = CombatState.BlockingUp;
        }
        else {
            if (controlScheme == ControlScheme.LeftStick) {
                if (Input.GetKeyDown(KeyCode.JoystickButton5)) {
                    if (Input.GetAxis("HorizontalP2") < -0.2f) {
                        animator.SetTrigger("Slash Left");
                        state = CombatState.AttackingMid;
                    }
                    else if (Input.GetAxis("HorizontalP2") > 0.2f) {
                        animator.SetTrigger("Slash Right");
                        state = CombatState.AttackingMid;
                    }
                    else if (Input.GetAxis("VerticalP2") < -0.2f) {
                        animator.SetTrigger("Slash Up");
                        state = CombatState.AttackingUp;
                    }
                    else {
                        animator.SetTrigger("Slash Down");
                        state = CombatState.AttackingUp;
                    }
                    attacking = true;
                }
            }
            else {
                if (Input.GetKeyDown(KeyCode.JoystickButton5)) {
                    if (Input.GetAxis("HorizontalP2RightStick") < -0.2f) {
                        animator.SetTrigger("Slash Left");
                        state = CombatState.AttackingMid;
                    }
                    else if (Input.GetAxis("HorizontalP2RightStick") > 0.2f) {
                        animator.SetTrigger("Slash Right");
                        state = CombatState.AttackingMid;
                    }
                    else if (Input.GetAxis("VerticalP2RightStick") < -0.2f) {
                        animator.SetTrigger("Slash Up");
                        state = CombatState.AttackingUp;
                    }
                    else {
                        animator.SetTrigger("Slash Down");
                        state = CombatState.AttackingUp;
                    }
                    attacking = true;
                }
            }
        }
    }

    public void Spark() {
        animator.SetTrigger("Spark");
    }

    void AutoAttack() {
        if (attacking)
            StartCoroutine("DelayAttack");
    }

    IEnumerator DelayAttack() {
        state = CombatState.AttackingMid;
        attacking = true;
        animator.SetTrigger("Slash Right");
        boxCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        state = CombatState.Idle;
        attacking = false;
    }

    public void ResetMove() {
        animator.SetFloat("Sword Move", 0);
        state = CombatState.Idle;
        attacking = false;
        //  boxCollider.enabled = false;
    }
}
