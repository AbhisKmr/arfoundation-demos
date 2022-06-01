using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacement : MonoBehaviour
{
    [SerializeField]
    GameObject product;

    void Update()
    {
        var p = product.transform.position;
        var y = transform.position.y;
        transform.position = new Vector3(p.x, y, p.z);
    }
}
