using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{

    public float speed;
    public float minSpeed;
    public float maxSpeed;

    public float xRotSpeed;
    public float yRotSpeed;

    public float decentSpeed;
    public float minDecentSpeed;
    public float maxDecentSpeed;

    public bool isSub;

    public Rigidbody subRigi;
    public GameObject Player;
    public GameObject playerContainer;

    void FixedUpdate()//this will use simple keycodes for now, but we can use this for the unity input system if we want. This is just to see the best way to control the sub
    {
        if(isSub)//checks to see if the player has pressed e on the control pannel
        {
            SubControl();
        }
        else
        {
            subRigi.isKinematic = false;
        }
    }

    public void SubControl()
    {
        subRigi.isKinematic = false;
        //this will ensure that the sub will allways move forward. 0 speed will stop the throttle of the speed.
        subRigi.AddForce(transform.forward * speed, ForceMode.Impulse);

        //float rotationX = Input.GetAxis("Vertical") * yRotSpeed;
        float rotationY = Input.GetAxis("Horizontal") * xRotSpeed;
        Quaternion rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
        subRigi.MoveRotation(subRigi.rotation * rotation);


        //This will increase the speed of sub
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += 100f;
        }

        //This will decrease the speed of sub
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed -= 100f;
        }

        //Moves sub up
        if (Input.GetKey(KeyCode.W))
        {
            subRigi.AddForce(transform.up * decentSpeed);
        }

        //moves sub down
        if (Input.GetKey(KeyCode.S))
        {
            subRigi.AddForce(-transform.up * decentSpeed);
        }

        //this will cap the speed to the set max speed
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

        //This will set the sub speed to zero when the thrust is tunred off
        if (speed <= minSpeed)
        {

            //right now the sub will stop immediatly, but we can probably find a way to make it gradually stop.
            speed = minSpeed;
            subRigi.velocity = Vector3.zero;
            subRigi.angularVelocity = Vector3.zero;
        }

        //this will cap the decent speed to the set max decent speed
        if (decentSpeed >= maxDecentSpeed)
        {
            decentSpeed = maxDecentSpeed;
        }

        //This will set the sub decent speed to zero when the thrust is tunred off
        if (decentSpeed <= minDecentSpeed)
        {

            //right now the sub will stop immediatly, but we can probably find a way to make it gradually stop.
            decentSpeed = minDecentSpeed;
            subRigi.velocity = Vector3.zero;
            subRigi.angularVelocity = Vector3.zero;
        }

    }
}
