using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public GameObject PlayerObj;
    Transform startPlayerTransform;

    public static GameManager Instance;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        //SET THE START POS OF PLAYER


    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsych(sceneIndex));

    }

    IEnumerator LoadAsych(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }

        if (operation.isDone)
        {
            loadingScreen.SetActive(false);
            startPlayerTransform = GameObject.FindGameObjectWithTag("playerStartPos").transform;
            PlayerObj.transform.position = startPlayerTransform.position;
            PlayerObj.transform.rotation = startPlayerTransform.rotation;
        }

    }

    //to be called when player passes through a portal
    public void saveGame()
    {
        SaveSystem.saveGame(Player.Instance, InventoryManager.Instance);

    }

    //to be called at the start of the game - aka start button will call and check and according
    //to the things, the level and stats will be loaded
    public void loadGame()
    {
        GameData data = SaveSystem.loadGame();

        Player.Instance.health = data.playerHealth;
        Player.Instance.breath = data.playerBreath;
        Player.Instance.activeSceneIndex = data.levelSceneIndex;

        InventoryManager.Instance.Items = data.Items;
        InventoryManager.Instance.itemCounts = data.itemCounts;


    }


}
