using System.Collections;
using System.Collections.Generic;
using UnityEngine;//使用了这些命名空间
using UnityEngine.Events;




public enum NodeType 
{
    Normal,
    NumChildren,//根据子节点数量显示数字
    NumData,//根据赋值显示数字
    //这块分类有点多余感觉，可以只要两个类别（？）
    //应该把下面有多少节点红点个数这个数字通过public显示给外面看，只有叶子结点可以修改这个数字
}


/// <summary>
/// 树中的各个节点
/// </summary>
public class TreeNode 
{

    public string path;
    public string parentpath;



    public NodeType type;//节点的类型
    public int redNodeCount;//红点子节点数，如果跟消息结合那就是消息数目,不是节点数，是红点数
    public bool redNodeActive;//是否激活红点显示
    

    public UnityAction<TreeNode> logicHander;//声明带参数的委托
    public UnityAction<NodeType, bool, int> OnRedNodeActiveChange;
    //使用这个委托：OnRedNodeActiveChange=new UnityAction<NodeType, bool, int>(这些参数的方法)

    /// <summary>
    /// 根据红点节点的类型进行赋值和决定显示和隐藏
    /// </summary>
    /// <returns></returns>
    //public virtual bool RefreshRedNodeState()
    //{

    //    if (type == NodeType.NumChildren)//节点类型是数字
    //    {
    //        //redNodeCount = TreeSystem.GetInstance().GetChildCount(path);
    //        redNodeActive = redNodeCount > 0;
    //    }
    //    else
    //    {
    //        redNodeCount = RefreshRedNodeCont();//其他的只有一个红点
    //    }
    //    if (type == NodeType.NumData)
    //    {
    //        if (redNodeCount > 0)
    //        {
    //            redNodeActive = true;
    //        }
    //        else
    //        {
    //            redNodeActive = false;
    //        }
    //    }
    //    logicHander?.Invoke(this);//logichander这个action是否为空，不为空将this作为参数启动监听函数（？）
    //    OnRedNodeActiveChange?.Invoke(type, redNodeActive, redNodeCount);
    //    return redNodeActive;
    //}

    public virtual int RefreshRedNodeCont()
    {
        return 1;
    }



    public virtual bool RefreshRedNodeState()//现在不管类型
    {
        //根据count来决定是否激活,那额什么时候刷新count呢
        if (redNodeCount > 0) redNodeActive = true;
        else redNodeActive = false;
        //    logicHander?.Invoke(this);//logichander这个action是否为空，不为空将this作为参数启动监听函数（？）
        if (OnRedNodeActiveChange == null)
        {
            Debug.Log("没有回调");
        }
            OnRedNodeActiveChange?.Invoke(type, redNodeActive, redNodeCount);//对外控制显示
        Debug.Log("执行了回调"+redNodeCount);
            //    return redNodeActive;
        return redNodeActive;
    }
}
