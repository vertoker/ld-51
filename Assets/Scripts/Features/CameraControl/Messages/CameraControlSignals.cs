using Features.UI.Menu.Messages;
using UnityEngine;

namespace Features.CameraControl.Messages
{
    public enum EntityViewOption { First, Third, ActorFocused }
    public class CameraControlSignals
    {
        public sealed class CameraToMenu
        {
            public MenuAction TranslateOption { get; }

            public CameraToMenu(MenuAction translateOption)
            {
                TranslateOption = translateOption;
            }
        }
        
        public sealed class CameraToObject
        {
            public EntityViewOption ViewOption { get; }
            public Transform Transform { get; }
            public Vector3 Offset { get; }
            public bool PresenterMode { get; }
            
            public CameraToObject(EntityViewOption viewOption, Transform transform, Vector3 offset, bool presenterMode)
            {
                ViewOption = viewOption;
                Transform = transform;
                Offset = offset;
                PresenterMode = presenterMode;
            }
        }
    }
}