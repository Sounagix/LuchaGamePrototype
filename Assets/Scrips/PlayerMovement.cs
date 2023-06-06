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

    private GameObject mesh;

    private CapsuleCollider capsuleCollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        life = GetComponent<Life>();
        sounds = GetComponent<Sounds>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        mesh = transform.GetChild(0).gameObject;
    }

    public void InitJumpAnimEvent()
    {
        if (onGround)
        {
            sounds.PlaySound(SOUND.JUMP);
            animator.SetTrigger("Jump");
        }
    }

    public void PlayerJump()
    {
        if (onGround && !life.IsDeath())
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    public void MovePlayer(Vector2 inputDir)
    {
        Vector3 movDir = new Vector3(inputDir.x, 0, inputDir.y);
        if (!animator.GetBool("Block") && rb.velocity.magnitude < maxSpeed)
        {
            float mSpeed = moveSpeed;
            if (sounds.IsPlaying(SOUND.STEP))
                sounds.PlaySound(SOUND.STEP);
            float xValue = (rb.velocity.x * transform.forward.x) / 7;
            float yValue = (rb.velocity.z * transform.forward.z) / 7;
            if (!onGround)
            {
                mSpeed = moveSpeed / 10;
            }
            if (name == "Player2")
            {
                
            }
            animator.SetFloat("Vertical", xValue);
            animator.SetFloat("Horizontal", yValue);
            rb.AddForce(movDir * mSpeed, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ring"))
        {
            Vector3 startPos = transform.position + -transform.up * capsuleCollider.bounds.size.y / 2;
            Vector3 dir = Vector3.Normalize(collision.GetContact(0).point - startPos);
            print("Enter " + dir);
            onGround = dir.y > 0.5f;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ring"))
        {
            //Vector3 startPos = transform.position + -transform.up * capsuleCollider.bounds.size.y / 2;
            //Vector3 dir = -transform.up;
            //RaycastHit hit;
            //int layer = (1 << 8);
            //onGround = Physics.Raycast(startPos, dir, out hit, 0.1f, layer, QueryTriggerInteraction.Ignore);
            onGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ring"))
        {
            Vector3 startPos = transform.position + -transform.up * capsuleCollider.bounds.size.y / 2;
            Vector3 dir = Vector3.Normalize(collision.GetContact(0).point - startPos);
            onGround = dir.y > 0.5f;
        }
    }


    private void FixedUpdate()
    {
        if (!life.IsDeath())
        {
            transform.forward = Vector3.Normalize(enemy.transform.position - transform.position);
            Quaternion currentRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);
            animator.SetBool("OnGround", onGround);
            float yVel = rb.velocity.y;
            if (yVel > 0.0f)
            {
                yVel = 1.0f;
            }
            else if (yVel < 0.0f)
            {
                yVel = -1.0f;
            }
            else
            {
                yVel = 0.0f;
            }
            animator.SetFloat("UpVelocity", yVel);
            
        }
    }
}
