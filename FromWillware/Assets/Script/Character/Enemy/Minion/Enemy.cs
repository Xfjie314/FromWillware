using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private Animator anim;
    private NavMeshAgent agent;

    [Header("目标设置")]
    public Transform playerTarget;

    [Header("战斗参数")]
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    private float lastAttackTime = 0f;

    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //  初始化血量（使用 Character 基类的变量）
        CurrentHP = MaxHP;

        if (playerTarget == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) playerTarget = p.transform;
        }
    }

    void Update()
    {
        if (isDead || playerTarget == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTarget.position);
            anim.SetBool("isWalking", true);
        }
        else
        {
            agent.isStopped = true;
            anim.SetBool("isWalking", false);

            Vector3 direction = (playerTarget.position - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                anim.SetTrigger("DoAttack");
                lastAttackTime = Time.time;
            }
        }
    }

    // 重写基类死亡方法
    public override void Die()
    {
        base.Die(); 

        isDead = true;
        anim.SetTrigger("DoDeath");
        agent.isStopped = true;
        GetComponent<Collider>().enabled = false;

        //销毁尸体
        Destroy(gameObject, 5f);
    }
}