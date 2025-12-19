using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text quoteSuccess;
    public Text quoteHappiness;
    public Text quoteFreedom;
    public Text gameOverQuote;
    public Text keysCounter;
    public static bool sceneLoaded = false;
    public static bool isPlayerDead = false;
    public static bool isLevelLose = false;
    public static bool isLevelWin = false;
    public static int keys = 0;
    public int keysToWin = 10;
    private static int chIndex = 1;
    private int randQuoteIndex = 0;
    public static bool slowPlayer = false;
    private static bool slowingPlayer = false;
    private bool isChangingColor = false;
    public static int enemies = 0;
    readonly private static string[] arrHaikuLine1 = { "Stillness of one’s mind", "A lone pebble alters fate", "Rising from the depths", "Brilliant it blossoms", "Peer through the mist and we’ll be" };
    readonly private static string[] arrHaikuLine2 = { "Shattered, but alive", "Worries strangle growth", "Unafraid, defenders stand", "Flourishing and free", "Feel the earth below" };
    readonly private static string[] arrHaikuLine3 = { "A cool bed beneath the stars", "A sturdy defense", "A hidden respite", "Deep breaths released to the wind", "A new beginning" };
    readonly private static string[] arrGameOverQuote = { "It is better to fail in originality than to succeed in imitation", "Aspire to inspire, before we expire",
                                                          "Shallow men believe in luck. Strong men believe in cause and effect", "The road to success or failure is almost exactly the same", "Failure is delay, not defeat" };
    public AudioClip[] arrBackgroundMusic;
    private Color[] arrColors = { Color.white, Color.cyan, Color.magenta };


    public Canvas beatLevelCanvas;
    public Canvas mainCanvas;
    public Canvas gameOverCanvas;
    public AudioSource mainAudioSource;
    public AudioClip beatLevelSFX;
    private void normalisePlayer()
    {
        PlayerController.moveSpeed = 5f;
        slowPlayer = false;
        slowingPlayer = false;
    }

    private void slowDownPlayer()
    {
        PlayerController.moveSpeed = 3f;

        Invoke(nameof(normalisePlayer), 2);
    }

    private void initiateBeatLevelAnimation()
    {
        playBeatLevelAnimation(chIndex);
    }
    private void playBeatLevelAnimation(int childIndex)
    {
        if(chIndex < 6)
        {
            beatLevelCanvas.transform.GetChild(childIndex).GetComponent<Animation>().Play();
            chIndex++;
            Invoke(nameof(initiateBeatLevelAnimation), 2);
        }
        else if( chIndex >= 6 && chIndex < 7)
        {
            beatLevelCanvas.transform.GetChild(childIndex).GetComponent<Animation>().Play();
            beatLevelCanvas.transform.GetChild(childIndex).GetChild(0).GetComponent<Animation>().Play();
            chIndex++;
            Invoke(nameof(initiateBeatLevelAnimation), 2);
        }

    }

    private void changeColor()
    {
        int i = Random.Range(0, 3);
        mainAudioSource.gameObject.GetComponent<Camera>().backgroundColor = arrColors[i];
        isChangingColor = false;
    }

    private void initiateGameOverAnimation()
    {
        playGameOverAnimation(chIndex);
    }
    private void playGameOverAnimation(int childIndex)
    {
        if (chIndex < 4)
        {
            gameOverCanvas.transform.GetChild(childIndex).GetComponent<Animation>().Play();
            chIndex++;
            Invoke(nameof(initiateGameOverAnimation), 2);
        }
        else if (chIndex >= 4 && chIndex < 5)
        {
            gameOverCanvas.transform.GetChild(childIndex).GetComponent<Animation>().Play();
            gameOverCanvas.transform.GetChild(childIndex).GetChild(0).GetComponent<Animation>().Play();
            chIndex++;
            Invoke(nameof(initiateGameOverAnimation), 2);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        int i = Random.Range(0, 3);
        mainAudioSource.clip = arrBackgroundMusic[i];
        mainAudioSource.Play();
        if (sceneLoaded == true)
        {
            sceneLoaded = false;
            isPlayerDead = false;
            isLevelLose = false;
            isLevelWin = false;
            keys = 0;
            chIndex = 1;
            slowPlayer = false;
            enemies = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isChangingColor == false)
        {
            isChangingColor = true;
            Invoke(nameof(changeColor), 1);
        }
        keysCounter.text = keys.ToString();
        if(keys == keysToWin && isLevelWin != true)
        {
            mainAudioSource.time = 0.0f;
            mainAudioSource.clip = beatLevelSFX;
            mainAudioSource.Play();
            isLevelWin = true;
            mainCanvas.gameObject.SetActive(false);
            beatLevelCanvas.gameObject.SetActive(true);
            randQuoteIndex = Random.Range(0, 5);
            quoteSuccess.text = arrHaikuLine1[randQuoteIndex];
            randQuoteIndex = Random.Range(0, 5);
            quoteHappiness.text = arrHaikuLine2[randQuoteIndex];
            randQuoteIndex = Random.Range(0, 5);
            quoteFreedom.text = arrHaikuLine3[randQuoteIndex];
            Invoke(nameof(initiateBeatLevelAnimation), 2);
            Cursor.visible = true;
        }
        else if(isPlayerDead == true && isLevelLose != true)
        {
            mainAudioSource.pitch = 0.5f;
            isLevelLose = true;
            mainCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(true);
            randQuoteIndex = Random.Range(0, 5);
            gameOverQuote.text = arrGameOverQuote[randQuoteIndex];
            Invoke(nameof(initiateGameOverAnimation), 2);
            Cursor.visible = true;
        }

        if (slowPlayer == true && slowingPlayer == false)
        {
            slowingPlayer = true;
            slowDownPlayer();
        }
        Debug.Log(PlayerController.moveSpeed);
    }
}
