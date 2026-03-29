using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if(player.IsDead) return;
        player.CurrentHP -= damage;
        Debug.Log("The player has been hit " + damage);
        if (player.CurrentHP <= 0)
        {
            player.Die();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            Damage ennemyDamage = other.GetComponent<Damage>();
            if (ennemyDamage != null)
                TakeDamage(ennemyDamage.damage);
        }
        else
        {
            Debug.Log("No Tag EnemyAttack");
            return;
        }
    }
}
