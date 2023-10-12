using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HealthManagerScriptableObject", menuName = "ScriptableObjects/ Enemy Counter Manager")]
public class EnemyCounterScriptableObject : ScriptableObject
{
    public int maxEnemies = 45;
    public int enemyCounter = 0;


    public UnityEvent<int> enemyKilled;

    private void OnEnable()
    {
        enemyCounter = 0;

        if (enemyKilled != null)
        {
            enemyKilled = new UnityEvent<int>();
        }
    }

    public void EnemyKilled()
    {
        enemyCounter++;
        enemyKilled.Invoke(enemyCounter);
    }
}
