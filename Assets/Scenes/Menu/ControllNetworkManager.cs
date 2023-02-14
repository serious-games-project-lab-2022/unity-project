using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllNetworkManager : MonoBehaviour
{
    public static ControllNetworkManager Singleton { get; private set; }
    private void Awake()
    {
        var singletonAlreadyExists = Singleton != null && Singleton != this;
        if (singletonAlreadyExists)
        {
            Destroy(this.gameObject);
            return;
        }
        Singleton = this;

    }

}
