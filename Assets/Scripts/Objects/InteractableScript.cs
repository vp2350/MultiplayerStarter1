using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    [SerializeField] protected string indicatorString = "Collect Objective";
    [SerializeField] ObjectiveIndicatorUI objectiveIndicator;

    public void Start()
    {
        if(objectiveIndicator == null)
        {
            objectiveIndicator = GameObject.Find("Objective Indicator").GetComponent<ObjectiveIndicatorUI>();
        }
    }
    public virtual void Trigger()
    {
        Debug.Log("Triggered");
        this.transform.parent.gameObject.SetActive(false);
    }

    public virtual void SetIndicatorText()
    {
        string text = "Press " + Controls.Instance.Interact + " to " + indicatorString;
        objectiveIndicator.SetText(text);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerInteractionController>().SetInteractable(this);

            if(objectiveIndicator)
            {
                SetIndicatorText();
                objectiveIndicator.SetVisible(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerInteractionController>().RemoveInteractable();

            if (objectiveIndicator)
            {
                objectiveIndicator.SetVisible(false);
            }
        }
    }
}
