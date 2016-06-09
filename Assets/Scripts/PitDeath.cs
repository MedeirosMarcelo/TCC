using UnityEngine;

namespace Assets.Scripts
{
    using CharacterController = Character.CharacterController;

    public class PitDeath : MonoBehaviour
    {
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<CharacterController>().Die();
            }
        }
    }
}