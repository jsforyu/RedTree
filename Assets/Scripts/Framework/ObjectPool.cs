using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// һ����һ������أ��Ż����������������������ʱ������С����صĴ�С
/// </summary>
public class ObjectPool
{
    public Stack<GameObject> objpool=new Stack<GameObject>();
    public GameObject GetObj(string name)
    {
        GameObject result = null;
        if (objpool.Count > 0)
        {
            result = objpool.Pop();
        }
        else
        {
            result = Object.Instantiate(Resources.Load<GameObject>("Prefabs/"+name));//��Ԥ�������һ������
        }
        result.SetActive(true);
        return result;
    }

    public void DesObj(GameObject obj)
    {
        obj.SetActive(false);
        objpool.Push(obj);
    }

    public void ClearPool()
    {
        objpool.Clear();
    }
}



public class PoolManager : BaseManager<PoolManager>
{

    public Dictionary<string, ObjectPool> poollist=new Dictionary<string, ObjectPool>();


    public void CreatAPool(string name)
    {
        if (!poollist.ContainsKey(name))
        {
            poollist.Add(name, new ObjectPool());
        }
    }

    public GameObject GetGameObject(string name)
    {
        if (poollist.TryGetValue(name, out ObjectPool pool))
        {
            return pool.GetObj(name);
        }
        else
        {
            CreatAPool(name);
            return poollist[name].GetObj(name);
        }

    }

    public void DesGameObject(string name,GameObject obj)
    {
        if (poollist.TryGetValue(name, out ObjectPool pool))
        {
            pool.DesObj(obj);
        }
    }
}
