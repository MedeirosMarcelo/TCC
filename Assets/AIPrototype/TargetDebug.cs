using UnityEngine;

public class TargetDebug : MonoBehaviour
{
    const float posotionStep = 0.05f;
    const float rotationStep = 2f;
    void Update()
    {
        var position = transform.position;
        var rotation = transform.rotation;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            position.z += posotionStep;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position.z -= posotionStep;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                rotation *= Quaternion.Euler(0f, -rotationStep, 0f);
            }
            else
            {
                position.x -= posotionStep;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                rotation *= Quaternion.Euler(0f, rotationStep, 0f);
            }
            else
            {
                position.x += posotionStep;
            }
        }
        transform.position = position;
        transform.rotation = rotation;
    }
}
