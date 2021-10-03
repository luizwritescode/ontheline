using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGenerateTerrain : MonoBehaviour
{   

    public GameObject building;
    public List<GameObject> objs;
    public int buildingAmount = 100;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < buildingAmount; i++)
        {
            objs.Add(Instantiate(building, new Vector3(Mathf.Clamp(Random.value, -100, 100),Mathf.Clamp(Random.value, -5, 5),Mathf.Clamp(Random.value, -100, 100)), Quaternion.Euler(0,0,0)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
