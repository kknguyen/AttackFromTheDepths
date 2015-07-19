using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager manager;

	//Game Options
	public int masterVolume;
	public int musicVolume;
	public int soundEffectsVolume;

	//Character Stats
	public float health;
	public float experience;

	// Singleton design, only ONE instance of game manager at a time
	void Awake ()
	{
		if (manager == null) {
			DontDestroyOnLoad (this.gameObject);
			manager = this;
		} else if (manager != this) {
			Destroy (this.gameObject);
		}
	}


	public void masterVolumeChanged(){
		Slider go = GameObject.Find ("master").GetComponent<Slider>();
		if(go != null)
			masterVolume = (int)go.value;
	}
	public void musicVolumeChanged(){
		Slider go = GameObject.Find ("music").GetComponent<Slider>();
		if(go != null)
			musicVolume = (int)go.value;
	}
	public void soundEffectsVolumeChanged(){
		Slider go = GameObject.Find ("soundEffects").GetComponent<Slider> ();
		if (go != null)
			soundEffectsVolume = (int)go.value;
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerData.dat");

		PlayerData data = new PlayerData();
		data.health = health;
		data.experience = experience;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
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

[Serializable]
class PlayerData
{
	public float health;
	public float experience;
}
