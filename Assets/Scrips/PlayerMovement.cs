using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    [Min(1.0f)]
    private float moveSpeed = 2.0f;

    [SerializeField]
    [Min(1.0f)]
    private float jumpSpeed = 2.0f;

    [SerializeField]
    [Min(1.0f)]
    private float maxSpeed = 2.0f;

    [SerializeField]
    private Animator animator;


    private bool onGround = false;

    private Rigidbody rb;

    private Life life;

    private Sounds sounds;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        life = GetComponent<Life>();
        sounds = GetComponent<Sounds>();
    }

    public void PlayerJump()
    {
        if (onGround && !life.IsDeath())
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            onGround = false;
        }
    }

    public void MovePlayer(Vector2 inputDir)
    {
        Vector3 movDir = new Vector3(inputDir.x, 0, inputDir.y);
        if (rb.velocity.magnitude < maxSpeed)
        {
            if(sounds.IsPlaying(SOUND.STEP))
                sounds.PlaySound(SOUND.STEP);
            float xValue = rb.velocity.x / 7;
            float yValue = rb.velocity.z / 7;
            animator.SetFloat("Vertical", xValue);
            animator.SetFloat("Horizontal", yValue);
            rb.AddForce(movDir * moveSpeed, ForceMode.Impulse);
        }
        //else
        //{
        //    animator.SetFloat("Vertical", 0);
        //    animator.SetFloat("Horizontal", 0);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ring"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ring"))
        {
            onGround = false;
        }
    }

    private void FixedUpdate()
    {
        if (!life.IsDeath())
        {
            transform.LookAt(enemy.transform);
            Quaternion currentRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);
        }
    }
}
