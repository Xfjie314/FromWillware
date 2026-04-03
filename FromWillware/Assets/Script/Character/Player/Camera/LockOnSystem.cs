using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockOnSystem : MonoBehaviour
{
    public float lockRange = 15f;
    public LayerMask enemyLayer;
    public CinemachineTargetGroup targetGroup;
    public bool IsLocking;
    public Transform currentTarget;
    
    private List<Transform> enemies = new List<Transform>();
    private CameraFollow cameraFollow;

    void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (currentTarget == null)
                LockOn();
            else
                Unlock();
        }
    }

    void LockOn()
    {
        enemies.Clear();

        Collider[] hits = Physics.OverlapSphere(transform.position, lockRange, enemyLayer);

        float minDist = Mathf.Infinity;
        Transform bestTarget = null;

        foreach (var hit in hits)
        {
            Transform enemy = hit.transform;

            float dist = Vector3.Distance(transform.position, enemy.position);

            if (dist < minDist)
            {
                minDist = dist;
                bestTarget = enemy;
            }
        }

        if (bestTarget != null)
        {
            currentTarget = bestTarget;

            // 加入相机组
            targetGroup.AddMember(currentTarget, 1f, 2f);
        }
        cameraFollow.enabled = true;
        IsLocking = true;
    }

    void Unlock()
    {
        if (currentTarget != null)
        {
            targetGroup.RemoveMember(currentTarget);
            currentTarget = null;
        }
        cameraFollow.enabled = false;
        IsLocking = false;
    }
}
