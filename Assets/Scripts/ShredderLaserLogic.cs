using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderLaserLogic : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        if(collidingObject.tag == "isLaser")
        {
            Destroy(collidingObject);
        }
    }

}
