using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float fadeSpeed = 2f;
    public bool isDispelled;
    public float returnTimer;
    private Color originalColor;
    private SpriteRenderer rend;
    private bool returnTimerActive = false;
    public bool isDispelling;

    public void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        originalColor = rend.color;
        Dispel(true, 5f);
    }

    public void Dispel(bool shouldDispel, float timeToReturn)
    {
        isDispelled = shouldDispel;
        if (timeToReturn > 0)
        {
            returnTimer = timeToReturn;
            returnTimerActive = true;
        }
        else 
        {
            returnTimerActive = false;
        }
    }

    public void Update()
    {
        if (returnTimerActive)
        {
            returnTimer -= Time.deltaTime;
            if (returnTimer <= 0)
            {
                Dispel(false, 0);
            }
        }
        if (isDispelled)
        {
            if (rend.color.a > 0f)
            {
                Color currentColor = rend.color;
                currentColor.a = Mathf.Max(0f, currentColor.a - fadeSpeed * Time.deltaTime);
                rend.color = currentColor;
            }
            else if (rend.color.a <= 0f)
            {
            }
        }
        else
        {
            if (rend.color.a < 1f)
            {
                Color currentColor = rend.color;
                currentColor.a = Mathf.Min(1f, currentColor.a + fadeSpeed * Time.deltaTime);
                rend.color = currentColor;
            }
            else if (rend.color.a >= 1f)
            {
            }
        }
    }
}
