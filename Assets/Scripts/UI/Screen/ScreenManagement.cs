using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DroidDigital.PacMan.UI
{
    static internal class ScreenManagement
    {
        public static List<Screen> ScreenList = new List<Screen>();
        
        public static void GetScreens()
        {
            ScreenList = GameObject.FindObjectsOfType<Screen>().ToList();
        }

        public static void ActiveScreen(Screen.ScreenName screenToActivate)
        {
            ScreenList.FirstOrDefault(e => e.Name == screenToActivate).enabled = true;

            ScreenList.Where(e => e.Name != screenToActivate).All(e => e.IsEnable = false);
        }
    }
}