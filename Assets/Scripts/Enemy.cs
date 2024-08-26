using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;
    private float timerDamageWithHero;
    private float lifePoints;
    public static event EventHandler<OnAttackHeroEventArgs> OnAttackHero;
    public class OnAttackHeroEventArgs: EventArgs{
        public float attackStat;
    }
    // Timers
    private float timerAutomaticHeroAttack;
    private float maxTimerAutomaticHeroAttack = 0.5f;


    private void Awake(){
        timerAutomaticHeroAttack = maxTimerAutomaticHeroAttack;
    }
    private void Start(){
        timerDamageWithHero = enemySO.maxTimerDamageWithHero;
        lifePoints = enemySO.lifePoints;
        Hero.Instance.OnAttackEnemy += Enemy_OnAttackEnemy;
    }

    private void Update()
    {
        timerAutomaticHeroAttack += Time.deltaTime;
        // Move closer to the hero
        Move();
        // Handle death
        if (lifePoints <= 0){
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        // Move
        Vector3 moveDir = Hero.Instance.transform.position - transform.position;
        moveDir = moveDir.normalized;
        transform.position += moveDir * enemySO.moveSpeed * Time.deltaTime;
        // Rotate
        transform.up = Vector3.Slerp(transform.up, moveDir, enemySO.rotateSpeed * Time.deltaTime);
    }

    private void UpdateLifePoints(float lifePointsUpdate){
        lifePoints = lifePoints + lifePointsUpdate;
    }

    private void Enemy_OnAttackEnemy(object sender, Hero.OnAttackEnemyEventArgs e){
        if (this == e.enemy){
            UpdateLifePoints(- e.attackStat / enemySO.defenseStat);
        }
    }
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (timerAutomaticHeroAttack >= maxTimerAutomaticHeroAttack){
            timerAutomaticHeroAttack = 0f;
            // Enemy damages hero
            OnAttackHero?.Invoke(this, new OnAttackHeroEventArgs{
                attackStat = enemySO.attackStat});

        }
    }


}
