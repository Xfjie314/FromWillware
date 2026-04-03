using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponSystem : MonoBehaviour
{
    public Transform WeaponPoint;
    public List<Transform> Weapons;
    public int CurrentWeaponIndex = 0;
    public Transform CurrentWeapon;
    
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in WeaponPoint)
        {
            Weapons.Add(child);
            child.gameObject.SetActive(false);
            child.GetComponentInChildren<Collider>().enabled = false;
        }
        if(Weapons.Count > 0)
            EquipWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeWeapon();
        }
    }

    public void EquipWeapon(int index)
    {
        CurrentWeaponIndex = index;
        Weapons[index].gameObject.SetActive(true);
        CurrentWeapon = Weapons[index];
        
    }

    public void ChangeWeapon()
    {
        Weapons[CurrentWeaponIndex].gameObject.SetActive(false);
        CurrentWeaponIndex = (CurrentWeaponIndex+1)%Weapons.Count;
        EquipWeapon(CurrentWeaponIndex);
    }
}
