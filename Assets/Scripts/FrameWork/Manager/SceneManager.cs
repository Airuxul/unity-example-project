using System.Collections;
using UnityEngine;
using UnityEngine.Events;

//场景切换模块
namespace FrameWork.Manager
{
    public class SceneManager : BaseManager
    {
        public string currentSceneName = "StartUp";
        
        //切换场景
        public void LoadScene(string name, UnityAction func = null)
        {
            //场景同步加载
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
            currentSceneName = name;
            //加载完成过后才会执行func
            func?.Invoke();
        }

        public void LoadSceneAsync(string name, UnityAction func = null)
        {
            //公共Mono模块
            AppFacade.MonoManager.StartCoroutine(ReallyLoadSceneAsync(name, func));
        }

        private IEnumerator ReallyLoadSceneAsync(string name, UnityAction func)
        {
            AsyncOperation ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);
            while (!ao.isDone)
            {
                AppFacade.EventManager.EventTrigger("Loading", ao.progress);
                yield return ao.progress;
            }
            currentSceneName = name;
            //加载完成后执行func
            func?.Invoke();
        }
    }
}