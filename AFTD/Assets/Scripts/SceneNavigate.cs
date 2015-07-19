using UnityEngine;
using System.Collections;
/**
 * this class is used only for scence naviation
 * mostly used in Menu
 */ 
public class SceneNavigate : MonoBehaviour {
	public void sceneChange(string sceneName)
	{
		Application.LoadLevel (sceneName);
	}

	public void quitGame(){
		Application.Quit ();
	}
}
