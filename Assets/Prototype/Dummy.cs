using UnityEngine;
using System.Collections;

public class Dummy : MonoBehaviour {

    public float resetDelay = 3f;
    MeshRenderer mesh;
    float timeLeft;

    void Start () {
        mesh = GetComponent<MeshRenderer>();
	}


    void Update() {
        ResetDelay();
    }

    void OnTriggerEnter(Collider col) {
        if (col.name == "Sword") {
            mesh.enabled = false;
            timeLeft = resetDelay;
        }
    }

    void ResetDelay() {
        if (mesh.enabled == false) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                mesh.enabled = true;
            }
        }
    }
}
