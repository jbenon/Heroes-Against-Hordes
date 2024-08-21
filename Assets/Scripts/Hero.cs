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
    private float collisionDiameter = 1f;
    // Stats
    private float lifePoints = 100f;
    private float attackStat = 200f;
    private float defenseStat = 100f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Move();
        
        // Handle death
        if (lifePoints <= 0){
            Debug.Log("Game over");
        }
    }

    private void Move()
    {
        // Move
        Vector2 inputVector = gameInputs.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0f);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        // Rotate
        transform.up = Vector3.Slerp(transform.up, moveDir, rotateSpeed * Time.deltaTime);
    }

    // Functions to set hero parameters
    public void ChangeLifePoints(float lifePointsChange){
        lifePoints += lifePointsChange;
    }
    // Functions to access hero parameters
    public float GetCollisionDiameter(){
        return collisionDiameter;
    }
    public float GetAttackStat(){
        return attackStat;
    }
    public float GetDefenseStat(){
        return defenseStat;
    }
}
