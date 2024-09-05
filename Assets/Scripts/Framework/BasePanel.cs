using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// Panel基类
/// </summary>
public class BasePanel : MonoBehaviour
{
    //key是物体的名字，根据key找与其相关ui组件的list
    protected Dictionary<string, List<UIBehaviour>> DicContro = new Dictionary<string, List<UIBehaviour>>();
    //一个物体上面有按钮，文字啥的
    //这个panel上的物体和其身上的组件，根据组件找到物体，那么就有一个问题，需要遍历所有的组件
    //感觉后面得改成按照物体找组件（？）g根据组件找子物体是有什么好处么
    //查找子物体tranform.getchild
    
    protected virtual void Awake()
    {
        FindChildsUIComponents<Button>();
        FindChildsUIComponents<Image>();
        FindChildsUIComponents<Toggle>();
        FindChildsUIComponents<Slider>();
        FindChildsUIComponents<ScrollRect>();
        FindChildsUIComponents<Text>();
        FindChildsUIComponents<GridLayoutGroup>();
    }


    private void FindChildsUIComponents<T>() where T : UIBehaviour
    {
        T[] s = this.GetComponentsInChildren<T>();//这个panel上的子物体组件
        for (int i = 0; i < s.Length; i++)
        {
            string name = s[i].gameObject.name;//根据组件找物体
            if (DicContro.ContainsKey(name))
            {
                DicContro[name].Add(s[i]);
            }
            else
            {
                DicContro.Add(name, new List<UIBehaviour>() { s[i] });
            }
            if(s[i] is Button)//类型转换检查,is不抛出异常
            {
                (s[i] as Button).onClick.AddListener(() =>//as类型转换，会抛出异常
                {
                    //是按钮添加监听
                    
                }
                );
            }else if(s[i] is Toggle)//Toggle是开关
            {
                (s[i] as Toggle).onValueChanged.AddListener((b) => { }
                    
                );
            }
        }
    }

    /// <summary>
    /// 获取某nameUI面板下面的m某个UI组件
    /// </summary>
    /// <typeparam name="T">UI组件类型</typeparam>
    /// <param name="name">UI面板物体名字</param>
    /// <returns></returns>
    public T GetUIContro<T>(string name) where T : UIBehaviour
    {
        if (DicContro.ContainsKey(name))
        {
            for(int i = 0; i < DicContro[name].Count; i++)
            {
                return DicContro[name][i] as T;
            }
        }
        return null;
    }
}