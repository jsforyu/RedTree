using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCon : MonoBehaviour
{
    Button bu;
    RedNodeItem item;
    //ͨ����awake��ȡʵ������start�и���Щʵ����ʼ��
    private void Awake()
    {
        item = this.GetComponentInChildren<RedNodeItem>();
        AddlistenerForButton();
    }

    public void AddlistenerForButton()
    {
        bu = GetComponent<Button>();
        bu.onClick.AddListener(ChangeCount);//�����Ժ��������
        Debug.Log("���������˰�ť�ص�"+item.path);
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
            Debug.Log(item.path + "��ʱ������");
        }
    }
}
