using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public Canvas quitMenu; 
	public Button startText; 
	public Button exitText; 
	public GameObject gameController; 

	// Use this for initialization
	void Start () {

		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> (); 
		exitText = exitText.GetComponent<Button> (); 
		quitMenu.enabled = false; 
	
	}

	public void exitPress()
	{

		quitMenu.enabled = true; 
		startText.enabled = false; 
		exitText.enabled = false; 

	}

	public void NoPress() 

	{

		quitMenu.enabled = false; 
		startText.enabled = true; 
		exitText.enabled = true; 

	}

	public void startLevel()
	{

		if (GameController.instance == null) {
			Instantiate (gameController); 
		}
		Application.LoadLevel (2); 

	}

	public void exitGame()
	{
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
