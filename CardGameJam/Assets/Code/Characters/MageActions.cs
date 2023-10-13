using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MageActions : MonoBehaviour
{

    private bool isRage = false;

    private float initialPosY;

    //private float currentPosX;

    private float elapsedTime = 0;

    private Rigidbody2D rbMage;

    private void Start()
    {
        initialPosY = transform.localPosition.y;
        rbMage = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        

        if (isRage)
        {
            
            rbMage.velocity = new Vector2(0f, 3f);
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 0.15f)
            {
                //currentPosX = transform.localPosition.x;
                rbMage.velocity = Vector2.zero;
                transform.localPosition = new Vector3(0, 0, 0);
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