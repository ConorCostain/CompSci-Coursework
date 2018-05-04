using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class LevelSelect : MonoBehaviour
{

	public Vector2 startPosistion;
	public float horizontalSpacing;
	public float verticalSpacing;
	public GameObject levelButton;
	

	
	//called before level starts
	private void Awake()
	{
		//Gets data required to find the unity scenes
		int amountOfScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
		List<string> levels = new List<string>();
		string temp;

		//Searches through the scenes for levels
		for (int i = 0; i < amountOfScenes; i++)
		{
			temp = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
			if (temp.Contains("Level"))
			{
				levels.Add(temp);
			}
		}
		

		Vector2 currentPosition;
		//Loops through each of the levels
		for (int i = 0; i < levels.Count; i++)
		{
			currentPosition = startPosistion + Vector2.right * horizontalSpacing * i;

			if (currentPosition.x >= startPosistion.x * -1)
			{
				currentPosition = PositionCalc(currentPosition);
			}
			//Sets up the Button
			ButtonSetup(currentPosition, levelButton, i + 1, levels[i]);
			

		}
	}

	//Recursion used!!
	private Vector2 PositionCalc(Vector2 currentPosition)
	{
		//Moves it back by a full length horizontally and down a row
		currentPosition = Vector2.right * (currentPosition.x + (2 * startPosistion.x)) +
			Vector2.up * (currentPosition.y + verticalSpacing);
		//If thats not brought it back to the left enough it calls itself again to go down another line
		if (currentPosition.x >= startPosistion.x * -1)
		{
			return PositionCalc(currentPosition);
		}
		return currentPosition;
	}

	//Instantiates and sets up the values of the Button
	private void ButtonSetup(Vector2 pos, GameObject buttonPrefab, int levelNumber, string sceneName)
	{
		Vector3 pos3 = Vector3.up * pos.y + Vector3.right * pos.x;
		GameObject button = Instantiate(buttonPrefab, pos3, Quaternion.identity, gameObject.GetComponentInParent<Transform>());
		button.GetComponent<RectTransform>().anchoredPosition = pos;

		FindObjectsOfType<TMP_Text>().Where(t => t.gameObject.name == "LevelButton").FirstOrDefault().text = levelNumber.ToString();
        FindObjectsOfType<TMP_Text>().Where(t => t.gameObject.name == "HighScore").FirstOrDefault().text = "HS : " + GetHighScore(sceneName);

        Button buttonScript = button.GetComponent<Button>();

		//Adds an event listener
		buttonScript.onClick.AddListener(() => ButtonClick(sceneName));
	}

	public void ButtonClick(string sceneName)
	{
		PlaySessionManager.ins.LoadScene(sceneName);
	}

	//File Reading used
    public string GetHighScore(string levelName)
    {
        string highScore = "N/A";
        
        //checks to prevent trying to read a file that does not exist
        if (System.IO.File.Exists(levelName + ".txt"))
        {
            //use of file reading
            using (StreamReader reader = new StreamReader(levelName + ".txt"))
            {
                highScore = reader.ReadLine();
            }
        }

        return highScore;
    }

}