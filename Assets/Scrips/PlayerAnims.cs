using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack playerAttack;

    [SerializeField]
    private PlayerMovement playerMovement;

    private Animator animator;

    private Sounds sounds;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        sounds = playerAttack.gameObject.GetComponent<Sounds>();
    }
    

    public void AttackAnimEvent()
    {
        sounds.PlaySound(SOUND.ATTACK);
        playerAttack.Attack();
    }

    public void BlockingAnimEvent()
    {
        if (animator.GetBool("Block"))
        {
            animator.SetFloat("BlockSpeed", 0.0f);
        }
    }

    public void EndJumpAnimEvent()
    {
        playerMovement.PlayerJump();
    }

    public void EndAttackingAnimEvent()
    {
        playerAttack.EndAttackAnim();
    }
}
