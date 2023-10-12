using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageActions : MonoBehaviour
{

    private bool isRage = false;

    private Vector2 initialPos;

    private float elapsedTime = 0;

    private Rigidbody2D rbMage;

    private void Start()
    {
        initialPos = transform.position;
        rbMage = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {


        if (isRage)
        {
            
            rbMage.velocity = new Vector2(0f, 3f);

            if (elapsedTime < 1.5f)
            {
                elapsedTime += Time.deltaTime;
                rbMage.velocity = Vector2.zero;
                transform.Translate(initialPos);
                elapsedTime = 0;
                isRage = false;
            }
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
        isRage = true;
    }
}