using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    Rigidbody ballRb;
    float throwPower = 10f;
    GameObject childFireObject;

    // Start is called before the first frame update
    void Start()
    {
        childFireObject = transform.GetChild(0).gameObject;
        ballRb = GetComponent<Rigidbody>();
        //throw at an angle of 30 degrees
        Vector3 direction = new Vector3(0, 0.5f, 1);
        ballRb.AddForce(throwPower * direction, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Bogeyman")
        {
            //remove the parent (ball) from the fire so that it doesn't disappear immediately after removing the ball
            childFireObject.transform.parent = null;
            Destroy(gameObject);
        }
        
    }
}
