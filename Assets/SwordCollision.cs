using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class SwordCollision : MonoBehaviour {

    [Header("Children")]
    public GameObject swordCollider;

    Animator animator;

    void Awake() {
        Assert.IsNotNull(swordCollider);
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
