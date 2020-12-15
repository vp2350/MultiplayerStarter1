using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]GameObject character;
    [SerializeField][Range(0f,1f)] float minMouseDisplacementRadius = .25f;
    [SerializeField] [Range(1f, 10f)] float displacementScale = 2f;
    void Start()
    {
        GetComponents();
    }

    void Update()
    {
        Vector3 displacement = Utils.Instance.ConvertMousePosToScreenQuarters(Input.mousePosition);

        Vector3 target = new Vector3(character.transform.position.x,
                                     character.transform.position.y,
                                     transform.position.z);

        if (displacement.magnitude > minMouseDisplacementRadius)
        {
            target += displacement.normalized * Mathf.Lerp(0, displacement.magnitude * displacementScale,
                                                              displacement.magnitude - minMouseDisplacementRadius);
            
        }

        transform.position = target;
    }

    void GetComponents()
    {
        if (character == null)
        {
            character = GameObject.Find("PlayerChar");
        }
    }
}
