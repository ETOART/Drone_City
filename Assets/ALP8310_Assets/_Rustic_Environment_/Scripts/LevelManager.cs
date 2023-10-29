using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This should be added in the "Game_Main" scene
public class LevelManager : MonoBehaviour
{
	/// <summary>
	/// Here add all the extra scenes to load them additively.
	/// Eg: Game_Level_01, Game_Level_02, Game_XXX etc
	/// </summary>
    [SerializeField]
	private string[] m_GameScenes;

	[SerializeField]
	private bool m_LoadScenesOnStart;


	public void LoadGameScenes()
	{
		foreach (string scene in m_GameScenes)
			SceneManager.LoadScene (scene, LoadSceneMode.Additive);
	}

	public void UnloadGameScenes()
	{
		foreach (string scene in m_GameScenes)
			SceneManager.UnloadScene(scene);
	}

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		if (m_LoadScenesOnStart)
			LoadGameScenes();
	}
}