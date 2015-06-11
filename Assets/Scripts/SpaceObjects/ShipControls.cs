using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ShipControls : MonoBehaviour {

    [SerializeField]
    private float shipSpeed = 10f;
    [SerializeField]
    private float rotationSpeed = 45f;
    [SerializeField]
    private new Rigidbody rigidbody;

    void Awake()
    {
        Time.timeScale = 0.05f;
        rigidbody = GetComponent<Rigidbody>();
    }

	void FixedUpdate () {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.AddForce(transform.up * shipSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddForce(-transform.up * shipSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.forward, rotationSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.forward, -rotationSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 0.2f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale = 0.4f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Time.timeScale = 0.8f;
        }
	}
}
