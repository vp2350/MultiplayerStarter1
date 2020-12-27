using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]Rigidbody2D rb;
    [Space]

    [Header("Movement")]
    [SerializeField][Range(5,50)] float maxWalkSpeed = 1.5f;
    [SerializeField] bool accelerationEnabled = true;
    [SerializeField] [Range(1, 200)] float accelerationpercent = 110f;
    GameObject camera1Obj;
    Camera camera1;
    [SerializeField] GameObject lightMask;
    [SerializeField] GameObject lightMask1;
    [SerializeField] GameObject lightMask2;


    float acceleration, speed;
    Vector2 move;

    private PhotonView PV;
    void Start()
    {
        PV = this.transform.parent.GetComponent<PhotonView>();
        camera1Obj = (this.transform.parent).gameObject;
        camera1 = camera1Obj.transform.Find("Camera").GetComponent<Camera>();
        if(!PV.IsMine)
        {
            camera1Obj.transform.Find("Camera").gameObject.SetActive(false);
            lightMask.SetActive(false);
            lightMask1.SetActive(false);
            lightMask2.SetActive(false);
        }
        acceleration = (accelerationpercent / 100f) * maxWalkSpeed;
        GetComponents();
    }

    void Update()
    {
        if (this.transform.parent.GetComponent<PhotonView>().IsMine)
        {
            RotateSprite();
            UpdateMoveControls();
        }
    }

    private void FixedUpdate()
    {
        UpdateMove();
    }

    #region updates
    void RotateSprite()
    {

        // Player can control direction
        Vector3 mousePos = camera1.ScreenToWorldPoint(Input.mousePosition); //get mouse position
        Vector3 direction = transform.position - mousePos; //get vector from position to mouse pos
        float angleOfRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90; //get angle
        transform.localRotation = Quaternion.Euler(0, 0, angleOfRotation); //set rotation
    }

    void UpdateMove()
    {


        if (accelerationEnabled) { //accelearte if enabled
            if (move.magnitude > 0) speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxWalkSpeed);
            else speed = Mathf.Max(speed - acceleration * Time.deltaTime, 0f) ;
        } else { //otherwize go to max speed
            speed = maxWalkSpeed;
        }

        move = move * speed * Time.deltaTime; //multiply by speed and time
        Vector2 target = new Vector2(transform.position.x + move.x, transform.position.y + move.y); //add to move
        rb.MovePosition(target); //move rigidbody
    }

    void UpdateMoveControls()
    {
        float x = 0, y = 0;
        if (Input.GetKey(Controls.Instance.Left))
        {
            x = -1;
        }
        if (Input.GetKey(Controls.Instance.Right))
        {
            x = 1;
        }

        if (Input.GetKey(Controls.Instance.Up))
        {
            y = 1;
        }
        if (Input.GetKey(Controls.Instance.Down))
        {
            y = -1;
        }

        move = new Vector2(x, y); //turn into vector
        move = move.normalized; //normalize
    }
    #endregion

    void GetComponents()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

}
