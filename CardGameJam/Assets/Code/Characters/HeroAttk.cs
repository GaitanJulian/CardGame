using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class HeroAttk : MonoBehaviour
{
    private float maxAngle = -30f;
    private float minAngle = 0f;
    public bool isFighting;
    public float power = 8f;
    public float oscillationSpeed = 4f;
    private Rigidbody2D rb2d; // Contador de animaciones ejecutadas.

    public int countForKills;

    public GameObject visualFX;

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
        if (countForKills == 0)
        {
            visualFX.SetActive(false);
        }
        else
        {
            visualFX.SetActive(false);
        }
    }



    private IEnumerator OscillateAnimation()
    {
        float initialRotation = transform.rotation.eulerAngles.z;
        float elapsedTime = 0f;

        float t = 0;

        if (countForKills == 0)
        {
            do
            {
                float oscillationValue = Mathf.Sin(elapsedTime * oscillationSpeed);

                t = -(oscillationValue + 1f) / 2f;

                float newRotation = Mathf.Lerp(minAngle, maxAngle, Mathf.Pow(t, power));

                rb2d.MoveRotation(newRotation);

                elapsedTime += Time.deltaTime;

                if( elapsedTime%2 < 0.1f )
                {
                    healthManager.DecreaseHealth(1);
                }

                yield return null;
            } while (isFighting);
        }
        else
        {
            do
            {
                float oscillationValue = Mathf.Sin(elapsedTime * oscillationSpeed);

                t = -(oscillationValue + 1f) / 2f;

                float newRotation = Mathf.Lerp(minAngle, maxAngle, Mathf.Pow(t, power));

                rb2d.MoveRotation(newRotation);

                elapsedTime += Time.deltaTime;
                
                yield return null;
            } while (elapsedTime < 4);
        }

        countForKills -= 1;

        elapsedTime = 0f;

        isFighting = false;

        transform.eulerAngles = new Vector3(0, 0, 0);

    }
}
