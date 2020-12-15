using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : Singleton<PlayerInfo>
{
	public Transform playerPos;
	public bool hasShot;
	public bool isDead = false;
}
