using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luz : MonoBehaviour
{
    [SerializeField]
    private Vector2 speedV;

    [SerializeField]
    private Color[] colors;

    private Vector3 dir;

    private float speed;


    private void Start()
    {
        ChangeRot();
        InvokeRepeating(nameof(ChangeRot), 0, 2);
    }

    private void LateUpdate()
    {
        transform.Rotate(dir * speed * Time.deltaTime);
    }

    private void ChangeRot()
    {
        dir = new Vector3(/*Random.Range(-0.1f, 0.1f)*/0, Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        speed = Random.Range(speedV.x, speedV.y);
    }

}
