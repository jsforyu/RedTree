using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// �������������ṩ���ⲿ����,д�ɵ���
/// �����ĸ���д�������������ÿ���ڵ�ĸ���
/// ���ݶ�������·���Զ�����panel��
/// </summary>
/// 
public class TreeSystem : MonoBehaviour //ָ��������TreeSystem��TreeSystem��Ҫ���޲εĹ��캯��
{
    private static TreeSystem instance;
    public Dictionary<string, TreeNode> allNodesDic = new Dictionary<string, TreeNode>();//path����path���Ӧ��node    
    public List<RedNodeItem> itemobj = new List<RedNodeItem>();
    private void Awake()
    {
        instance = this;
        string[] data = ReadData.GetInstance().ReadDataFromTxt("TreeData");
        IniateRedTree(data);
        int i = 0;
        foreach (var node in allNodesDic.Keys)
        {
            itemobj[i].path = allNodesDic[node].path;
            AddListenerRedNodeChange(itemobj[i].path, itemobj[i].OnRedNodeStateChangeEvent);
            //Debug.Log("�Ѿ����" + allNodesDic[node].path);
            i++;
        }
    }

    public static TreeSystem GetInstance()
    {
        return instance;
    }
    public void IniateRedTree(string[] treedata)
    {
        
        foreach(var data in treedata)
        {
            string[] pathstring = data.Split("/");
            List<string> path = new List<string>();
            path.AddRange(pathstring);//���ַ�����ת��list
            for(int i=0;i<path.Count;i++) path[i]=path[i].Replace("\r", "");
            TreeNode node = new TreeNode { path = path[path.Count - 1], parentpath = path[path.Count - 2] };
            allNodesDic.Add(path[path.Count - 1], node);
            //�ֵ���ŵ��ǵ�ǰpath�����������node��node����parentpath
        }
    }
    public void AddListenerRedNodeChange(string path,UnityAction<NodeType,bool,int> callback)//��������ί�У��൱�ڽ�������������������������
    {                                                                                               //����Ϊ��ί�е������÷���callback=���������Է������Է�װ��һ��ί�д�����
        //Ӧ�ø�ί�зż��������ˣ����ί��ִ�У���������������ִ��֮��ִ��
        if(allNodesDic.TryGetValue(path,out TreeNode node))//ͨ������ֵ,�ҵ���ֵ�Ž�out���������
        {
            //Debug.Log("��" + path + "����˻ص�");
            node.OnRedNodeActiveChange += callback;
        }
        else
        {
            Debug.LogError($"key:{path} not exits");
        }
    }
    public void RemoveListenerRedNodeChange(string path, UnityAction<NodeType, bool, int> callback)
    {
        if(allNodesDic.TryGetValue(path,out TreeNode node))
        {
           node.OnRedNodeActiveChange -= callback;
        }
    }

    /// <summary>
    /// �ݹ�ظ��º��͸��ڵ��״̬,���ϸ���
    /// </summary>
    /// <param name="path"></param>
    public void UpdateRedNodeState(string path)
    {
        Debug.Log("��ʼ������");
        if (allNodesDic.TryGetValue(path,out TreeNode node))
        {
            if (node.redNodeCount == 1&& allNodesDic.ContainsKey(node.parentpath)) allNodesDic[node.parentpath].redNodeCount = 1;
            if(node.redNodeCount==0 && allNodesDic.ContainsKey(node.parentpath)) allNodesDic[node.parentpath].redNodeCount = 0;
            //Debug.Log(node.path + node.parentpath);
            node.RefreshRedNodeState();
            UpdateRedNodeState(node.parentpath);
        }
        else
        {
            Debug.Log(path+"������");
        }


    }

    /// <summary>
    /// ����ӽڵ������
    /// </summary>
    /// <returns></returns>
    public int GetChildCount(string path)//ͨ��һ��·�������ӽڵ����
    {
        int count = 0;
        ComputeChildCount(path,ref count);
        return count;
    }


    /// <summary>
    ///  �����ӽڵ����,��ʵ���м��������
    /// </summary>
    /// <param name="path"></param>
    /// <param name="a"></param>
    public void ComputeChildCount(string path,ref int count )
    {

        foreach(var item in allNodesDic.Values)
        {
            if (item.parentpath == path)
            {
                if (item.redNodeActive)
                {
                    count += item.redNodeCount;
                }
            }
        }
    }
}
