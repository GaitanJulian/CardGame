using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemAttkAnim : MonoBehaviour
{
    private float maxAngle = 45f;
    private float minAngle = 0f;
    private bool isFighting = false;
    float power = 8f;
    float oscillationSpeed = 4f;
    private Rigidbody2D rb2d; // Contador de animaciones ejecutadas.
    private int countForKills = 0;

    [SerializeField] private EnemyCounterScriptableObject enemyCounter;

    private void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();

        
    }

    private void OnEnable()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnCardMatched += HandleCardMatched;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCardMatched -= HandleCardMatched;
        }
    }

    private void HandleCardMatched()
    {
        isFighting = false;
        countForKills++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFighting && collision.gameObject.tag == "Player")
        {
            isFighting = true;
            StartCoroutine(OscillateAnimation());
            
        }
    }

    private IEnumerator OscillateAnimation()
    {
        float initialRotation = transform.rotation.eulerAngles.z;
        float elapsedTime = 0f;

        if (countForKills == 0)
        {
            do
            {
                float oscillationValue = Mathf.Sin(elapsedTime * oscillationSpeed);

                float t = -(oscillationValue + 1f) / 2f;

                float newRotation = Mathf.Lerp(minAngle, maxAngle, Mathf.Pow(t, power));

                rb2d.MoveRotation(newRotation);

                elapsedTime += Time.deltaTime;

                yield return null;
            } while (isFighting);
        }
        else
        {
            do
            {
                float oscillationValue = Mathf.Sin(elapsedTime * oscillationSpeed);

                float t = -(oscillationValue + 1f) / 2f;

                float newRotation = Mathf.Lerp(minAngle, maxAngle, Mathf.Pow(t, power));

                rb2d.MoveRotation(newRotation);

                elapsedTime += Time.deltaTime;

                yield return null;
            } while (elapsedTime < 4);
        }



        isFighting = false;
        gameObject.SetActive(false);
        countForKills--;
        enemyCounter.EnemyKilled();

    }
}
