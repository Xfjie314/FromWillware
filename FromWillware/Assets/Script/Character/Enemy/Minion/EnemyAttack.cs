using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Damage
{

    private Collider weaponCollider;

    // 核心状态锁
    private bool hasDealtDamage = false;

    void Start()
    {
        weaponCollider = GetComponent<Collider>();

        // 武器默认关闭碰撞
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    // 开启武器，并重置状态锁
    public void EnableWeapon()
    {
        hasDealtDamage = false;
        if (weaponCollider != null) weaponCollider.enabled = true;
        Debug.Log("enenmyweapon on" );
    }

    // 关闭武器
    public void DisableWeapon()
    {
        if (weaponCollider != null) weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 碰到玩家，且这一刀还没有造成过伤害
        if (other.CompareTag("Player") && !hasDealtDamage)
        {
            
            Debug.Log("enenmy attack：" + damage);
            // 砍中一次后锁死
            hasDealtDamage = true;
            if (weaponCollider != null)
            {
                weaponCollider.enabled = false;
            }
        }
    }
}