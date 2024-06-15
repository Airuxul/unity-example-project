using UI.Panel;

namespace UI
{
    public class BasePanel : UIWidget
    {
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