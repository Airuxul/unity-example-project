using System.Collections.Generic;
using UnityEngine.Events;

namespace FrameWork.Manager
{
    public interface IEventInfo
    {
    }

    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }


    public class EventManager : BaseManager
    {
        private Dictionary<string, IEventInfo> _eventDic = new();

        public void AddEventListener<T>(string name, UnityAction<T> action)
        {
            if (_eventDic.TryGetValue(name, value: out var value))
            {
                if (value is EventInfo<T> eventInfo)
                {
                    eventInfo.actions += action;
                }
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T>(action));
            }
        }

        public void AddEventListener(string name, UnityAction action)
        {
            if (_eventDic.TryGetValue(name, out var value))
            {
                if (value is EventInfo eventInfo)
                {
                    eventInfo.actions += action;
                }
            }
            else
            {
                _eventDic.Add(name, new EventInfo(action));
            }
        }

        public void EventTrigger<T>(string name, T info)
        {
            if (_eventDic.TryGetValue(name, out var value))
            {
                if (value is EventInfo<T> eventInfo)
                {
                    eventInfo.actions?.Invoke(info);
                }
            }
        }

        public void EventTrigger(string name)
        {
            if (_eventDic.TryGetValue(name, out var value))
            {
                if (value is EventInfo eventInfo)
                {
                    eventInfo.actions?.Invoke();
                }
            }
        }

        public void RemoveEventListener<T>(string name, UnityAction<T> action)
        {
            if (_eventDic.TryGetValue(name, out var value))
            {
                if (value is EventInfo eventInfo)
                {
                    eventInfo.actions?.Invoke();
                }
            }
        }

        public void RemoveEventListener(string name, UnityAction action)
        {
            if (_eventDic.TryGetValue(name, out var value))
            {
                if (value is EventInfo eventInfo)
                {
                    eventInfo.actions -= action;
                }
            }
        }

        private void Clear()
        {
            _eventDic.Clear();
        }

        public override void Destroy()
        {
            Clear();
            _eventDic = null;
        }
    }
}