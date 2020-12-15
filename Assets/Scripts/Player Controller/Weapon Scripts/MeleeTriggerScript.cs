using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTriggerScript : MonoBehaviour
{
    [SerializeField] float damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Damageable":
                // Do damage to other object 
                break;
            case "Enemy":
            case "Breakable":
                // Break the other object
                other.gameObject.GetComponent<BreakableObjectScript>().GetHit(damage);
                break;
            case "Player":
                //Don't scratch yourself - duh
                break;
            case "Bullet":
                // Do nothing because claws can't destroy bullets
                break;
            default:
                // Do nothing so the claws just disappear
                break;

        }
    }
    
    private void OnTriggerEnter2D(CircleCollider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Damageable":
                // Do damage to other object 
                break;
            case "Breakable":
                // Break the other object
                Debug.Log("hit object");
                other.gameObject.GetComponent<BreakableObjectScript>().GetHit(damage);
                break;
            case "Player":
                //Don't scratch yourself - duh
                break;
            case "Bullet":
                // Do nothing because claws can't destroy bullets
                break;
            default:
                // Do nothing so the claws just disappear
                break;

        }
    }
}
