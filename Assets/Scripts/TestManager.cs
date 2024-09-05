using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;//unity自带的对象池


public class TestManager : MonoBehaviour
{
    public static TestManager instance;
    //public List<RedNodeItem> itemobj = new List<RedNodeItem>();
    private ObjectPool<GameObject> pool= new(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, true, 10, 1000);//启用安全检查为true，容量为10，最大容量为100

    private void Start()
    {
        TreeSystem.GetInstance().allNodesDic["club1"].redNodeCount = 1;
        TreeSystem.GetInstance().UpdateRedNodeState("club1");

        GameObject button1 = PoolManager.GetInstance().GetGameObject("show1");
        GameObject parent = GameObject.Find("mainCanvas/change");
        button1.transform.SetParent(parent.transform);
        //pool.Get();
        //pool.Release(obj);从池子中释放
    }

    private void Awake()
    {

        instance = this;
    }


    static GameObject createFunc()//在池子为空的时候调用
    {
        //加载一个prefab

        return null;
    }

    static void actionOnGet(GameObject obj)//从池子中获取实例
    {

    }
    static void actionOnRelease(GameObject obj)//释放一个实例
    {

    }

    static void actionOnDestroy(GameObject obj)//当池子容量最大无法回到池子中时调用
    {

    }
}
