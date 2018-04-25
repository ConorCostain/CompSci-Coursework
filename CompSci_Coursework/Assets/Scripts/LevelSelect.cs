using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSelect : MonoBehaviour
{

	public Vector2 startPosistion;
	public float horizontalSpacing;
	public float verticalSpacing;
	public GameObject levelButton;
	

	//private Vector2 currentPosistion;

	private void Awake()
	{
		Vector2 currentPosistion;
		int amountOfScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
		List<string> levels = new List<string>();
		string temp;

		for (int i = 0; i < amountOfScenes; i++)
		{
			temp = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
			if (temp.Contains("Level"))
			{
				levels.Add(temp);
			}
		}
		Debug.Log(levels.Count.ToString());

		for (int i = 0; i < levels.Count; i++)
		{
			currentPosistion = startPosistion + Vector2.right * horizontalSpacing * i;

			if (currentPosistion.x >= startPosistion.x * -1) // needs fix
			{
				currentPosistion = PosistionCalc(currentPosistion);
			}
			Debug.Log(currentPosistion.x.ToString() + ", " + currentPosistion.y.ToString());
			ButtonSetup(currentPosistion, levelButton, i + 1, levels[i]);
			

		}
	}

	//Recurrence used
	private Vector2 PosistionCalc(Vector2 currentPosistion)
	{
		currentPosistion = Vector2.right * (currentPosistion.x + (2 * startPosistion.x)) +
			Vector2.up * (currentPosistion.y + verticalSpacing);
		if (currentPosistion.x >= startPosistion.x * -1)
		{
			return PosistionCalc(currentPosistion);
		}
		return currentPosistion;
	}

	private void ButtonSetup(Vector2 pos, GameObject buttonPrefab, int levelNumber, string sceneName)
	{
		Debug.Log("ButtonSetup Running");
		Vector3 pos3 = Vector3.up * pos.y + Vector3.right * pos.x;
		GameObject button = Instantiate<GameObject>(buttonPrefab, pos3, Quaternion.identity, gameObject.GetComponentInParent<Transform>());
		button.GetComponent<RectTransform>().anchoredPosition = pos;

		FindObjectOfType<TMP_Text>().text = levelNumber.ToString();

		Button buttonScript = button.GetComponent<Button>();

		buttonScript.onClick.AddListener(() => ButtonClick(sceneName));
	}

	public void ButtonClick(string sceneName)
	{
		PlaySessionManager.ins.LoadScene(sceneName);
	}



}