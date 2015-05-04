using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	private bool isFirstMenu = true;
	private bool isGameMenu = false;
	private bool isOptionsMenu = false;

	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI ()
	{
		FirstMenu();
		LoadGameMenu();
		OptionsMenu();
	}

	void FirstMenu ()
	{
		if (isFirstMenu)
		{
			// Change text to content when assets are created
			if (GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-100, 200, 50), "Play"))
			{
				Application.LoadLevel ("LevelOneTest");
			}

			// Change text to content when assets are created
			if (GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-25, 200, 50), "Options"))
			{

			}

			// Change text to content when assets are created
			if (GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+50, 200, 50), "Quit"))
			{
				Application.Quit ();
			}
		}
	}

	void LoadGameMenu()
	{

	}

	void OptionsMenu()
	{

	}
}
