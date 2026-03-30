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
    private WeaponSystem weaponSystem;
    private Transform currentWeapon;
    private Collider currentWeaponCollider;
    
    
    private bool canCombo = false;
    private bool inputBuffered = false;
    
    public int comboStep = 0;
    public bool IsAttacking;
    public bool EnableAttacking = true;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerParry = GetComponent<PlayerParry>();
        weaponSystem = GetComponent<WeaponSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = weaponSystem.CurrentWeapon;
        currentWeaponCollider = currentWeapon.GetComponentInChildren<Collider>();
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J)&&!playerMove.IsRolling&&!player.StaminaEmpty&&!playerParry.IsDefensing)
        {
           
            if (IsAttacking)
            {
                inputBuffered = true;
            }
            else
            {
                StartAttack();
            }
            
        }
    }

    void StartAttack()
    {
        var weapon = currentWeapon.GetComponent<Weapon>();
    
        animator.SetFloat("AttackSpeed", weapon.AttackSpeed);
        

        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        comboStep = 1;
        if (comboStep == 1)
        {
            animator.SetTrigger("Combo1");
            player.ConsumeStamina(weapon.ConsumingStamina);
        }
        IsAttacking = true;
    }

    public void EnableCombo()
    {
        canCombo = true;

        if (inputBuffered)
        {
            inputBuffered = false;
            DoNextCombo();
        }
    }
    
    void DoNextCombo()
    {
        var weapon = currentWeapon.GetComponent<Weapon>();
        if (!canCombo) return;

        comboStep++;
        
        if(comboStep==2)
        {
            animator.SetTrigger("Combo2");
            player.ConsumeStamina(weapon.ConsumingStamina);
        }
        else if(comboStep==3)
        {
            animator.SetTrigger("Combo3");
            player.ConsumeStamina(weapon.ConsumingStamina);
        }
        
        canCombo = false;
    }
    
    public void ResetCombo()
    {
        animator.SetInteger("ComboIndex", 0);
        comboStep = 0;
        IsAttacking = false;
        canCombo = false;
        inputBuffered = false;
        Debug.Log("Combo Reset");
    }
    
    public void SetIsAttacking()
    {
        IsAttacking = true;
    }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    public void EnableWeapon()
    {
        Debug.Log("Weapon On");
        //Debug.Log("开启的Collider是: " + currentWeaponCollider.name);
        currentWeaponCollider.enabled = true;
    }

    public void DisableWeapon()
    {
        
        currentWeaponCollider.enabled = false;
    }
}
