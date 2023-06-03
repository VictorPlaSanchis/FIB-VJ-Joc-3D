using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gluTrap : MonoBehaviour
{

    public float slowerFactor = 1.001f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
            other.gameObject.GetComponent<playerMovement>().scaleSpeed(1.0f / slowerFactor);
            other.gameObject.GetComponent<playerMovement>().setCanJump(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.GetComponent<playerMovement>().scaleSpeed(slowerFactor);
            other.gameObject.GetComponent<playerMovement>().setCanJump(true);
    }

}
