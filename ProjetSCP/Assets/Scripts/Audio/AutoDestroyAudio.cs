using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAudio : MonoBehaviour
{
    public bool isUsed = true;

    private float timeUntilUsed = 0;

    void Update()
    {
        if (isUsed == false)
        {
            Destroy(gameObject);
        }

        if (timeUntilUsed > 3)
        {
            Destroy(gameObject);
        }
        else if(!isUsed)
        {
            timeUntilUsed += Time.deltaTime;
        }
    }
}
