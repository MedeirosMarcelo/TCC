using UnityEngine;
using System.Collections;

public class SwordControl3 : MonoBehaviour {

    public int controller = 1;
    public bool autoAttack;
    public bool attacking;
    public CombatState state;
    BoxCollider boxCollider;
    Animator animator;
    Stance stance;

    enum Stance {
        Right,
        Left,
        Up
    }

    void Start() {
        stance = Stance.Right;
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

        if (Input.GetKeyDown(KeyCode.F)) {
            if (stance == Stance.Right) stance = Stance.Left;
            else if (stance == Stance.Left) stance = Stance.Up;
            else if (stance == Stance.Up) stance = Stance.Right;
            animator.SetTrigger("Idle " + stance.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.Q)) {
            animator.SetTrigger("Block Mid");
            state = CombatState.BlockingMid;
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            animator.SetTrigger("Block Up");
            state = CombatState.BlockingUp;
        }
        else {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                animator.SetTrigger("Attack");
                if (stance == Stance.Up) {
                    state = CombatState.AttackingUp;
                }
                else {
                    state = CombatState.AttackingMid;
                }
                attacking = true;
            }
        }
    }

    void ControlP2() {
        if (Input.GetKeyDown(KeyCode.JoystickButton5)) {
            animator.SetTrigger("Attack");
            if (stance == Stance.Up) {
                state = CombatState.AttackingUp;
            }
            else {
                state = CombatState.AttackingMid;
            }
            attacking = true;
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton4)) {
            if (stance == Stance.Right) stance = Stance.Left;
            else if (stance == Stance.Left) stance = Stance.Up;
            else if (stance == Stance.Up) stance = Stance.Right;
            animator.SetTrigger("Idle " + stance.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton1)) {
            if (stance == Stance.Right) stance = Stance.Up;
            else if (stance == Stance.Left) stance = Stance.Right;
            else if (stance == Stance.Up) stance = Stance.Left;
            animator.SetTrigger("Idle " + stance.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton0)) {
            animator.SetTrigger("Block Mid");
            state = CombatState.BlockingMid;
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton3)) {
            animator.SetTrigger("Block Up");
            state = CombatState.BlockingUp;
        }
    }

    public void Spark() {
        animator.SetTrigger("Spark");
    }

    void AutoAttack() {
        if (!attacking)
            StartCoroutine("DelayAttack");
    }

    IEnumerator DelayAttack() {
        attacking = true;
        animator.SetTrigger("Slash Right");
        boxCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        state = CombatState.Idle;
        attacking = false;
    }

    public void ResetMove() {
        animator.SetFloat("Sword Move", 0);
        attacking = false;
        state = CombatState.Idle;
        if (stance == Stance.Right) stance = Stance.Left;
        else if (stance == Stance.Left) stance = Stance.Right;
        else if (stance == Stance.Up) stance = Stance.Up;
        //animator.SetTrigger("Sword " + stance.ToString());
        //  boxCollider.enabled = false;
    }
}
