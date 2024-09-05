using System.Collections;
using System.Collections.Generic;
using UnityEngine;//ʹ������Щ�����ռ�
using UnityEngine.Events;




public enum NodeType 
{
    Normal,
    NumChildren,//�����ӽڵ�������ʾ����
    NumData,//���ݸ�ֵ��ʾ����
    //�������е����о�������ֻҪ������𣨣���
    //Ӧ�ð������ж��ٽڵ�������������ͨ��public��ʾ�����濴��ֻ��Ҷ�ӽ������޸��������
}


/// <summary>
/// ���еĸ����ڵ�
/// </summary>
public class TreeNode 
{

    public string path;
    public string parentpath;



    public NodeType type;//�ڵ������
    public int redNodeCount;//����ӽڵ������������Ϣ����Ǿ�����Ϣ��Ŀ,���ǽڵ������Ǻ����
    public bool redNodeActive;//�Ƿ񼤻�����ʾ
    

    public UnityAction<TreeNode> logicHander;//������������ί��
    public UnityAction<NodeType, bool, int> OnRedNodeActiveChange;
    //ʹ�����ί�У�OnRedNodeActiveChange=new UnityAction<NodeType, bool, int>(��Щ�����ķ���)

    /// <summary>
    /// ���ݺ��ڵ�����ͽ��и�ֵ�;�����ʾ������
    /// </summary>
    /// <returns></returns>
    //public virtual bool RefreshRedNodeState()
    //{

    //    if (type == NodeType.NumChildren)//�ڵ�����������
    //    {
    //        //redNodeCount = TreeSystem.GetInstance().GetChildCount(path);
    //        redNodeActive = redNodeCount > 0;
    //    }
    //    else
    //    {
    //        redNodeCount = RefreshRedNodeCont();//������ֻ��һ�����
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
    //    logicHander?.Invoke(this);//logichander���action�Ƿ�Ϊ�գ���Ϊ�ս�this��Ϊ����������������������
    //    OnRedNodeActiveChange?.Invoke(type, redNodeActive, redNodeCount);
    //    return redNodeActive;
    //}

    public virtual int RefreshRedNodeCont()
    {
        return 1;
    }



    public virtual bool RefreshRedNodeState()//���ڲ�������
    {
        //����count�������Ƿ񼤻�,�Ƕ�ʲôʱ��ˢ��count��
        if (redNodeCount > 0) redNodeActive = true;
        else redNodeActive = false;
        //    logicHander?.Invoke(this);//logichander���action�Ƿ�Ϊ�գ���Ϊ�ս�this��Ϊ����������������������
        if (OnRedNodeActiveChange == null)
        {
            Debug.Log("û�лص�");
        }
            OnRedNodeActiveChange?.Invoke(type, redNodeActive, redNodeCount);//���������ʾ
        Debug.Log("ִ���˻ص�"+redNodeCount);
            //    return redNodeActive;
        return redNodeActive;
    }
}
