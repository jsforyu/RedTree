using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 可以挂载到场景中的物体上,这个是饿汉式
/// </summary>
public class BaseManagerMono<T> : MonoBehaviour where T: MonoBehaviour
{

    private static T instance;
    public static T GetInstance()
    {
        
            return instance;
    }

    private void Awake()
    {
        instance = this as T;
    }
}
