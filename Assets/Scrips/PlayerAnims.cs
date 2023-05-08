using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack playerAttack;

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
}
