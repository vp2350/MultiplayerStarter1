using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenSpawner : MonoBehaviour
{
    Transform[] spawns;
    [SerializeField] GameObject Warden;
    GameObject _warden;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GetComponentsInChildren<Transform>();

    }

    public void SpawnWarden()
    {
        if (_warden == null)
        {
            _warden = GameObject.Instantiate(Warden);
            Vector3 pos = PlayerInfo.Instance.transform.position;
            foreach (Transform spawn in spawns)
            {
                if ((spawn.position - PlayerInfo.Instance.playerPos.position).magnitude > (pos - PlayerInfo.Instance.playerPos.position).magnitude)
                {
                    pos = spawn.position;
                }
            }

            _warden.transform.position = pos;
        }
}

}
