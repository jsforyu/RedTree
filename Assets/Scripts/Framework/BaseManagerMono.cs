using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���Թ��ص������е�������,����Ƕ���ʽ
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
