using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundBackgroundView : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 5.0f;

    private float runningTime;

    // Update is called once per frame
    void Update()
    {
        runningTime += Time.deltaTime * cameraSpeed;
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(45.0f,
                                                                      Mathf.Sin(runningTime) * 26.6f,
                                                                      0.0f));
    }
}
