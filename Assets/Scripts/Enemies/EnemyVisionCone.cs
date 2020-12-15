using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionCone : MonoBehaviour
{
	public bool CanSeePlayer(LayerMask playerLayer)
	{
		return GetComponent<Collider2D>().IsTouchingLayers(playerLayer);
	}
}
