using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotLamp : MonoBehaviour
{
    public Vector3 rotTo;
    public float speed;
    public iTween.EaseType easeType;
    public iTween.LoopType loopType;
    void Start()
    {
        iTween.RotateTo(this.gameObject, iTween.Hash("rotation", rotTo, "speed", speed, "easetype", easeType, "looptype", loopType));
    }

}
