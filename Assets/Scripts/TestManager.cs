using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;//unity�Դ��Ķ����


public class TestManager : MonoBehaviour
{
    public static TestManager instance;
    //public List<RedNodeItem> itemobj = new List<RedNodeItem>();
    private ObjectPool<GameObject> pool= new(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, true, 10, 1000);//���ð�ȫ���Ϊtrue������Ϊ10���������Ϊ100

    private void Start()
    {
        TreeSystem.GetInstance().allNodesDic["club1"].redNodeCount = 1;
        TreeSystem.GetInstance().UpdateRedNodeState("club1");

        GameObject button1 = PoolManager.GetInstance().GetGameObject("show1");
        GameObject parent = GameObject.Find("mainCanvas/change");
        button1.transform.SetParent(parent.transform);
        //pool.Get();
        //pool.Release(obj);�ӳ������ͷ�
    }

    private void Awake()
    {

        instance = this;
    }


    static GameObject createFunc()//�ڳ���Ϊ�յ�ʱ�����
    {
        //����һ��prefab

        return null;
    }

    static void actionOnGet(GameObject obj)//�ӳ����л�ȡʵ��
    {

    }
    static void actionOnRelease(GameObject obj)//�ͷ�һ��ʵ��
    {

    }

    static void actionOnDestroy(GameObject obj)//��������������޷��ص�������ʱ����
    {

    }
}
