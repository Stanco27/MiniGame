using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if(player)  {
        transform.localPosition = new Vector3(player.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
    }


}
