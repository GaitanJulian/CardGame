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
    

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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

        do
        {
            float oscillationValue = Mathf.Sin(elapsedTime * oscillationSpeed);
            float t = (oscillationValue + 1f) / 2f;
            float newRotation = Mathf.Lerp(minAngle, maxAngle, Mathf.Pow(t, power));
            rb2d.MoveRotation(newRotation);
            elapsedTime += Time.deltaTime;

            yield return null;
        } while (isFighting && elapsedTime < 5.5f);

        isFighting = false;
        gameObject.SetActive(false);

    }
}
