using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Rotation : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(10f, 20f, 30f) * Time.deltaTime, Space.World);
    }
}
