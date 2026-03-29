using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string Name;
    public float AttackSpeed;
    public float ConsumingStamina;
    public int WeaponDamage;
    public string Introduction;
    
    private Damage damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponentInChildren<Damage>();
        WeaponDamage = damage.damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
