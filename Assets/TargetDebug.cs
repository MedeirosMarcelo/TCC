using UnityEngine;

public class TargetDebug : MonoBehaviour {
    const float step = 0.05f;
	void Update () {
        var position = transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            position.z += step;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position.z -= step;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= step;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += step;
        }
        transform.position = position;
	}
}
