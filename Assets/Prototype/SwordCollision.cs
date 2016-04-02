using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class SwordCollision : MonoBehaviour {

    public bool realisticCollision = true;
    public bool useSphereCast;
    public float sphereRadius = 2f;
    public float sphereDistance = 2f;
    public Transform baseObj;
    public Transform tipObj;
    public bool drawDebug;
    int layerMask = 1 << 8;
    Animator animator;

    void Awake() {
        animator = transform.parent.GetComponent<Animator>();
        layerMask = ~layerMask;
    }

    void Update() {
        SphereCastCheck();
    }

    void OnTriggerEnter(Collider col) {
        if (!useSphereCast) {
            if (col.tag == "Sword") {
                Hit();
            }
        }
    }

    void SphereCastCheck() {
        if (useSphereCast) {
            RaycastHit rayHit;
            Debug.DrawRay(baseObj.position, (tipObj.position - baseObj.position).normalized * sphereDistance, Color.blue);
            //if (Physics.SphereCast(baseObj.position, sphereRadius, (tipObj.position - baseObj.position).normalized, out rayHit, sphereDistance, layerMask, QueryTriggerInteraction.Collide)) {
            if (Physics.CapsuleCast(baseObj.position, tipObj.position, sphereRadius, (tipObj.position - baseObj.position).normalized, out rayHit, sphereDistance, layerMask, QueryTriggerInteraction.Collide)) {
                if (rayHit.collider.tag == "Sword" && rayHit.collider.gameObject != this.gameObject) {
                    print("SPHERE CAST!");
                    Hit();
                }
            }
        }
    }

    void Hit() {
        if (realisticCollision) {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play("Slash Right Back", 0, currentState.normalizedTime);
            animator.SetTrigger("Spark");
            StartCoroutine("DelayAttack");
        }
    }

    void OnDrawGizmos() {
#if UNITY_EDITOR
        if (drawDebug) {
            Gizmos.DrawSphere(baseObj.position, sphereRadius);
            Gizmos.DrawSphere(tipObj.position, sphereRadius);
        }
#endif
    }
}
