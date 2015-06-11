using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(SphereCollider))]
public class ObjectWithGravity : MonoBehaviour {

    private static List<ObjectWithGravity> objectsWithGravity;
    public static List<ObjectWithGravity> AllObjectsWithGravity
    {
        get
        {
            if (objectsWithGravity == null)
            {
                objectsWithGravity = new List<ObjectWithGravity>();
            }
            return objectsWithGravity;
        }
    }

    public float GravityRadius
    {
        get
        {
            return collider.radius * transform.localScale.x;
        }
    }

    public float gravityAtCenterPoint = -9f;
    public new SphereCollider collider;
    public new Rigidbody rigidbody;
    [SerializeField]
    private List<ObjectWithMass> massObjects = new List<ObjectWithMass>();

    protected virtual void Awake()
    {
        AllObjectsWithGravity.Add(this);
        collider = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.useGravity = false;
        }
    }

    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, GravityRadius);
        foreach (Collider c in colliders)
        {
            OnTriggerEnter(c);
        }
    }

    void FixedUpdate()
    {
        foreach (ObjectWithMass o in massObjects)
        {
            Vector3 delta = this.transform.position - o.transform.position;
            float distance = (delta).magnitude;
            float pctGravity = 1f;// -distance / GravityRadius;
            o.rigidbody.AddForce(delta.normalized * gravityAtCenterPoint * pctGravity * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    void Update()
    {
        //List<ObjectWithMass> toRemove = new List<ObjectWithMass>();
        //foreach (ObjectWithMass o in massObjects)
        //{
        //    if (o == null || (!Util.QuickSphereCheck(o.transform.position, transform.position, GravityRadius) && massObjects.Contains(o)))
        //    {
        //        toRemove.Add(o);
        //        TrajectoryIndicator ti = o.GetComponent<TrajectoryIndicator>();
        //        if (ti != null && ti.objectsWithGravity.Contains(this))
        //        {
        //            ti.objectsWithGravity.Remove(this);
        //        }
        //    }
        //}
        //foreach (ObjectWithMass o in toRemove)
        //{
        //    massObjects.Remove(o);
        //}
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.isTrigger)
        {
            return;
        }
        ObjectWithMass o = c.GetComponent<ObjectWithMass>();
        if (o == null)
        {
            o = c.GetComponentInParent<ObjectWithMass>();
        }
        if (o != null 
            && o.gameObject != gameObject
            && !massObjects.Contains(o))
        {
            massObjects.Add(o);
            TrajectoryIndicator ti = o.GetComponent<TrajectoryIndicator>();
            if (ti != null && !ti.objectsWithGravity.Contains(this))
            {
                ti.objectsWithGravity.Add(this);
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.isTrigger)
        {
            return;
        }
        ObjectWithMass o = c.GetComponent<ObjectWithMass>();
        if (o == null)
        {
            o = c.GetComponentInParent<ObjectWithMass>();
        }
        if (o != null
            && massObjects.Contains(o))
        {
            massObjects.Remove(o);
            TrajectoryIndicator ti = o.GetComponent<TrajectoryIndicator>();
            if (ti != null && ti.objectsWithGravity.Contains(this))
            {
                ti.objectsWithGravity.Remove(this);
            }
        }
    }

    void OnDisable()
    {
        AllObjectsWithGravity.Remove(this);
    }
}
