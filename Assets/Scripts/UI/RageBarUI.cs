using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBarUI : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider slider;
    [SerializeField] Color baseColor;
    [SerializeField] Color transformingColor;
    [SerializeField] Image fill;

    // Start is called before the first frame update
    void Start()
    {
        if(playerStats == null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }
        if(slider == null)
        {
            slider = gameObject.GetComponent<Slider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.HasTransformAbility)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1); // Make it visible
            slider.value = playerStats.TransformPercentage;
           if (fill != null)
            {
                if (!playerStats.isTransformed)
                {
                    fill.color = baseColor;
                }
                else
                {
                    fill.color = transformingColor;
                }
            }
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0); // Basically "Disable" it by changing it's scale to 0
        }
        
    }
}
