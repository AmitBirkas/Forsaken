using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowMouse : MonoBehaviour
{

    new Transform transform = null;
    new Camera camera = null;

    void Start()
    {
        transform = gameObject.transform;
        camera = GameObject.FindObjectOfType<Camera>();
    }

    void Update()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        transform.position = mousePos;
    }
}
