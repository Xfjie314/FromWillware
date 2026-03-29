using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        // 只要武器碰到了任何东西，立刻打印出来！
        Debug.Log("<color=red>weapon take ：</color>" + other.gameObject.name);
    }
}