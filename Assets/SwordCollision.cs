using UnityEngine;
using System.Collections;

public class SwordCollision : MonoBehaviour {

    Animator animator;

    void Start() {
        animator = transform.parent.GetComponent<Animator>();
	}

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Sword") {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play("Slash Right Back", 0, currentState.normalizedTime);
            animator.SetTrigger("Spark");
        }
    }
}
