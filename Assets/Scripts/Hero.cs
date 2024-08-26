using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero Instance { get; private set; }

    [SerializeField]
    private GameInputs gameInputs;
    private float moveSpeed = 4f;
    private float rotateSpeed = 8f;
    private Rigidbody2D playerRigidbody2d;
    public event EventHandler<OnAttackEnemyEventArgs> OnAttackEnemy;
    public class OnAttackEnemyEventArgs: EventArgs{
        public Enemy enemy;
        public float attackStat;
    }
    // Stats
    private float lifePoints = 100f;
    private float attackStat = 200f;
    private float defenseStat = 50f;
    // Timers
    private float timerAutomaticEnemyAttack;
    private float maxTimerAutomaticEnemyAttack = 0.5f;

    private void Awake()
    {
        Instance = this;
        playerRigidbody2d = GetComponent<Rigidbody2D>();
        timerAutomaticEnemyAttack = maxTimerAutomaticEnemyAttack;
    }

    private void Start(){
        Enemy.OnAttackHero+= Hero_OnAttackHero;
    }
    private void Update()
    {
        timerAutomaticEnemyAttack += Time.deltaTime;
        // Handle death
        if (lifePoints <= 0){
            Debug.Log("Game over");
        }
        Debug.Log(lifePoints);
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = gameInputs.GetMovementVectorNormalized();
        
        if (inputVector.x !=0 | inputVector.y != 0){
            // Move
            Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0f);
            playerRigidbody2d.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
            // Rotate
            float moveAngle = -90 + Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
            Quaternion moveQuaternion = Quaternion.AngleAxis(moveAngle, Vector3.forward);
            playerRigidbody2d.MoveRotation(Quaternion.Slerp(transform.rotation, moveQuaternion, Time.deltaTime * rotateSpeed));
        }
    }

    private void UpdateLifePoints(float lifePointsUpdate){
        lifePoints = lifePoints + lifePointsUpdate;
    }

    private void Hero_OnAttackHero(object sender, Enemy.OnAttackHeroEventArgs e){
        UpdateLifePoints(- e.attackStat / defenseStat);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (timerAutomaticEnemyAttack >= maxTimerAutomaticEnemyAttack){
            timerAutomaticEnemyAttack = 0f;
            // Hero damages enemy
            OnAttackEnemy?.Invoke(this, new OnAttackEnemyEventArgs{
                enemy = collider.gameObject.GetComponent<Enemy>(),
                attackStat = attackStat});

        }
    }

}
