using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private string enemyTag;

    [SerializeField]
    private int enemyLayer;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private float damage;

    [SerializeField]
    private Animator animator;

    private bool isBlocking = false;

    private bool attacking = false;

    public void Attack()
    {
        int layer = (1 << enemyLayer | 1 << 9);
        RaycastHit hit;
        Vector3 direction = transform.forward;
        
         Debug.DrawRay(transform.position, transform.position + (direction * attackRange), Color.blue, 1.0f);
        if (Physics.Raycast(transform.position, direction, out hit, attackRange, layer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag(enemyTag) && !hit.collider.GetComponent<Life>().IsDeath())
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 1.0f);                
                bool killer = hit.collider.GetComponent<Life>().DamageLife(damage);
                if (killer)
                {
                    animator.SetTrigger("Dance");
                }
            }
            else if (hit.collider.CompareTag("Caja"))
            {
                hit.collider.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
            }
        }
    }

    public void KickAttack()
    {
        int layer = (1 << enemyLayer);
        RaycastHit hit;
        Vector3 direction = transform.forward;

        // Debug.DrawRay(transform.position, transform.position + (direction * attackRange), Color.blue, 1.0f);
        if (Physics.Raycast(transform.position, direction, out hit, attackRange, layer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag(enemyTag) && !hit.collider.GetComponent<Life>().IsDeath())
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 1.0f);
                bool killer = hit.collider.GetComponent<Life>().DamageLife(damage);
                if (killer)
                {
                    animator.SetTrigger("Dance");
                }
            }
            else if (hit.collider.CompareTag("Caja"))
            {
                hit.collider.GetComponent<Rigidbody2D>().AddForce(transform.forward * 5, ForceMode2D.Impulse);
            }
        }
    }

    public void AttackAnim()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
        }
    }

    public void KickAnim()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Kick");
        }
    }

    public void EndAttackAnim()
    {
        attacking = false;
    }

    public void InitBlock()
    {
        isBlocking = true;
        animator.SetBool("Block", true);
    }

    public void EndBlock()
    {
        isBlocking = false;
        animator.SetFloat("BlockSpeed", 1.0f);
        animator.SetBool("Block", false);
    }

    public bool IsPlayerBlocking()
    {
        return isBlocking;
    }
}
