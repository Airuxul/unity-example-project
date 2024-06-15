using System;
using UnityEngine;

namespace UI.Panel
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWidget : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private bool _visible;
        public bool Visible
        {
            get => _visible;
            set
            {
                if (_visible == value)
                {
                    return;
                }
                _visible = value;
                OnVisibleChanged();
            }
        }

        public virtual void OnVisibleChanged()
        {
            _canvasGroup.alpha = _visible ? 1 : 0;
            _canvasGroup.blocksRaycasts = _visible;
            _canvasGroup.interactable = _visible;
        }
    }
}