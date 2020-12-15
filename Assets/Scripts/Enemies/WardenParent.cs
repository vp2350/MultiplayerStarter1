using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenParent : MonoBehaviour
{

	[SerializeField] GameObject Child;

	private void Update()
	{
		if (Child == null)
		{
			GameObject.Destroy(gameObject);
		}
	}
}
