using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonExample : MonoBehaviour
{
    private static SingletonExample instance;
    private SingletonExample() { }
    public static SingletonExample getInstance()
    {
        if(instance == null)
            instance = new SingletonExample();
        return instance;
    }

    
}
