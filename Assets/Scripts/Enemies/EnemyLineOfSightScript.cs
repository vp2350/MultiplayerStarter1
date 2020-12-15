using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyLineOfSightScript : MonoBehaviour
{
    // Mask for everything that isn't walls or the player
    private LayerMask mask;
    [SerializeField] Transform pos;

    private void Start()
    {
        mask = LayerMask.GetMask("Walls");
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        Vector3 direction = PlayerInfo.Instance.playerPos.position - pos.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude, mask.value);

        if(hit)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
