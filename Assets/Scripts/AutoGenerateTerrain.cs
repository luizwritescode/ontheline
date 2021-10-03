using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGenerateTerrain : MonoBehaviour
{   

    public GameObject building;
    public List<GameObject> objs;
    public int buildingAmount;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < buildingAmount; i++)
        {
            objs.Add(
                Instantiate<GameObject>(
                    building,
                    new Vector3(
                        RandomValue(5f) * 100 - 100/2,
                        Random.value * 10 - 10/2,
                        Random.value * 2000),
                    Quaternion.Euler(0,0,0)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float RandomValue( float th = 0f )
    {
        float f = Random.value;

        while(Mathf.Abs(f) < th)
            f = Random.value;
        
        return f;
    }
}
