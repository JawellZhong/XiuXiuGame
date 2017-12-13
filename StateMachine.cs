using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态基类;
public class State
{
    private int mName;//状态名字;
    private StateMachine mMachine;//所属的状态机;
    public State(int varName)
    {
        this.mName = varName;
    }
    public StateMachine pMachine
    {
        get
        {
            return mMachine;
        }

        set
        {
            mMachine = value;
        }
    }
    //状态名字的属性;
    public int pName
    {
        get
        {
            return mName;
        }
    }
    //状态的开始;
    public virtual void OnStart() { }
    //状态的更新;
    public virtual void OnUpdate() { }
    //状态的结束;
    public virtual void OnEnd() { }
}
//状态机;
public class StateMachine
{
    //保存所有的状态;
    Dictionary<int, State> mAllStates;
    State mCurState;//当前状态；
    //当前状态属性;
    public State pCurState
    {
        get
        {
            return mCurState;
        }
    }
    //状态机构造函数;
    public StateMachine()
    {
        mAllStates = new Dictionary<int, State>();
    }
    //状态机更新方法;
    public void Update()
    {
        //更新当前状态;
        if(mCurState!=null)
            mCurState.OnUpdate();
    }
    //添加状态;
    public void AddState(State varState)
    {
        if (!mAllStates.ContainsKey(varState.pName))
        {
            varState.pMachine = this;
            mAllStates.Add(varState.pName, varState);
        }
    }
    //切换状态;
    public void SetState(int varState)
    {
        State state;
        if (mAllStates.TryGetValue(varState, out state))
        {
            if (mCurState != null)
            {
                //结束当前状态；
                mCurState.OnEnd();
            }

            mCurState = state;//将最新的状态赋值给当前状态;
            if (mCurState != null)
            {
                mCurState.OnStart();//当前状态开始;
            }
           
        }
    }
    //获取一个状态;
    public State GetState(int varState)
    {
        State state;
        mAllStates.TryGetValue(varState, out state);
        return state;
    }
}
