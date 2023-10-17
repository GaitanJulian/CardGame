using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EnemyCounterScriptableObject", menuName = "ScriptableObjects/ Enemy Counter Manager")]
public class EnemyCounterScriptableObject : ScriptableObject
{
    public int maxEnemies = 45;
    public int enemyCounter = 0;


    public UnityEvent<int> enemyKilled;

    private void OnEnable()
    {
        ResetCounter();
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

    public void ResetCounter()
    {
        enemyCounter = 0;
        enemyKilled.Invoke(enemyCounter);
    }
}
