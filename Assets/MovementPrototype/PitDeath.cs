using UnityEngine;
using System.Collections;

public class PitDeath : MonoBehaviour
{

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<CharController>().fsm.ChangeState("DEATH");
        }
    }
}
