using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshPro textMesh;

    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 1;
    private float disappearTimer;
    
    private Color textColor;
   
    private Vector3 moveVector;


    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        moveVector = new Vector3(0.1f, 0.5f) * 60f;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            //do things during the first half of the popup
            float increaseScaleAmount = 0.5f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;

        } else
        {
            //do things during the second half of the popup
            float decreaseScaleAmount = 0.5f;
            transform.localScale += Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer <= 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
