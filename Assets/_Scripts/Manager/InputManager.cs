// John van den Berg

/*=============================================================================
	InputManager
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * Manager: InputManager
 * 
 * The InputManager is used to map all input keys to game actions.
 */
namespace Manager
{
    public class InputManager
    {
        public static KeyCode ActionBarButtonPrimary { get { return KeyCode.Mouse0; } }
        public static KeyCode ActionBarButtonSecondary { get { return KeyCode.Mouse1; } }
        public static KeyCode ActionBarButton1 { get { return KeyCode.Alpha1; } }
        public static KeyCode ActionBarButton2 { get { return KeyCode.Alpha2; } }
        public static KeyCode ActionBarButton3 { get { return KeyCode.Alpha3; } }
        public static KeyCode ActionBarButton4 { get { return KeyCode.Alpha4; } }
    }
}
