using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float smooth = 5.0f;
    public float tiltAngle = 60.0f;
    public GameObject player; 

    public float interval = 5f;
    public float gravityTimer;

    public float pushForce = 0.8f;
    public float pushTimer;

    public float gravityForce = .1f;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        tiltAngle = PickFallingSide();

        gravityTimer = Time.time;
        pushTimer= Time.time;

    }

    private void Awake() {
        transform.rotation = new Quaternion (0,0,0,0);
    }

    // Update is called once per frame
    void Update()
    {   

        interval = Mathf.Max(5f, Random.value * 10f);

        // Movement forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        float tiltAroundZ = Input.GetAxis("Horizontal") * (tiltAngle) * ( pushForce ) ; 
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
  
        Debug.Log(tiltAroundZ);
        //apply gravity force
        if( transform.rotation.z > 0f && tiltAngle > 0f ) 
        {
            transform.Rotate(0,0, Time.deltaTime * Mathf.Abs(smooth) *  Mathf.Max(Mathf.Abs(transform.rotation.z), .2f) * gravityForce );
            Debug.Log(transform.rotation.z);
        }
        else if( transform.rotation.z < -0f && tiltAngle < 0f ) 
        {
            transform.Rotate(0,0, Time.deltaTime * Mathf.Abs(smooth) *  Mathf.Max(Mathf.Abs(transform.rotation.z), .2f) * gravityForce );
            Debug.Log(transform.rotation.z);

        }
        // else if( transform.rotation.z < -0.001f && tiltAngle > 0f ) 
        //     transform.Rotate(0,0, Mathf.Abs(Time.deltaTime * smooth *  Mathf.Max(Mathf.Abs(transform.rotation.z), .2f) * gravityForce ));
        // else if( transform.rotation.z > 0.001f && tiltAngle < 0f ) 
        //     transform.Rotate(0,0, Time.deltaTime * Mathf.Abs(smooth) *  Mathf.Max(Mathf.Abs(transform.rotation.z), .2f) * gravityForce );

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, tiltAngle), Time.deltaTime * smooth *  Mathf.Max(Mathf.Abs(transform.rotation.z), .2f));
  
        //if ( transform.rotation.z > 0 ) tiltAroundZ = -tiltAroundZ;

        //apply player force
        if ( transform.rotation.z == 0 && tiltAroundZ > 0)
            transform.rotation = new Quaternion(0,0, .00001f,0);
        else if ( transform.rotation.z == 0 && tiltAroundZ < 0)
            transform.rotation = new Quaternion(0,0, -.00001f,0);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, tiltAroundZ), Time.deltaTime * .001f);

        // Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, tiltAroundZ), Time.deltaTime * smooth);


        //Intensify player force strength
        if( Input.GetButton("Horizontal") )
            pushForce += Time.deltaTime;
        else if( Input.GetButtonUp("Horizontal"))
            pushForce = 1.0f;

        //Intensify gravity force strength
        if( ( Mathf.Abs(transform.rotation.z) == 0 ) && Time.time - gravityTimer > interval) {
            tiltAngle = PickFallingSide();
            gravityTimer = Time.time;
            gravityForce = .1f;
        } else if( tiltAngle > 0)
        {
            gravityForce += Time.deltaTime;
            tiltAngle += Time.deltaTime;
        }
        else if ( tiltAngle < 0 )
        {
            gravityForce += Time.deltaTime;
            tiltAngle -= Time.deltaTime;
        }
       
        
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

    float RandomValue( float th = 0f )
    {
        float f = Random.value;

        while(Mathf.Abs(f) < th)
            f = Random.value;
        
        return f;
    }

}