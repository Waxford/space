using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class TrajectoryIndicator : MonoBehaviour {

    public List<ObjectWithGravity> objectsWithGravity = new List<ObjectWithGravity>();
    private ObjectWithGravity closestGravitationalObjectCache = null;
    private ObjectWithGravity closestGravitationalObject
    {
        get
        {
            if (closestGravitationalObjectCache != null)
            {
                return closestGravitationalObjectCache;
            }
            for (int i = 0; i < objectsWithGravity.Count; ++i)
            {
                if ((closestGravitationalObjectCache == null
                     || (transform.position - objectsWithGravity[i].transform.position).sqrMagnitude < (transform.position - closestGravitationalObjectCache.transform.position).sqrMagnitude)
                    && objectsWithGravity[i].gameObject != gameObject)
                {
                    closestGravitationalObjectCache = objectsWithGravity[i];
                }
            }
            //Debug.Log(gameObject.name + " closest: " + ((closestGravitationalObjectCache != null) ? closestGravitationalObjectCache.gameObject.name : "null"));
            return closestGravitationalObjectCache;
        }
    }

    [SerializeField]
    private float period = 0.05f;
    [SerializeField]
    private int steps = 15;
    [SerializeField]
    private float size = 0.1f;

    private new Rigidbody rigidbody;

    private GameObject[] indications;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        indications = new GameObject[steps];
        GameObject indicatorParent = new GameObject("Trajectory");
        indicatorParent.transform.parent = transform;
        indicatorParent.transform.localPosition = Vector3.zero;
        for (int i = 0; i < steps; ++i)
        {
            indications[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            indications[i].transform.localScale = Vector3.one * size * Camera.main.orthographicSize;
            SphereCollider sc = indications[i].GetComponent<SphereCollider>();
            sc.isTrigger = true;
            Rigidbody r = indications[i].AddComponent<Rigidbody>();
            r.isKinematic = true;
            r.useGravity = false;
            indications[i].transform.parent = indicatorParent.transform;
        }
    }

    void Update()
    {
        closestGravitationalObjectCache = null;
        Vector3 velocity = rigidbody.velocity;
        if (closestGravitationalObject != null)
        {
            velocity -= closestGravitationalObject.rigidbody.velocity;
        }
        Vector3 location = transform.position;
        for (int i = 0; i < steps; ++i)
        {
            indications[i].transform.position = location + velocity * period;
            indications[i].transform.localScale = Vector3.one * size * Camera.main.orthographicSize;
            location = indications[i].transform.position;
            if(closestGravitationalObject != null){
                Vector3 delta = closestGravitationalObject.transform.position - indications[i].transform.position;
                float distance = delta.magnitude;
                float pctGravity = 1f;//-distance / (closestGravitationalObject.GravityRadius);
                velocity += delta.normalized * closestGravitationalObject.gravityAtCenterPoint * pctGravity * period;
            }
        }
    }

    void LateUpdate()
    {
    }

}
