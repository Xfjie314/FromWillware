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
    
    public bool IsAttacking;
    public bool EnableAttacking = true;

    public bool EnableCombo1 = true;
    public bool EnableCombo2;
    public bool EnableCombo3;
    
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
            var stamina = currentWeapon.GetComponent<Weapon>().ConsumingStamina;
            float attackSpeed = currentWeapon.GetComponent<Weapon>().AttackSpeed;
            animator.SetFloat("AttackSpeed",attackSpeed);
            player.ConsumeStamina(stamina);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            animator.SetTrigger("Combo1");
            //else if(EnableCombo2) animator.SetTrigger("Combo2");
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

    public void EnableWeapon()
    {
        currentWeaponCollider.enabled = true;
    }

    public void DisableWeapon()
    {
        currentWeaponCollider.enabled = false;
    }
    
    //设置Combo
    public void SetCombo1()
    {
        EnableCombo1 = true;
    }
    public void SetCombo2()
    {
        EnableCombo2 = true;
    }

    public void SetCombo3()
    {
        EnableCombo3 = true;
    }
    public void ResetCombo1()
    {
        EnableCombo1 = false;
    }
    public void ResetCombo2()
    {
        EnableCombo2 = false;
    }
    public void ResetCombo3()
    {
        EnableCombo3 = false;
    }
}
