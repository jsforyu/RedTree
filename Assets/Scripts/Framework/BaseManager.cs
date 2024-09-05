using System.Collections;
using UnityEngine;


/// <summary>
/// 单例类的基类，所有与单例和Manager有关的单例类继承这个,这个是懒汉式
/// </summary>
public class BaseManager<T>where T:new()
{
    private static T instance;
    public static T GetInstance()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }

}
