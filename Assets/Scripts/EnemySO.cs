using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    // Physics
    public float rotateSpeed;
    public float collisionDiameter;
    // Timers
    public float maxTimerDamageWithHero;
    // Stats
    public float lifePoints;
    public float moveSpeed;
    public float attackStat;
    public float defenseStat;
}
