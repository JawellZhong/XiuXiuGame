using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//事件委托;
public delegate void EventFun<T>(T obj);

//事件管理器;
public class EventManager<T>
{
    //用来存方法的字典，通过事件ID将方法进行分类存储；
    private Dictionary<int, List<EventFun<T>>> mAllEvents;

    public EventManager()
    {
        mAllEvents = new Dictionary<int, List<EventFun<T>>>();
    }
    //添加事件；varType：事件ID,varFun:等待被通知的方法;
    public void AddEvent(int varType, EventFun<T> varFun)
    {
        List<EventFun<T>> funList;
        //如果没有这个Key的键值对;
        if (!mAllEvents.TryGetValue(varType, out funList))
        {
            //创建一个新的方法集合；
            funList = new List<EventFun<T>>();
            //将新的方法集合添加到字典(新的键值对);
            mAllEvents.Add(varType, funList);
        }
        //将等待被通知的方法加入到这件事情对应的集合;
        funList.Add(varFun);
    }
    //移除事件；varType：事件ID,varFun:等待被通知的方法;
    public void ReMoveEvent(int varType, EventFun<T> varFun)
    {
        List<EventFun<T>> funList;
        //如果有这个Key的键值对;
        if (mAllEvents.TryGetValue(varType, out funList))
        {
            funList.Remove(varFun);
        }
    }
    //通知事件;varType：事件ID,obj:事件的参数;
    public void Notify(int varType, T obj)
    {
        List<EventFun<T>> funList;
        //如果有这个Key的键值对;
        if (mAllEvents.TryGetValue(varType, out funList))
        {
            //调用这个集合里面的所有委托方法;
            foreach (var item in funList)
            {
                item(obj);//通知;
            }
        }
    }
}
