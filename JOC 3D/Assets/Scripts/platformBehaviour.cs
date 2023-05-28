using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformBehaviour : MonoBehaviour
{

    public enum PlatformType { 
        Turn,
        TurnAndJump,
        Jump
    };

    public PlatformType platformType = PlatformType.Jump;

    public void Start()
    {
        if(platformType != PlatformType.Jump) {
            setTurnIndicator(true);
        }
    }

    public void setTurnIndicator(bool enable)
    {
        foreach (Transform child in transform)
        {
            if (child.name == "turnIndicator")
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = enable;
            }
        }
    }

    public void alreadyTurned() {
        platformType = PlatformType.Jump;
        setTurnIndicator(false);
    }


}
