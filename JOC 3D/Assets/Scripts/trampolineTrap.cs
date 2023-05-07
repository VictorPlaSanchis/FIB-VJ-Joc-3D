using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolineTrap : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.GetComponent<playerMovement>().Jump();
    }

}
