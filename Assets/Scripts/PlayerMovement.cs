using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float smooth = 1.0f;
    public float tiltAngle = 60.0f;
    public GameObject player; //Camera 

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        tiltAngle = PickFallingSide();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        float tiltAroundZ = Input.GetAxis("Horizontal") * (tiltAngle); 
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
  
        //apply gravity force
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, tiltAngle), Time.deltaTime * smooth * Mathf.Abs(transform.rotation.z));

            
        //apply player force
        transform.Rotate(0,0,tiltAroundZ * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, tiltAroundZ), Time.deltaTime * smooth);

        if( transform.rotation.z == 0)
            tiltAngle = PickFallingSide();
       
        Debug.Log(transform.rotation.z);
        
        if( transform.rotation.z > .48f || transform.rotation.z < -.48f )
            Die();

    }

    void Die()
    {
       rb.useGravity = true;
    }

    float PickFallingSide()
    {
        if(Random.value > 0.5f)
            return 60f;
        else
            return -60f;
    }

}