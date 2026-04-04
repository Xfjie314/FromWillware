using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotateSpeed = 10f;

    private LockOnSystem lockOnSystem;

    void Start()
    {
        lockOnSystem = GetComponent<LockOnSystem>();
    }

    void Update()
    {
        if (lockOnSystem != null && lockOnSystem.IsLocking)
        {
            RotateToTarget(lockOnSystem.currentTarget);
        }
    }

    void RotateToTarget(Transform target)
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0; // 🔴 防止角色抬头低头

        if (dir.sqrMagnitude < 0.01f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime
        );
    }

   
}
