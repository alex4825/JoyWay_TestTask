using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    Rigidbody ballRb;
    Quaternion rotation;
    float throwPower = 10f;
    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
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
            Destroy(gameObject);
        }
        
    }
}
