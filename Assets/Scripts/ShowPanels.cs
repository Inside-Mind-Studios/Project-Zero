using UnityEngine;
using System.Collections;

namespace sparky
{
    public class ShowPanels : MonoBehaviour
    {
        public GameObject[] panels;

        //Call this function to activate and display the Options panel during the main menu
        public void ShowPanel(GameObject panel)
        {
            panel.SetActive(true);
        }

        //Call this function to deactivate and hide the Options panel during the main menu
        public void HidePanel(GameObject panel)
        {
            panel.SetActive(false);
        }

        //Call this function to activate and display the main menu panel during the main menu
        public void ShowMenu()
        {
            panels[0].SetActive(true);
        }

        //Call this function to deactivate and hide the main menu panel during the main menu
        public void HideMenu()
        {
            panels[0].SetActive(false);
        }
    }
}