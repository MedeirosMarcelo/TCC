using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

    public float minDistance = 32f;
    public float maxDistance = 40f;
    public float distance = 32f;
    public Vector3 positionOffset;
    public float margin = 1f;
    public float lerpAmount = 5f;   
    public Rect frustum;

    GameManager gameManager;
    Camera camera;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        float minX = float.MaxValue;
        float minZ = float.MaxValue;
        float maxX = float.MinValue;
        float maxZ = float.MinValue;

        foreach (CharController character in gameManager.characterList)
        {
            Vector3 position = character.transform.position;
            minX = Mathf.Min(minX, position.x);
            minZ = Mathf.Min(minZ, position.z);
            maxX = Mathf.Max(maxX, position.x);
            maxZ = Mathf.Max(maxZ, position.z);
        }

        frustum = new Rect(minX - margin, minZ - margin, (maxX - minX) + 2*margin, (maxZ - minZ) + 2*margin);
        Debug.DrawLine(new Vector3(frustum.x, 1, frustum.y), new Vector3(frustum.xMax, 1, frustum.y));
        Debug.DrawLine(new Vector3(frustum.x, 1, frustum.y), new Vector3(frustum.x, 1, frustum.yMax));
        Debug.DrawLine(new Vector3(frustum.xMax, 1, frustum.y), new Vector3(frustum.xMax, 1, frustum.yMax));
        Debug.DrawLine(new Vector3(frustum.x, 1, frustum.yMax), new Vector3(frustum.xMax, 1, frustum.yMax));

        float height = Mathf.Max(frustum.height, frustum.width / camera.aspect);
        float width = height * camera.aspect;
        distance = height * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float clampedDist = Mathf.Clamp(distance, minDistance, maxDistance);
        transform.position = Vector3.Lerp(transform.position, new Vector3(frustum.center.x, clampedDist, frustum.center.y), Time.deltaTime * lerpAmount);
	}
}
