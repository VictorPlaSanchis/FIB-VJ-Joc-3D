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

    public void alreadyTurned() {
        platformType = PlatformType.Jump;
    }


}
