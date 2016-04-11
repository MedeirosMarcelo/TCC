using UnityEngine;
using System.Collections;

public class SwordSpark : MonoBehaviour {

    void Start() {
        Destroy(this.gameObject, 0.5f);
    }
}
