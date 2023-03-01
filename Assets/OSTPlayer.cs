using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSTPlayer : MonoBehaviour
{
    public static OSTPlayer instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
