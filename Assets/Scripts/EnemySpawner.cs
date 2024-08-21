using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemyList;

    private float timerEnemySpawn;
    private float maxTimerEnemySpawn = 1f;

    private void Start()
    {
        timerEnemySpawn = maxTimerEnemySpawn;
    }

    private void Update()
    {
        if (timerEnemySpawn >= maxTimerEnemySpawn)
        {
            System.Random randomEnemyListIndex = new System.Random();
            Vector3 enemyPosition = GenerateRandomEnemyPosition();
            Instantiate(enemyList[randomEnemyListIndex.Next(enemyList.Count)], 
                enemyPosition, Quaternion.identity);
            timerEnemySpawn = 0f;
        }
        timerEnemySpawn += Time.deltaTime;
    }

    private Vector3 GenerateRandomEnemyPosition()
    {
        // Get screen size in real world coordinates
        Vector2 cameraCoordsTopRightCorner = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float halfHeight = cameraCoordsTopRightCorner.y;
        float halfWidth = cameraCoordsTopRightCorner.x;
        // Generate random position on one of the edges
        Vector3 enemyPosition = new Vector3(
            Random.Range(-halfWidth, halfWidth),
            Random.Range(-halfHeight, halfHeight),
            0f
        );
        int enemySide = Random.Range(1, 4);
        switch (enemySide)
        {
            default:
            case 1:
                enemyPosition[0] = -halfWidth - 1;
                break;
            case 2:
                enemyPosition[0] = halfWidth + 1;
                break;
            case 3:
                enemyPosition[1] = -halfHeight - 1;
                break;
            case 4:
                enemyPosition[1] = halfHeight + 1;
                break;
        }
        return enemyPosition;
    }
}
