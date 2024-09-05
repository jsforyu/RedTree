using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


/// <summary>
/// 本质是一个面板（？）,控制显示
/// </summary>
public class RedNodeItem : BasePanel
{
    public string path;
    public  GameObject redNodeOhj;
    public  Text countText;//显示数字的，应该从basepanel那里拿UI组件吧

    private new void Awake()
    {
        //Debug.Log("开始添加回调");
        //TreeSystem.GetInstance().AddListenerRedNodeChange(path, OnRedNodeStateChangeEvent);//这放的不还是方法，chatgpt说编译器会把方法封装成一个委托
        //TreeSystem.GetInstance().UpdateRedNodeState(path);//在这每个点刷新，一运行就刷新，固定按帧刷新就用这个函数，这样刷新感觉刷新顺序会重要（？）
    }



    public void OnRedNodeStateChangeEvent(NodeType type,bool active,int count )
    {
        //Debug.Log("执行到这里了");
        if (active) redNodeOhj.SetActive(true);
        else redNodeOhj.SetActive(false);
        if (type == NodeType.NumChildren || type == NodeType.NumData)
        {
            countText.text = count.ToString();
        }
        countText.gameObject.SetActive(type != NodeType.Normal);
    }

}