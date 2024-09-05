using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCon : MonoBehaviour
{
    Button bu;
    RedNodeItem item;
    //通常在awake获取实例，在start中给这些实例初始化
    private void Awake()
    {
        item = this.GetComponentInChildren<RedNodeItem>();
        AddlistenerForButton();
    }

    public void AddlistenerForButton()
    {
        bu = GetComponent<Button>();
        bu.onClick.AddListener(ChangeCount);//点了以后消除红点
        Debug.Log("给这个添加了按钮回调"+item.path);
    }
    
    public void ChangeCount()
    {
        if(TreeSystem.GetInstance().allNodesDic.TryGetValue(item.path,out TreeNode node))
        {
            node.redNodeCount = 0;
            TreeSystem.GetInstance().UpdateRedNodeState(item.path);
        }
        else
        {
            Debug.Log(item.path + "暂时不存在");
        }
    }
}
