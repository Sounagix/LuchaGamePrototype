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

        float distanceToPoint = Vector3.Distance(point, transform.position);
        if (distance > 20 && distanceToPoint < 50)
        {
            Vector3 dir = (point - transform.position).normalized;
            dir.y = 0.0f;
            transform.Translate(-dir * 5 * Time.deltaTime); 
        }
        else if(distance < 20 && distanceToPoint > 20)
        {
            Vector3 dir = (point - transform.position).normalized;
            dir.y = 0.0f;
            transform.Translate(dir * 10 * Time.deltaTime);
        }
        transform.LookAt(point);
        //transform.position = new Vector3(point.x, transform.position.y, transform.position.z);
    }
}
