using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start, Input, Growing, None, JumpCloud
}

public class GameManager : MonoBehaviour
{

    public AudioSource audioMenu;
    public AudioSource audioSource;
    public static GameManager instance;

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

    private int localScore;

    private int PillerPrefab = 100;

    [SerializeField]
    private float stickIncreaseSpeed, maxStickSize;

    
    
    [SerializeField] 
    private IntSo ScoreSO;
    
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

        // Configuram switch-urile pe care le folosim in joc
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

        // Initializam toate celelalte obiecte necesare jocului
        currentState = GameState.Start;

        endPanel.SetActive(false);

        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;

        scoreText.text = logicScript.playerScore.ToString();
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();

        CreateStartObjects();

        // pentru camera si background calculam offset-ul (va fi folosit de fiecare data cand mutam camera si background-ul)
        cameraOffsetX = currentCamera.transform.position.x - player.transform.position.x;
        backgroundOffsetX = backgrounds.transform.position.x;

        localScore = 0;
        GameStart();
    }
    

    //asociam obiectele din cod cu tag-urile lor respective din inspector
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

    // functia update a jocului apelata odata pe frame
    private void Update()
    {
        if(currentState == GameState.Input)
        {
            // in cazul in care jocul asteapta input-ul jucatorului
            // verificam daca jucatorul apasa space si incepem sa construim stick-ul(podul)
            // si sa setam noul 
            if(Input.GetKey("space") && canBuild)
            { 
                tutorial.SetActive(false);
                audioSource.Play();
                currentState = GameState.Growing;
                ScaleStick();
            }
        }


        // in cazul in care trebuie sa sarim pe nor
        // apelam coroutina care ne va anima pinguinul pentru a sari pe nor
        if (currentState == GameState.JumpCloud)
        {
            StartCoroutine(JumpOnCloud());
            currentState = GameState.None;
        }

        // in cazul in care platforma e in crestere
        // verificam daca platforma trebuie sa creasca in continuare(este apasata tasta space)
        // sau trebuie sa cada(nu mai este apasata tasta space)
        if(currentState == GameState.Growing)
        {
            if(Input.GetKey("space"))
            {
                ScaleStick();
            }
            else
            {
                audioSource.Stop();
                StartCoroutine(FallStick());
                currentState = GameState.None;
            }
        }
    }

    // aici crestem podul (pe verticala)
    void ScaleStick()
    {
        Vector3 tempScale = currentStick.transform.localScale;
        tempScale.y += Time.deltaTime * stickIncreaseSpeed;
        if (tempScale.y > maxStickSize)
            tempScale.y = maxStickSize;
        currentStick.transform.localScale = tempScale;
       
    }


    // intreg-ul "movement" al pinguinului pentru a sari pe nor odata ce a ajuns pe platforma DUPA ce a luat pastila
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


    //functia care este apelata odata ce norul trebuie sa cada
    IEnumerator FallStick()
    {   
        //incepem o alta corutina in care rotim podul
        yield return Rotate(currentStick.transform, rotateTransform, 0.4f);

        //calculam pozitia finala a "player"-ului dupa ce podul a cazut si il mutam acolo
        //adica facem pinguinul sa mearga pe pod
        Vector3 movePosition = currentStick.transform.position + new Vector3(currentStick.transform.localScale.y,0,0);
        movePosition.y = player.transform.position.y;
        anim.instance.animator.SetBool("walk", true);
        yield return Move(player.transform,movePosition,0.35f + 0.16f * currentStick.transform.localScale.y);

        //odata ce "player"-ul a mers pe pod, verificam daca se afla
        //pe o platforma (adica se afla in coliziune cu platforma)
        var results = Physics2D.RaycastAll(player.transform.position,Vector2.down);
        var result = Physics2D.Raycast(player.transform.position, Vector2.down);
        foreach (var temp in results)
        {
            if(temp.collider.CompareTag("Platform"))
            {
                result = temp;
             
            }
        }
        //odata ce nu se afla in coliziune cu platforma, setam animatia de cadere si
        //si gravitatia (pentru ca player-ul sa cada), iar jocul ia sfarsit
        if(!result || !result.collider.CompareTag("Platform"))
        {
            anim.instance.animator.SetBool("death", true);
            player.GetComponent<Rigidbody2D>().gravityScale = 1f;
            yield return Rotate(currentStick.transform, endRotateTransform, 0.5f);
            

            GameOver();
        }
        else
        {
            //in cazul in care a ajuns pe o platforma modificam scorul
            UpdateScore();

            //apoi mutam player-ul la o pozitia fixa pe platforma
            //(asta pentru a putea construi poduri intrucat in cazul in care pinguinul sa afla la marginea dreapta a platformei
            //podul care urma sa se construiesca ar fi fost acoperit de pinguin)
            movePosition = player.transform.position;
            movePosition.x = nextPillar.transform.position.x + nextPillar.transform.localScale.x * 0.5f - 1.45f;
            yield return Move(player.transform, movePosition, 0.2f);
            anim.instance.animator.SetBool("walk", false);


            //mutam camera mai in dreapta
            movePosition = currentCamera.transform.position;
            movePosition.x = player.transform.position.x + cameraOffsetX;
            yield return Move(currentCamera.transform, movePosition, 0.5f);


            //in cazul in care inca se poate construi (nu a fost colectata o pastila)
            //se creaza urmatoarea platforma, iar jocul incepe sa accepte din nou input-ul
            //utilizatorului
            if (canBuild)
            {
                currentState = GameState.Input;
                CreatePlatform();
                SetRandomSize(nextPillar);
                
                Vector3 stickPosition = currentPillar.transform.position;
                stickPosition.x += currentPillar.transform.localScale.x * 0.5f - 0.05f;
                stickPosition.y = currentStick.transform.position.y;
                stickPosition.z = currentStick.transform.position.z;
                currentStick = Instantiate(stickPrefab, stickPosition, Quaternion.identity);
            }
            else
            {
                //in cazul in care a fost colectata pastila
                //jocul isi pregateste state-ul astfel incat sa sara pe nor
                currentState = GameState.JumpCloud;
            }
        }
    }

    //cream obiectele de start:
    //instantiam player-ul si podul si cream platforma initiala
    void CreateStartObjects()
    {
        CreatePlatform();

        Vector3 playerPos = playerPrefab.transform.position;
        playerPos.x = playerPos.x + (currentPillar.transform.localScale.x * 0.5f - 0.35f);
        playerPos.y = playerPos.y + 0.07f;
        player = Instantiate(playerPrefab,playerPos,Quaternion.identity);
        player.name = "Player";


        Vector3 stickPos = stickPrefab.transform.position;
        stickPos.x += (currentPillar.transform.localScale.x*0.5f - 0.05f);
        currentStick = Instantiate(stickPrefab, stickPos, Quaternion.identity);
    }

    void CreatePlatform()
    {
        //cream platforma
        var currentPlatform = Instantiate(pillarPrefab);
        currentPillar = nextPillar == null ? currentPlatform : nextPillar;
        nextPillar = currentPlatform;
        currentPlatform.transform.position = pillarPrefab.transform.position + startPos;
        //setam distanta la care platforma sa fie generata(o distanta random intr-un anumit range)
        Vector3 tempDistance = new Vector3(Random.Range(spawnRange.x,spawnRange.y) + currentPillar.transform.localScale.x*0.5f,0,0);
        startPos += tempDistance;


        //in cazul in care am trecut de 20 de block-uri
        //avem o sansa de 20% sa se genereza pastila care ne
        //va duce in al doilea mod de joc
        if(Random.Range(0,4) == 0 && localScore > 20)
        {
            var tempCloud = Instantiate(cloudPrefab);
            Vector3 tempPos = currentPlatform.transform.position;
            tempPos.y = cloudPrefab.transform.position.y;
            tempCloud.transform.position = tempPos;
        }
    }
   
   //randomizarea dimensiunilor platformei
    void SetRandomSize(GameObject pillar)
    {
        var newScale = pillar.transform.localScale;
        var allowedScale = nextPillar.transform.position.x - currentPillar.transform.position.x
            - currentPillar.transform.localScale.x * 0.5f - 0.4f;
        newScale.x = Mathf.Max(minMaxRange.x,Random.Range(minMaxRange.x,Mathf.Min(allowedScale,minMaxRange.y)));
        pillar.transform.localScale = newScale;
    }

    //actualizarea scorului
    void UpdateScore()
    {
        logicScript.addScore();
        localScore += 1;
    }
    
    //jocul i-a sfarsit, deci afisam meniul de game over impreuna cu scorul
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

    //functia apelata odata ce "player"-ul intra in coliziune cu
    //pastila
    public void CloudAccessed()
    {
        canBuild = false;
    }

    //functia pentru a incepe jocul:
    //este creata prima platforma pe care se spawneaza pinguinul
    //si starea curenta a jocului este setata astfel incat
    //sa accepte input-ul utilizatorului
    public void GameStart()
    {
        CreatePlatform();
        SetRandomSize(nextPillar);
        currentState = GameState.Input;
        
    }

    //variabilele referitoare la scena si la scor sunt resetate
    //pentru ca scena sa fie incarcata din nou
    public void GameRestart()
    {
        ScoreSO.Value = 0;
        StateManager.instance.hasSceneStarted = false;
        logicScript.playerScore = 0;
        //Destroy(logicManager);
        SceneManager.LoadScene(2);
    }

    //corutina cu care mutam transformarea unui obiect la un anumit "target" intr-un anumit "timp"
    IEnumerator Move(Transform currentTransform,Vector3 target,float time)
    {
        //la fiecare frame vedem cat timp a trecut si calculam pozitia la care 
        //trebuie ca transformarea sa se afle la timpul respectiv(intrucat avem variabila "time" care ne indica cat
        //de mult sa dureze miscarea de mutare)
        //aplicam noua pozitie transformarii
        var passed = 0f;
        var init = currentTransform.transform.position;
        while(passed < time)
        {
            passed += Time.deltaTime;
            var normalized = passed / time;
            var current = Vector3.Lerp(init, target, normalized);
            currentTransform.position = current;


            //de asemenea actualizam pozitia "backgrounds-urilor"
            //intrucat cu aceasta functie putem sa mutam si camera
            backgrounds.transform.position = new Vector3(currentCamera.transform.position.x + backgroundOffsetX - 2, backgrounds.transform.position.y, backgrounds.transform.position.z);

            //functia aceasta va fi pusa pe pauza pana la urmatorul frame
            yield return null;
        }
    }

    //corutina cu care rotim podul la un anumit "target" intr-un anumit "timp"
    IEnumerator Rotate(Transform currentTransform, Transform target, float time)
    {
        //la fiecare frame vedem cat timp a trecut si calculam rotatia la care 
        //trebuie ca transformarea sa se afle la timpul respectiv(intrucat avem variabila "time" care ne indica cat
        //de mult sa dureze miscarea de mutare)
        //aplicam noua rotatie transformarii
        var passed = 0f;
        var init = currentTransform.transform.rotation;
        while (passed < time)
        {
            passed += Time.deltaTime;
            var normalized = passed / time;
            var current = Quaternion.Slerp(init, target.rotation, normalized);
            currentTransform.rotation = current;

            //functia aceasta va fi pusa pe pauza pana la urmatorul frame
            yield return null;
        }
    }
}
