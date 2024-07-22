using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Director : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayableAsset otherTimeAsset;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playableDirector.Play(otherTimeAsset);
        }
    }
}
