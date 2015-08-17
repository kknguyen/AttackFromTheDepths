using UnityEngine;
using System.Collections;

// Additional libraries
// Writing out binary files so that players can't modify them to save player prefs/hp/experience.
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Contain some data and enable to keep it from scene to scene.
// PlayerPrefs for "non-important" data. Shouldn't save game data/important stuff because it is an editable text file.
// "Singleton" design pattern, but not really.
public class GameManager : MonoBehaviour
{
	public static GameManager manager;

	public float health;
	public float experience;


	// Singleton design, only ONE instance of game manager at a time
	void Awake ()
	{
		if (manager == null)
		{
			DontDestroyOnLoad(this.gameObject);
			manager = this;
		}
		else if (manager != this)
		{
			Destroy (this.gameObject);
		}
	}

	// Saves data out to a file. **Will not work with web b/c we can only save local files.**
	public void Save()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		
		// Persistant nested path where Unity will save game data.
		FileStream file = File.Create(Application.persistentDataPath + "/playerData.dat");
		
		// Create a save file with the data you want to save.
		PlayerData data = new PlayerData();
		data.health = health;
		data.experience = experience;
		
		// Serialize the file and then write it, then close it.
		formatter.Serialize (file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);
			PlayerData data = (PlayerData)formatter.Deserialize(file);
			file.Close();

			health = data.health;
			experience = data.experience;
		}
	}

	public void DeleteData()
	{
		if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
		{
			File.Delete(Application.persistentDataPath + "/playerData.dat");
		}
	}
}

// To make this class serializable, [Serializable]
[Serializable]
class PlayerData
{
	//use gets/sets for security...but maybe later.
	public float health;
	public float experience;
}
