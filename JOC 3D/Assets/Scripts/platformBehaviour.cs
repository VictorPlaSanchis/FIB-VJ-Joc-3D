using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformBehaviour : MonoBehaviour
{

    public enum PlatformType { 
        Turn,
        FakeTurn,
        TurnAndJump,
        Jump
    };

    public PlatformType platformType = PlatformType.Jump;

    public void Start()
    {
        if (platformType == PlatformType.Turn || platformType == PlatformType.TurnAndJump)
        {
            setTurnIndicator(true);
        }
        else if (platformType == PlatformType.FakeTurn)
        {
            setFakeTurnIndicator(true);
        }
        else setTurnIndicator(false);
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

    public void setFakeTurnIndicator(bool enable)
    {
        foreach (Transform child in transform)
        {
            if (child.name == "fakeTurnIndicator")
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = enable;
            }
        }
    }

    public void alreadyTurned() {
        platformType = PlatformType.Jump;
        setTurnIndicator(false);
        setFakeTurnIndicator(false);
    }


}
