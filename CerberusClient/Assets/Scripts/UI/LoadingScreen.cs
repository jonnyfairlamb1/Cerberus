using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {
    public Image loadingScreenBackgroundImage;
    public TMP_Text mapNameText;
    public TMP_Text gamemodeText;

    public void StartLoadingScreen(string mapName, string gamemodeName, string modeAbbreviation) {
        loadingScreenBackgroundImage = Resources.Load<Image>(mapName);
        mapNameText.text = mapName;
        gamemodeText.text = gamemodeName;

        StartCoroutine(LoadLevel(mapName, modeAbbreviation));
    }

    private IEnumerator LoadLevel(string mapName, string modeAbbreviation) {
        AsyncOperation baseLevelLoad = SceneManager.LoadSceneAsync($"{mapName}_BaseLevel", LoadSceneMode.Additive);
        AsyncOperation gamemodeLevelLoad = SceneManager.LoadSceneAsync($"{mapName}_{modeAbbreviation}", LoadSceneMode.Additive);

        while (!baseLevelLoad.isDone) {
            yield return null;
        }
        while (!gamemodeLevelLoad.isDone) {
            yield return null;
        }

        if (baseLevelLoad.isDone && gamemodeLevelLoad.isDone) {
            var baseScene = SceneManager.GetSceneByName($"{mapName}_BaseLevel");
            var gamemodeScene = SceneManager.GetSceneByName($"{mapName}_{modeAbbreviation}");

            //the gamemode scene gets merged into the base scene
            SceneManager.MergeScenes(gamemodeScene, baseScene);

            SceneManager.SetActiveScene(baseScene);

            SceneManager.UnloadSceneAsync("MainMenu");
        }
        yield return null;
    }
}