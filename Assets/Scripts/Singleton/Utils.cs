using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : Singleton<Utils>
{
	/// <summary>
	/// returns a vector with x and y componets between 1 and -1, with positive being the top right corner and the origin at the center of the screen
	/// </summary>
	/// <param name="pos">the mouse position</param>
	/// <returns></returns>
	public Vector3 ConvertMousePosToScreenQuarters(Vector3 pos)
	{
		Vector3 output = new Vector3();

		output.x = (pos.x *2 / Screen.width) -1;
		output.y = (pos.y * 2 / Screen.height) - 1;
		output.z = 0;

		return output;
	}
}
