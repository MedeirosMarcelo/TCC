using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Game
{
    using CharacterController = Character.CharacterController;

    public class CameraController : MonoBehaviour
    {

        public float minDistance = 32f;
        public float maxDistance = 40f;
        public float distance = 32f;

        public float margin = 1f;
        public float lerpAmount = 5f;
        public Rect frustum;

        new Camera camera;
        // Use this for initialization
        void Start()
        {
            camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            float minX = float.MaxValue;
            float minZ = float.MaxValue;
            float maxX = float.MinValue;
            float maxZ = float.MinValue;

            var characters = PlayerManager.GetPlayerList().Select(p => p.Character);
            foreach (var character in characters)
            {
                if (!character.IsDead)
                {
                    Vector3 position = character.transform.position;
                    minX = Mathf.Min(minX, position.x);
                    minZ = Mathf.Min(minZ, position.z);
                    maxX = Mathf.Max(maxX, position.x);
                    maxZ = Mathf.Max(maxZ, position.z);
                }
            }

            frustum = new Rect(minX - margin, minZ - margin, (maxX - minX) + 2 * margin, (maxZ - minZ) + 2 * margin);
            Debug.DrawLine(new Vector3(frustum.x, 1, frustum.y), new Vector3(frustum.xMax, 1, frustum.y));
            Debug.DrawLine(new Vector3(frustum.x, 1, frustum.y), new Vector3(frustum.x, 1, frustum.yMax));
            Debug.DrawLine(new Vector3(frustum.xMax, 1, frustum.y), new Vector3(frustum.xMax, 1, frustum.yMax));
            Debug.DrawLine(new Vector3(frustum.x, 1, frustum.yMax), new Vector3(frustum.xMax, 1, frustum.yMax));

            float height = Mathf.Max(frustum.height, frustum.width / camera.aspect);
            float width = height * camera.aspect;
            distance = height * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float clampedDist = Mathf.Clamp(distance, minDistance, maxDistance);
            Vector3 towards = Quaternion.Euler(camera.transform.rotation.eulerAngles.x - 90, 0, 0) * Vector3.up;
            Debug.DrawLine(Vector3.zero, towards * 3f, Color.blue, 2f);
            Vector3 destination = new Vector3(frustum.center.x, 0, frustum.center.y);
            transform.position = Vector3.Lerp(transform.position, destination + towards * clampedDist, Time.deltaTime * lerpAmount);
        }

    }
}
