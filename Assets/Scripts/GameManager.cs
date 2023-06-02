using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState
{
    START,INPUT,GROWING,NONE
}

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private GameObject backgrounds;

    [SerializeField]
    private Vector2 minMaxRange, spawnRange;

    [SerializeField]
    private GameObject pillarPrefab, playerPrefab, stickPrefab, currentCamera, cloudPrefab, cloudImage;

    [SerializeField]
    private Transform rotateTransform, endRotateTransform;

    [SerializeField]
    private GameObject scorePanel, startPanel, endPanel;

    [SerializeField]
    private TMP_Text scoreText, scoreEndText, highScoreText;

    private GameObject currentPillar, nextPillar, currentStick, player;

    [SerializeField] private GameObject tutorial;

    private int score, highScore;

    private float cameraOffsetX, backgroundOffsetX;

    private bool canBuild;

    private GameState currentState;
    
    [SerializeField]
    private LogicScript logicScript;
    
    [SerializeField]
    private GameObject logicManager;

    public int PillerPrefab = 100;

    [SerializeField]
    private float stickIncreaseSpeed, maxStickSize;

    public static GameManager instance;
    public AudioSource audioSource;
    public AudioClip lineSound;
    [SerializeField] private IntSo ScoreSO;
    public AudioSource audioMenu;
    private void Awake()
    {
        // Configuram backgroundul curent in functie de cel selectat de jucator in inventar
        foreach (Transform t in backgrounds.transform) {
            if (t.parent == backgrounds.transform) {
                if (t.gameObject.name == PlayerPrefs.GetString("currentBackground"))
                    t.gameObject.SetActive(true);
                else
                {
                    t.gameObject.SetActive(false);
                }
            }
        }

        configureObjectsTags();

        audioMenu.mute = true; 
        canBuild = true;

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentState = GameState.START;

        endPanel.SetActive(false);

        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;

        scoreText.text = logicScript.playerScore.ToString();
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();

        CreateStartObjects();
        cameraOffsetX = currentCamera.transform.position.x - player.transform.position.x;

        backgroundOffsetX = backgrounds.transform.position.x;

        GameStart();
    }
    
    private void configureObjectsTags()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        logicManager = GameObject.FindGameObjectWithTag("Logic");

        backgrounds = GameObject.FindWithTag("Backgrounds");

        audioMenu = GameObject.FindGameObjectWithTag("AudioMenu").GetComponent<AudioSource>();
        audioSource = GameObject.FindGameObjectWithTag("AudioBuildingBridge").GetComponent<AudioSource>();

        cloudPrefab = GameObject.FindWithTag("Cloud");
        cloudImage = GameObject.FindWithTag("CloudImage");
        
    }

    private void Update()
    {
        if(currentState == GameState.INPUT)
        {
            if(Input.GetKey("space") && canBuild)
            { 
                tutorial.SetActive(false);
                audioSource.Play();
                currentState = GameState.GROWING;
                ScaleStick();
            }
            if(!canBuild)
            {
                StartCoroutine(JumpOnCloud());
                currentState = GameState.NONE;
            }
        }

        if(currentState == GameState.GROWING)
        {
            if(Input.GetKey("space"))
            {
                ScaleStick();
            }
            else
            {
                audioSource.Stop();
                StartCoroutine(FallStick());
                currentState = GameState.NONE;
            }
        }
    }

    void ScaleStick()
    {
        Vector3 tempScale = currentStick.transform.localScale;
        tempScale.y += Time.deltaTime * stickIncreaseSpeed;
        if (tempScale.y > maxStickSize)
            tempScale.y = maxStickSize;
        currentStick.transform.localScale = tempScale;
       
    }

    IEnumerator JumpOnCloud()
    {
        cloudImage.transform.position = new Vector3(player.transform.position.x + 6.65f, player.transform.position.y + 1.1f, player.transform.position.z);

        anim.instance.animator.SetBool("attack", true);
        yield return StartCoroutine(Move(player.transform, new Vector3(player.transform.position.x + 2.3f, player.transform.position.y + 2.3f, player.transform.position.z), 0.25f));

        yield return StartCoroutine(Move(player.transform, new Vector3(player.transform.position.x + 1.7f, player.transform.position.y + 1.2f, player.transform.position.z), 0.3f));

        anim.instance.animator.SetBool("death", true);
        anim.instance.animator.SetBool("attack", false);
        yield return StartCoroutine(Move(player.transform, new Vector3(player.transform.position.x + 1.8f, player.transform.position.y - 1.3f, player.transform.position.z), 0.32f));

        anim.instance.animator.SetBool("death", false);
        yield return null;
        SceneManager.LoadScene(3);
    }

    IEnumerator FallStick()
    {
        yield return Rotate(currentStick.transform, rotateTransform, 0.4f);

        Vector3 movePosition = currentStick.transform.position + new Vector3(currentStick.transform.localScale.y,0,0);
        movePosition.y = player.transform.position.y;

        anim.instance.animator.SetBool("walk", true);
        yield return Move(player.transform,movePosition,0.35f + 0.16f * currentStick.transform.localScale.y);

        var results = Physics2D.RaycastAll(player.transform.position,Vector2.down);
        var result = Physics2D.Raycast(player.transform.position, Vector2.down);
        foreach (var temp in results)
        {
            if(temp.collider.CompareTag("Platform"))
            {
                result = temp;
             
            }
        }
        if(!result || !result.collider.CompareTag("Platform"))
        {
            anim.instance.animator.SetBool("death", true);
            player.GetComponent<Rigidbody2D>().gravityScale = 1f;
            yield return Rotate(currentStick.transform, endRotateTransform, 0.5f);
            

            GameOver();
        }
        else
        {
            UpdateScore();

            movePosition = player.transform.position;
            movePosition.x = nextPillar.transform.position.x + nextPillar.transform.localScale.x * 0.5f - 1.45f;
            yield return Move(player.transform, movePosition, 0.2f);
            anim.instance.animator.SetBool("walk", false);

            movePosition = currentCamera.transform.position;
            movePosition.x = player.transform.position.x + cameraOffsetX;
            yield return Move(currentCamera.transform, movePosition, 0.5f);


            currentState = GameState.INPUT;
            if (canBuild)
            {
                CreatePlatform();
                SetRandomSize(nextPillar);
                
                Vector3 stickPosition = currentPillar.transform.position;
                stickPosition.x += currentPillar.transform.localScale.x * 0.5f - 0.05f;
                stickPosition.y = currentStick.transform.position.y;
                stickPosition.z = currentStick.transform.position.z;
                currentStick = Instantiate(stickPrefab, stickPosition, Quaternion.identity);
            }
        }
    }


    void CreateStartObjects()
    {
        CreatePlatform(false);

        Vector3 playerPos = playerPrefab.transform.position;
        playerPos.x = playerPos.x + (currentPillar.transform.localScale.x * 0.5f - 0.35f);
        playerPos.y = playerPos.y + 0.07f;
        player = Instantiate(playerPrefab,playerPos,Quaternion.identity);
        player.name = "Player";


        Vector3 stickPos = stickPrefab.transform.position;
        stickPos.x += (currentPillar.transform.localScale.x*0.5f - 0.05f);
        currentStick = Instantiate(stickPrefab, stickPos, Quaternion.identity);
    }

    void CreatePlatform(bool spawnCloud = true)
    {
        var currentPlatform = Instantiate(pillarPrefab);
        currentPillar = nextPillar == null ? currentPlatform : nextPillar;
        nextPillar = currentPlatform;
        currentPlatform.transform.position = pillarPrefab.transform.position + startPos;
        Vector3 tempDistance = new Vector3(Random.Range(spawnRange.x,spawnRange.y) + currentPillar.transform.localScale.x*0.5f,0,0);
        startPos += tempDistance;

        if(Random.Range(0,10) == 0 && spawnCloud)
        {
            var tempCloud = Instantiate(cloudPrefab);
            Vector3 tempPos = currentPlatform.transform.position;
            tempPos.y = cloudPrefab.transform.position.y;
            tempCloud.transform.position = tempPos;
        }
    }
   
    void SetRandomSize(GameObject pillar)
    {
        var newScale = pillar.transform.localScale;
        var allowedScale = nextPillar.transform.position.x - currentPillar.transform.position.x
            - currentPillar.transform.localScale.x * 0.5f - 0.4f;
        newScale.x = Mathf.Max(minMaxRange.x,Random.Range(minMaxRange.x,Mathf.Min(allowedScale,minMaxRange.y)));
        pillar.transform.localScale = newScale;
    }

    void UpdateScore()
    {
        logicScript.addScore();
    }

    void GameOver()
    {
        ScoreSO.Value = 0;
        endPanel.SetActive(true);

        if(logicScript.playerScore > highScore)
        {
            highScore = logicScript.playerScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        scoreEndText.text = "Score: " + logicScript.playerScore.ToString();
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
    }

    public void CloudAccessed()
    {
        canBuild = false;
    }


    public void GameStart()
    {
        CreatePlatform();
        SetRandomSize(nextPillar);
        currentState = GameState.INPUT;
        
    }

    public void GameRestart()
    {
        ScoreSO.Value = 0;
        StateManager.instance.hasSceneStarted = false;
        logicScript.playerScore = 0;
        //Destroy(logicManager);
        SceneManager.LoadScene(2);
    }

    //Helper Functions
    IEnumerator Move(Transform currentTransform,Vector3 target,float time)
    {
        var passed = 0f;
        var init = currentTransform.transform.position;
        while(passed < time)
        {
            passed += Time.deltaTime;
            var normalized = passed / time;
            var current = Vector3.Lerp(init, target, normalized);
            currentTransform.position = current;


            backgrounds.transform.position = new Vector3(currentCamera.transform.position.x + backgroundOffsetX - 2, backgrounds.transform.position.y, backgrounds.transform.position.z);
            yield return null;
        }
    }

    IEnumerator Rotate(Transform currentTransform, Transform target, float time)
    {
        var passed = 0f;
        var init = currentTransform.transform.rotation;
        while (passed < time)
        {
            passed += Time.deltaTime;
            var normalized = passed / time;
            var current = Quaternion.Slerp(init, target.rotation, normalized);
            currentTransform.rotation = current;
            yield return null;
        }
    }
}
