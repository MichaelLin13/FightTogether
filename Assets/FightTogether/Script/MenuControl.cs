using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject StartBtn; //需要按下StartBtn進入TypeMenu介面
	//public GameObject QuitBtn;

	public void EnterTypeMenu()
	{
		//SceneManager.LoadScene(1);
		//SceneManager.LoadScene("TypeMenu");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void EnterCharacterSelection()
	{
		SceneManager.LoadScene(2);
	}
		
	public void QuitGame()
	{
		Application.Quit();
	}
}
