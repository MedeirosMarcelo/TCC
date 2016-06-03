using UnityEngine;

namespace Assets.Scripts
{
    public class SwordSpark : MonoBehaviour
    {
        void Start()
        {
            Destroy(this.gameObject, 0.5f);
        }
    }
}