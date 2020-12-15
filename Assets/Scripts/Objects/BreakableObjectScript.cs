using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BreakableObjectScript : MonoBehaviour
{
    [SerializeField] Transform DroppedObject;
    [SerializeField] double Health;
    [SerializeField] Transform DestroyedObject;
    [SerializeField] bool rescanGraphWhenDestroyed = false;
    /// <summary>
    /// Handles when the object gets hit by something
    /// </summary>
    /// <param name="Damage">The amount of damage the attack did to the object</param>
    public void GetHit(double Damage)
    {
        Health -= Damage;
        if(Health <= 0.0f)
        {
            // change state to combat end if this is an enemy
            if (gameObject.tag == "Enemy" && 
                (AudioManager.Instance.currentState == AudioManager.AudioState.Combat || AudioManager.Instance.currentState == AudioManager.AudioState.StealthToCombat))
            {
                AudioManager.Instance.currentState = AudioManager.AudioState.CombatToCombatEnd;
            }

            // Drop an item
            if(DroppedObject != null)
            {
                Instantiate(DroppedObject, this.transform.position, Quaternion.identity);
            }

            // Play animation
            if (DestroyedObject != null)
            {
                Instantiate(DestroyedObject, this.transform.position, Quaternion.identity);
            }
            if (rescanGraphWhenDestroyed) GameObject.Find("A*").GetComponent<AstarPath>().Scan();
            // Destroy this game object
            Destroy(gameObject);
        }
    }
}
