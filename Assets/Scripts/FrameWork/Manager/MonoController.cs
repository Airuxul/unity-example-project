using UnityEngine;
using UnityEngine.Events;


namespace FrameWork.Manager
{
    public class MonoController : MonoBehaviour
    {
        private event UnityAction UpdateEvent;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            UpdateEvent?.Invoke();
        }

        public void AddUpdateListener(UnityAction func)
        {
            UpdateEvent += func;
        }

        public void RemoveUpdateListener(UnityAction func)
        {
            UpdateEvent -= func;
        }

        private void OnDestroy()
        {
            UpdateEvent = null;
        }
    }
}