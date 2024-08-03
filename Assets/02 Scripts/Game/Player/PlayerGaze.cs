using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGaze : MonoBehaviour
{
    private Vector3 offsetPosition;
    private Ray playerRay;
    private RaycastHit playerRayHit;

    private void Update()
    {
        playerRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(playerRay, out playerRayHit, Mathf.Infinity, LayerMask.GetMask("Floor"))) return;

        offsetPosition = new Vector3(playerRayHit.point.x, transform.position.y, playerRayHit.point.z);

        transform.LookAt(offsetPosition);
    }
}
