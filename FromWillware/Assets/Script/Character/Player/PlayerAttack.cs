using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private PlayerMove playerMove;
    private Rigidbody rb;
    private Player player;
    private PlayerParry playerParry;
    [SerializeField]
    private ConsumingStamina consumingStamina;
    
    public bool IsAttacking;

    public bool EnableAttacking = true;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        consumingStamina = GetComponentInChildren<ConsumingStamina>();
        player = GetComponent<Player>();
        playerParry = GetComponent<PlayerParry>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J)&&!playerMove.IsRolling&&!player.StaminaEmpty&&!playerParry.IsDefensing)
        {
            player.ConsumeStamina(consumingStamina.consumingStamina);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            animator.SetTrigger("OutSlash");
        }
            
    }

    public void SetIsAttacking()
    {
        IsAttacking = true;
    }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }
}
