namespace Features.UI.Menu.Messages
{
    public enum MenuAction { NaN, MainMenu, Play, Settings, Credits, Info, Exit}
    
    public class MenuBehaviourSignals
    {
        public sealed class ButtonPressed
        {
            public string Property { get; }

            public ButtonPressed(string property)
            {
                Property = property;
            }
        }

        public sealed class ActionAdded
        {
            public MenuAction Action { get; }

            public ActionAdded(MenuAction action)
            {
                Action = action;
            }
        }
    }
}