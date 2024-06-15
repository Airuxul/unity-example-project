using UI.Panel;

namespace UI
{
    public class BasePanel : UIWidget
    {
        public PanelConfig config;
        
        public virtual void ShowMe()
        {
            Visible = true;
        }

        public virtual void HideMe()
        {
            Visible = false;
        }
    }
}