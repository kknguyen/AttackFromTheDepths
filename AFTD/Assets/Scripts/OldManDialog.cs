using UnityEngine;
using System.Collections;

public class OldManDialog : MonoBehaviour {
	
	public Texture tint;
	public Texture oldManPNG;
	
	//Controls whether or not we pause game and show dialog or not.
	private bool showDialog;
	
	// Used to determine if the player is in range to start dialog with the old man.
	// Will only show dialog once and you must go out of range and then back in
	// to reinstantiate dialog.
	private bool inRange;
	
	// True if the first dialog message has been played before.
	private bool repeatDialog = false;
	
	// Used to control the scroll speed of the dialog text. **Not working yet.
	private float scrollSpeed = 1f;
	
	// True starts the typewriter display. False stops it so we don't go
	// past the array length for "message".
	private bool typeWriterGo = false;
	
	// The first message that the old man says. This will get placed in typeWriterMessage
	// letter by letter to be displayed.
	public string firstMessage; 
	
	// The repeating message that the old man says anytime after the first message 
	// dialog has been played before.This will get placed in typeWriterMessage
	// letter by letter to be displayed.
	public string repeatMessage;
	
	// What gets displayed in the dialog box. Used for typewriter style display.
	private string typeWriterMessage = "";
	
	// Used to keep track of the index/current character of displaying
	// the message in a typewriter style.
	private int currentChar = 0;
	
	private GUIStyle style;
	
	// Use this for initialization
	void Start () 
	{
		
		firstMessage = "Y-you saved me....thank you young one. I've found out that there is a knight that holds the key to the king. He hides behind an illusion of a castle wall....";
		repeatMessage = "There is a knight that holds the key to the king. He hides behind an illusion of a castle wall....";
		// Start a new GUIStyle so we can change the font size and color
		// of the font in the dialog box.
		style = new GUIStyle ();
		style.fontSize = 24;
		style.normal.textColor = Color.white;
		style.wordWrap = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// If typeWriterGo is true, that means that we are in
		// dialog so we display text in a typewriter style fashion
		// until we reach the end of the message.
		if (typeWriterGo && !repeatDialog && currentChar < firstMessage.Length)
		{
			StartCoroutine (TypeFirstText ());
		}
		// A different dialog for after the first dialog has been played. This is
		// to make dialog a little more realistic.
		else if (typeWriterGo && repeatDialog && currentChar < repeatMessage.Length)
		{
			StartCoroutine (TypeRepeatText ());
		}
		// If we end typeWriterGo early by unpausing or we reached
		// the end of the message to be displayed, we make typeWriterGo
		// false.
		else
		{
			typeWriterGo = false;
		}
		
		// What activates the dialog is the player
		// being in range of the old man. We also us
		if (inRange)
		{
			showDialog = ToggleShowDialog();
			inRange = false;
		}
		
	}
	
	// What makes our text look like typewrite style. First dialog.
	IEnumerator TypeFirstText()
	{
		typeWriterMessage += firstMessage [currentChar];
		++currentChar;
		// WaitForSeconds is affected by FPS and not TimeScale.
		// This makes it so we can do typewriter text even with
		// the game "paused".
		yield return new WaitForSeconds (1/scrollSpeed);
	}
	
	// What makes our text look like typewrite style. Second and beyond dialog.
	IEnumerator TypeRepeatText()
	{
		typeWriterMessage += repeatMessage [currentChar];
		++currentChar;
		// WaitForSeconds is affected by FPS and not TimeScale.
		// This makes it so we can do typewriter text even with
		// the game "paused".
		yield return new WaitForSeconds (1/scrollSpeed);
	}
	
	//The outer box collider keeps track if the player is
	// in range to start dialog.
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			inRange = true;
		}
		
	}
	
	//The outer box collider keeps track if the player is
	// out of range so that we can't start dialog.
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			inRange = false;
		}
		
	}
	
	void OnGUI()
	{
		if (showDialog) 
		{
			GUI.BeginGroup(new Rect(0,Screen.height/2+150, Screen.width, 300));
			
			GUI.DrawTexture(new Rect(0, 0, Screen.width, 100), tint);
			GUI.DrawTexture(new Rect(0, 0, 100, 100), oldManPNG);
			GUI.TextArea(new Rect(100,0,Screen.width-100,25), typeWriterMessage, style);
			if(GUI.Button(new Rect(Screen.width-200,50, 100, 30),"Ok..."))
			{
				showDialog = ToggleShowDialog();    
			}  
			GUI.EndGroup();
		}
	}
	
	bool ToggleShowDialog()
	{
		// Unpausing/resuming the game. We also set repeatDialog to true 
		// so that the old man will display a different dialog.
		if(Time.timeScale == 0)
		{   
			Time.timeScale = 1;
			repeatDialog = true;
			return false;
		}
		// When we pause the game, we are in dialog so the typeWriterGo 
		// bool goes true to start the typewriter style display of the dialog.
		// We make sure to erase the typeWriterMessage for if they toggle
		// for dialog again and reset the position of the currentChar index.
		// We also set repeatDialog to true so that the old man will display
		// a different dialog.
		else
		{
			typeWriterMessage = "";
			currentChar = 0;
			Time.timeScale = 0;
			typeWriterGo = true;
			return true;
		}
	}
}