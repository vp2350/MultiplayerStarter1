using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
	[SerializeField] List<Vector2> PatrolPoints = new List<Vector2>();
	int currentIndex = -1;


	public Vector2 nextPoint{ 
		get{
			currentIndex++;
			if (currentIndex == PatrolPoints.Count) currentIndex = 0;
			return PatrolPoints[currentIndex];
		}
	}

	private void OnDrawGizmos()
	{
		for (int i = 0; i < PatrolPoints.Count - 1; i++)
		{
			Gizmos.DrawLine(PatrolPoints[i], PatrolPoints[i + 1]);
		}
		Gizmos.DrawLine(PatrolPoints[0], PatrolPoints[PatrolPoints.Count - 1]);
	}

}
