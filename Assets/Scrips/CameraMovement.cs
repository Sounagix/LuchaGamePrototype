using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;

    [SerializeField]
    private GameObject player2;

    void Update()
    {
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);
        Vector3 point = player1.transform.position + (player1.transform.forward * distance / 2);

        transform.LookAt(point);
        if (distance > 20 && transform.position.y < 40.0f)
        {
            transform.position = transform.position + (Vector3.up * 10 * Time.deltaTime);
        }
        else if(distance < 20 && transform.position.y > 15.0f)
        {
            transform.position = transform.position + (-Vector3.up * 10 * Time.deltaTime);
        }
    }

    private float GetDistanceToGround()
    {
        int layer = (1 << 8);
        Vector3 dir = Vector3.down;
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, dir, out raycastHit, 100.0f, layer, QueryTriggerInteraction.Ignore)) 
        {
            return Vector3.Distance(transform.position, raycastHit.point);
        }
        return -1.0f;
    }
}
