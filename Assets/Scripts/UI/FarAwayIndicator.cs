using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FarAwayIndicator : MonoBehaviour {

    [SerializeField]
    private Transform indicator;
    [SerializeField]
    private float size;
    [SerializeField]
    private float orthoThreshold = 7f;

    void LateUpdate()
    {
        indicator.localScale = Vector3.one * size * Camera.main.orthographicSize;
        indicator.gameObject.SetActive(Camera.main.orthographicSize > orthoThreshold);
    }
}
