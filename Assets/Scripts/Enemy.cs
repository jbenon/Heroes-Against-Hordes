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

    private void Start(){
        timerDamageWithHero = enemySO.maxTimerDamageWithHero;
        lifePoints = enemySO.lifePoints;
    }

    private void Update()
    {
        // Move closer to the hero
        Move();

        // Handle damages
        if (HasCollisionWithHero())
        {
            if (timerDamageWithHero >= enemySO.maxTimerDamageWithHero){
                lifePoints -= Hero.Instance.GetAttackStat() / enemySO.defenseStat;
                Hero.Instance.ChangeLifePoints(- enemySO.attackStat / Hero.Instance.GetDefenseStat());
                timerDamageWithHero = 0f;
            }
            timerDamageWithHero += Time.deltaTime;
        }

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

    private bool HasCollisionWithHero()
    {
        float distanceToHero = Vector3.Distance(Hero.Instance.transform.position, transform.position);
        return distanceToHero - enemySO.collisionDiameter - Hero.Instance.GetCollisionDiameter() < 0;
    }

}
