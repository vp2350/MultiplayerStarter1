using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplotch : MonoBehaviour
{
    [SerializeField] float fadeTime = 1.0f;
    float currentFadeTime;
    [SerializeField] float fadeDelay = 2.0f;
    SpriteRenderer sprite;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        currentFadeTime = fadeTime;
    }
    void Update()
    {
        if(fadeDelay >= 0.0f)
        {
            fadeDelay -= Time.deltaTime;
        }
        else if(currentFadeTime >= 0.0f)
        {
            currentFadeTime -= Time.deltaTime;
            Color newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, currentFadeTime / fadeTime);
            sprite.color = newColor;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
