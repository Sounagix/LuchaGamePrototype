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
        //transform.position = new Vector3(point.x, transform.position.y, transform.position.z);
    }
}
