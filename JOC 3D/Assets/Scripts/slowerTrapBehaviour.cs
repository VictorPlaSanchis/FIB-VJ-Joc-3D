using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowerTrapBehaviour : MonoBehaviour
{

    public float slowerFactor = 4.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.GetComponent<playerMovement>().scaleSpeed(1.0f / slowerFactor);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.GetComponent<playerMovement>().scaleSpeed(slowerFactor);
    }

}
