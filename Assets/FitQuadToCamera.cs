using UnityEngine;

[ExecuteAlways]
public class FitQuadToCamera : MonoBehaviour
{
    public Camera cam;
    public float distance = 10f;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        Fit();
    }

    void LateUpdate()
    {
        Fit();
    }

    void Fit()
    {
        if (cam == null) return;

        // Make the Quad face the camera
        transform.rotation = cam.transform.rotation;

        // Position it in front of the camera at given distance
        transform.position = cam.transform.position + cam.transform.forward * distance;

        float height, width;

        if (cam.orthographic)
        {
            height = cam.orthographicSize * 2f;
            width  = height * cam.aspect;
        }
        else
        {
            float fovRad = cam.fieldOfView * Mathf.Deg2Rad;
            height = 2f * distance * Mathf.Tan(fovRad * 0.5f);
            width  = height * cam.aspect;
        }

        transform.localScale = new Vector3(width, height, 1f);
    }
}