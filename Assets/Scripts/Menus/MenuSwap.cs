using UnityEngine;
using System.Collections;

public class MenuSwap : MonoBehaviour 
{
	public GameObject[] menuList;
	private int currentActivePanelIndex = 0;

	public void showPanel(int panelIndex)
	{
		menuList [currentActivePanelIndex].SetActive (false);  	//Turn off the currentActivePanel     
		menuList[panelIndex].SetActive (true);   				//Turn currentActivePanel on again
		currentActivePanelIndex = panelIndex;					//Set currentActivePanelIndex to panelIndex
	}
}
