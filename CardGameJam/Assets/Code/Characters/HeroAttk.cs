using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttk : MonoBehaviour
{
    private float maxAngle = -30f;
    private float minAngle = 0f;
    public bool isFighting = false;
    public float power = 8f;
    public float oscillationSpeed = 4f;
    private Rigidbody2D rb2d; // Contador de animaciones ejecutadas.

    private int countForKills;

    [SerializeField] private HealthManagerScriptableObject healthManager;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFighting && !CompareTag("Enemy"))
        {
            isFighting = true;
            StartCoroutine(OscillateAnimation());
        }
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
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

                if(elapsedTime > 2 && elapsedTime % 1 == 0)

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
        countForKills--;

        isFighting = false;

    }
}
