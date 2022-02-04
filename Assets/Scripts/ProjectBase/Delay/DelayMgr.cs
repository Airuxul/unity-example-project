using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayMgr : BaseManager<DelayMgr>
{
      //字典延迟Line
        private readonly Dictionary<string, DelayLine> _delayLines = new Dictionary<string, DelayLine>();
        //默认事件
        private const string DefaultLineName = "default";

        public DelayMgr()
        {
            _delayLines.Add(DefaultLineName, new DelayLine());
        }
        public void AddLine(string lineName)
        {
            if (_delayLines.ContainsKey(lineName))
            {
                Debug.LogError("已经有该名字的延迟Line");
            }
            else
            {
                _delayLines.Add(lineName, new DelayLine());
            }
        }
        public void RemoveLine(string lineName)
        {
            if (_delayLines.ContainsKey(lineName))
            {
                if (lineName == DefaultLineName)
                {
                    Debug.LogError("不允许删除默认延迟Line");
                    return;
                }
                _delayLines[lineName].Dispose();
                _delayLines.Remove(lineName);
                
            }
            else
            {
                Debug.LogError("没有该名字的延迟Line");
            }
        }
        public void ShowAllLine()
        {
            string debugInfo = "共有延迟线：\n";
            foreach (KeyValuePair<string, DelayLine> delayLine in _delayLines)
            {
                debugInfo += delayLine.Key + '\n';
            }
            Debug.Log(debugInfo);
        }

        #region 添加延迟事件
         public void AddDelayAction( UnityAction action, float time,string lineName=DefaultLineName)
        {
            if (_delayLines.ContainsKey(lineName))
            {
                _delayLines[lineName].AddDelayAction(action, time);
            }
            else
            {
                Debug.LogError("没有\""+lineName+"\"该延迟Line");
            }
            
        }
        public void AddDelayAction<T>( UnityAction<T> action, float time,string lineName=DefaultLineName)
        {
            if (_delayLines.ContainsKey(lineName))
            {
                _delayLines[lineName].AddDelayAction(action, time);
            }
            else
            {
                Debug.LogError("没有\""+lineName+"\"该延迟Line");
            }
        }
        #endregion

        #region 实现延迟事件
    public void InvokeDelayActions(string lineName=DefaultLineName)
        {
            if (_delayLines.ContainsKey(lineName))
            {
                _delayLines[lineName].InvokeDelayActions();
            }
            else
            {
                Debug.LogError("没有\""+lineName+"\"该延迟Line");
            }
        }
        public void InvokeDelayActions<T>(T[] info ,string lineName=DefaultLineName)
        {
            if (_delayLines.ContainsKey(lineName))
            {
                _delayLines[lineName].InvokeDelayActions(info);
            }
            else
            {
                Debug.LogError("没有\""+lineName+"\"该延迟Line");
            }
        }
        #endregion
}
