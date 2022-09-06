using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCamera : MonoBehaviour
{
    private void Start()
    {
        FadeOut();
    }
    public void FadeIn()
    {
        iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 1f, "amount", 0f, "time", 1f));
    }

    public void FadeOut()
    {
        iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 0f, "amount", 0f, "time", 1f));
    }
}
