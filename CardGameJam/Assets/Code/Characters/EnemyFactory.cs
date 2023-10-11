using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private ObjectPooler objectPooler;

    private float yMaxRange = 2.97f;
    private float yMinRange = 2.61f;

    private void Start()
    {
        objectPooler = FindObjectOfType<ObjectPooler>();
        InvokeRepeating("Spawner", 2, Random.Range(2f, 4f));
    }

    private void Spawner()
    {
        GameObject enemy = objectPooler.GetPooledObject("Enemy");
        enemy.transform.position = new Vector2(10, Random.Range(yMinRange, yMaxRange));
        enemy.SetActive(true);
    }
}
