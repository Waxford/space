using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FollowShip : MonoBehaviour
{

    private static readonly Vector3 OFFSET = Vector3.back*400f;

    [SerializeField]
    private Transform toFollow;
    [SerializeField]
    private float scrollSpeed = 0.1f;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= cam.orthographicSize * scroll * scrollSpeed;
    }

    void LateUpdate()
    {
        transform.position = toFollow.position + OFFSET;
        transform.rotation = Quaternion.identity;
    }

}
