using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 建立整个树，提供给外部访问,写成单例
/// 把树的更新写在这里，遍历树的每个节点的更新
/// 根据读进来的路径自动生成panel？
/// </summary>
/// 
public class TreeSystem : MonoBehaviour //指定类型是TreeSystem，TreeSystem需要有无参的构造函数
{
    private static TreeSystem instance;
    public Dictionary<string, TreeNode> allNodesDic = new Dictionary<string, TreeNode>();//path，和path相对应的node    
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
            //Debug.Log("已经添加" + allNodesDic[node].path);
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
            path.AddRange(pathstring);//将字符数组转成list
            for(int i=0;i<path.Count;i++) path[i]=path[i].Replace("\r", "");
            TreeNode node = new TreeNode { path = path[path.Count - 1], parentpath = path[path.Count - 2] };
            allNodesDic.Add(path[path.Count - 1], node);
            //字典里放的是当前path和其所代表的node，node里有parentpath
        }
    }
    public void AddListenerRedNodeChange(string path,UnityAction<NodeType,bool,int> callback)//这里是用委托，相当于将函数当参数传进来（？），
    {                                                                                               //我认为是委托的这种用法，callback=方法，所以方法可以封装成一个委托传进来
        //应该给委托放监听函数了，这个委托执行，监听函数监听到执行之后执行
        if(allNodesDic.TryGetValue(path,out TreeNode node))//通过键找值,找到的值放进out这个参数中
        {
            //Debug.Log("给" + path + "添加了回调");
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
    /// 递归地更新红点和父节点的状态,往上更新
    /// </summary>
    /// <param name="path"></param>
    public void UpdateRedNodeState(string path)
    {
        Debug.Log("开始更新树");
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
            Debug.Log(path+"不存在");
        }


    }

    /// <summary>
    /// 获得子节点红点个数
    /// </summary>
    /// <returns></returns>
    public int GetChildCount(string path)//通过一个路径找其子节点个数
    {
        int count = 0;
        ComputeChildCount(path,ref count);
        return count;
    }


    /// <summary>
    ///  计算子节点个数,其实是有几个红点数
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
