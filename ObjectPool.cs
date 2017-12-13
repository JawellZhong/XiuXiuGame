using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//基本功能：对常用资源的创建，缓存，获取，销毁。

//对于同一个资源可以设置最大缓存数量，资源的定时销毁（如何某个资源2分钟内未使用则清空）；

public delegate T CreatObj<T>();
//对象池;
public class ObjectPool<T> where T: class
{
    public List<T> mList;//存储对象的列表;

    private CreatObj<T> CreatFun;//创建对象的方法;

    Action<T> ResetFun;//重置对象方法;
    public ObjectPool(CreatObj<T> creatFun, Action<T> resetFun)
    {
        this.mList = new List<T>();
        this.CreatFun = creatFun;
        this.ResetFun = resetFun;
    }
   
    public T Get()
    {
        T t;
        if (mList.Count > 0)
        {
            t = mList[0];
            mList.RemoveAt(0);
        }
        else
        {
            //if (CreatFun != null)
            //    return CreatFun();
           t = CreatFun == null ? default(T) : CreatFun();
        }
        if(ResetFun!=null)
            ResetFun(t);
        return t;
    }

    public void Set(T t)
    {
        mList.Add(t);
    }
}
