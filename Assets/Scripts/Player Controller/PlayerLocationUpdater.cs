using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocationUpdater : MonoBehaviour
{
    void Update()
    {
        PlayerInfo.Instance.playerPos = gameObject.transform;   
    }
}
