using System.Collections;
using System.Collections.Generic;
using FrameWork;
using FrameWork.Manager;
using UnityEngine;
using UnityEngine.Events;
class DelayEventInfo : EventInfo
{
    private float _delayTime;
    public float DelayTime
    {
        private set
        {
            if (value < 0)
            {
                Debug.LogError("延迟事件的延迟时间不能为负");
            }
                
            _delayTime = value;
        }
        get => _delayTime;
    }
    public DelayEventInfo(UnityAction action, float delayTime) : base(action) { this.DelayTime = delayTime; }
}
class DelayEventInfo<T> : EventInfo<T>
{
    private float _delayTime;
    public float DelayTime
    {
        private set
        {
            if (value < 0)
            {
                Debug.LogError("延迟事件的延迟时间不能为负");
            }
            _delayTime = value;
        }
        get => _delayTime;
    }
    public DelayEventInfo(UnityAction<T> action, float delayTime) : base(action) { this.DelayTime = delayTime; }
}
/// <summary>
/// 单线延迟，当执行Invoke时则将之前的事件流给消除
/// 使用队列的数据结构保存所有方法信息以及延迟时间，按照队列的方式先入先出的依次实现延迟事件流
/// 每个事件都会在之前的事件延迟后延迟实现
/// 比如输入事件a延迟1秒，事件b延迟2秒，事件c延迟3秒
/// 例子：数字表示时间
///     0 1 2 3 4 5 6
///       a   b     c
/// 当所有事件结束后将队列以及保存协程的信息清空
/// </summary>
public class DelayLine
{
        //记录未执行事件流
        private Queue<IEventInfo> _delayActions = new Queue<IEventInfo>();
        //记录执行协程的事件流
        private List<Coroutine> _ies = new List<Coroutine>();
        public void AddDelayAction(UnityAction action, float time)
        {
            _delayActions.Enqueue(new DelayEventInfo(action, time));
        }
        public void AddDelayAction<T>(UnityAction<T> action, float time)
        {
            _delayActions.Enqueue(new DelayEventInfo<T>(action, time));
        }
        private void ClearDelayActions()
        {
            _delayActions.Clear();
        }
        public void InvokeDelayActions()
        {
            StopDelayEnumerator();
            float currentDelayTime = 0;
            DelayEventInfo currentDelayEvent;
            while (_delayActions.Count != 0)
            {
                currentDelayEvent = _delayActions.Dequeue() as DelayEventInfo;
                currentDelayTime += currentDelayEvent.DelayTime;
                _ies.Add(AppFacade.MonoMgr.StartCoroutine(DelayToCallEnumerator(currentDelayEvent.actions, currentDelayTime)));
            }
            ClearDelayActions();
        }
        public void InvokeDelayActions<T>(T[] info)
        {
            StopDelayEnumerator();
            float currentDelayTime = 0;
            int currentIndex = 0;
            DelayEventInfo<T> currentDelayEvent;
            while (_delayActions.Count != 0)
            {
                currentDelayEvent = _delayActions.Dequeue() as DelayEventInfo<T>;   
                currentDelayTime += currentDelayEvent.DelayTime;
                _ies.Add(AppFacade.MonoMgr.StartCoroutine(DelayToCallEnumerator(currentDelayEvent.actions, info[currentIndex], currentDelayTime)));
                currentIndex++;
            }
            ClearDelayActions();
        }
        private IEnumerator DelayToCallEnumerator(UnityAction action, float time)
        {
            yield return new WaitForSeconds(time);
            action();
        }
        private IEnumerator DelayToCallEnumerator<T>(UnityAction<T> action, T info, float time)
        {
            yield return new WaitForSeconds(time);
            action(info);
        }
        private void StopDelayEnumerator()
        {
            foreach (Coroutine e in _ies)
            {
                AppFacade.MonoMgr.StopCoroutine(e);
            }
            _ies.Clear();
        }
        public void Dispose()
        {
            ClearDelayActions();
            StopDelayEnumerator();
            _delayActions = null;
            _ies = null;
        }
}

