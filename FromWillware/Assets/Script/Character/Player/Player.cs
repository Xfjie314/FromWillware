using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float MaxStamina;
    public float CurrentStamina;
    public float StaminaRecoverRate;
    public bool StaminaEmpty;

   
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        CurrentStamina = MaxStamina;
     
    }

    // Update is called once per frame
    void Update()
    {
        RecoverStamina();
        if (CurrentStamina <= 0)
        {
            StaminaEmpty = true;
        }
        else
        {
            StaminaEmpty = false;
        }
    }

    public void ConsumeStamina(float amount)
    {
        CurrentStamina -= amount;
    }

    void RecoverStamina()
    {
        if (CurrentStamina <= MaxStamina)
        {
            CurrentStamina += StaminaRecoverRate * Time.deltaTime;
            CurrentStamina = Mathf.Clamp(CurrentStamina, -20, MaxStamina);//限制精力的范围0~MaxStamina
        }
    }
}
