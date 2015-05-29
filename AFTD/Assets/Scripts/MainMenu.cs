using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	private bool isFirstMenu = true;
	private bool isGameMenu = false;
	private bool isOptionsMenu = false;
	private bool isSoundLevelsMenu = false;
	private bool isKeyBindingsMenu = false;
	private bool isClearDataMenu = false;
	private bool isDataDeleted = false;
	
	private float masterVolumeValue = 100f;
	private float musicVolumeValue = 100f;
	private float soundEffectsVolumeValue = 100f;

	GUIStyle centered;
	
	void Awake()
	{

	}
	
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}
	
	void OnGUI()
	{
		FirstMenu();
		LoadGameMenu();
		OptionsMenu();
		SoundLevelsMenu();
		KeyBindingsMenu();
		ClearDataMenu();
		DataDeleted();

	}
	
	void FirstMenu()
	{
		if (isFirstMenu)
		{
			// Change text to content when assets are created
			if (GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-100, 200, 50), "Play"))
			{
				Application.LoadLevel("LevelOneTest");
			}
			
			// Change text to content when assets are created
			if (GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-25, 200, 50), "Options"))
			{
				isFirstMenu = false;
				isOptionsMenu = true;
			}
			
			// Change text to content when assets are created
			if (GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+50, 200, 50), "Quit"))
			{
				Application.Quit();
			}
		}
	}
	
	void LoadGameMenu()
	{
		
	}
	
	void OptionsMenu()
	{
		if(isOptionsMenu)
		{
			// Change text to content when assets are created
			if (GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-100, 200, 50), "Sound Levels"))
			{
				isOptionsMenu = false;
				isSoundLevelsMenu = true;
			}
			
			// Change text to content when assets are created
			if (GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-25, 200, 50), "Key Bindings"))
			{
				isOptionsMenu = false;
				isKeyBindingsMenu = true;
			}
			
			if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+50, 200, 50), "Clear Data"))
			{
				isOptionsMenu = false;
				isClearDataMenu = true;
			}

			if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+125, 200, 50), "Back"))
			{
				isFirstMenu = true;
				isOptionsMenu = false;
			}
		}
	}
	
	void SoundLevelsMenu()
	{
		if(isSoundLevelsMenu)
		{
			GUI.Label(new Rect(Screen.width/2-200,Screen.height/2-107, 200, 50), "Master Volume");
			masterVolumeValue = GUI.HorizontalSlider(new Rect(Screen.width/2-100,Screen.height/2-100, 200, 50), masterVolumeValue, 0f, 100f);
			GUI.Label(new Rect(Screen.width/2+105,Screen.height/2-107, 200, 50), ((int)masterVolumeValue).ToString());
			
			GUI.Label(new Rect(Screen.width/2-200,Screen.height/2-57, 200, 50), "Music Volume");
			musicVolumeValue = GUI.HorizontalSlider(new Rect(Screen.width/2-100,Screen.height/2-50, 200, 50), musicVolumeValue, 0f, 100f);
			GUI.Label(new Rect(Screen.width/2+105,Screen.height/2-57, 200, 50), ((int)musicVolumeValue).ToString());
			
			GUI.Label(new Rect(Screen.width/2-250,Screen.height/2-7, 200, 50), "Sound Effects Volume");
			soundEffectsVolumeValue = GUI.HorizontalSlider(new Rect(Screen.width/2-100,Screen.height/2-0, 200, 50), soundEffectsVolumeValue, 0f, 100f);
			GUI.Label(new Rect(Screen.width/2+105,Screen.height/2-7, 200, 50), ((int)soundEffectsVolumeValue).ToString());
			
			if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+50, 200, 50), "Back"))
			{
				isOptionsMenu = true;
				isSoundLevelsMenu = false;
			}
			
		}
	}
	
	void KeyBindingsMenu()
	{
		if(isKeyBindingsMenu)
		{
			if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+50, 200, 50), "Back"))
			{
				isOptionsMenu = true;
				isKeyBindingsMenu = false;
			}
		}
	}

	void ClearDataMenu()
	{
		if (isClearDataMenu)
		{
			if(GUI.Button(new Rect(Screen.width/2-250,Screen.height/2, 200, 50), "Yes"))
			{
				GameManager.manager.DeleteData();
				Debug.Log("deleted data");

				isClearDataMenu = false;
				isDataDeleted = true;

			}

			if(GUI.Button(new Rect(Screen.width/2+50,Screen.height/2, 200, 50), "No"))
			{
				isOptionsMenu = true;
				isClearDataMenu = false;
			}
			GUI.Label(new Rect(Screen.width/2-125,Screen.height/2-50, 250, 50), "Are you sure you want to clear your data?");
		}
	}

	void DataDeleted()
	{
		if (isDataDeleted)
		{
			GUI.Label(new Rect(Screen.width/2-75, Screen.height/2, 150, 50), "User data deleted!");

			if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+50, 200, 50), "Back"))
			{
				isOptionsMenu = true;
				isDataDeleted = false;
			}
		}
	}
}