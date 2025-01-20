using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

/*                                  !!!Warning!!!
 * This code was written by the !greatest programmer and game developer Sayed Anowar (https://sayedanowar.xyz)
 * Changing or modifying any code may break the game or kill the bird.
 * Best of luck debugging my code.
*/
public class GameManager : MonoBehaviour
{
    public GameObject cube;
    public GameObject leftCube;
    public GameObject bird;
    public GameObject bg;
    public GameObject pipes;
    public GameObject mainCanvas;
    public GameObject mainMenu;
    public GameObject mainGame;
    public GameObject gameWorld;
    public GameObject gameUI;
    public GameObject controlInstruction;
    public TMP_Text scoreText;
    public GameObject preGame;
    public GameObject mainCamera;
    public GameObject GOWTextBG;
    public AudioSource BGMaudioSource;
    public AudioSource SFXaudioSource;
    public TMP_Text gameOverWinText;
    public TMP_Text rightIndicator;
    public TMP_Text wrongIndicator;
    public TMP_Text preGameCountDownText;
    public TMP_InputField nameIF;
    public Image gwImg;
    public Image goImg;
    public Slider speedSlider;
    public Text speedSliderText;
    public AudioClip[] audioClips;
    public Image preGameBG;
    //public Image preGameTextImage;
    public float bottomBuffer = 0.1f;
    public float birdSpeed = 50f;
    public float preGameSpeed = 10f;
    public float speedDif = 0.75f;
    public float rotateSpeed = 500f;
    public float testTime = 30f;
    public float preGameTime = 5f;
    public float instructionTime = 3f;
    public float ceillingHeight = -108f;
    public float gameOverPanelTime = 5f;
    public float birdY = 0f;
    public float wheelSensitivity = 10f;
    public float leapSens = 30000f;
    public Optimization opt;
    public LBManager LBManager;
    public LeapPointerController LPCon;
    public GameObject LPointer;

    private bool birdHitObstacle = false;
    private bool isGameRunning = false;
    private bool isPreGameON = false;
    private bool isMenuOn = true;
    private int score = 0;
    private int countDown = 5;
    private int playerPos = -2;
    private Vector3 tempVector;

    void Start()
    {        
        DOTween.Init();
        mainGame.SetActive(false);
        gameUI.SetActive(false);
        mainMenu.SetActive(true);
        isGameRunning = false;
        isMenuOn = true;
        bird.gameObject.transform.position = new Vector3(0f,-163f,0f);
        tempVector = bird.transform.position;
        opt = opt.GetComponent<Optimization>();
        opt.enabled = false;
        nameIF.ActivateInputField();
        //InvokeRepeating("GameStarter", 1f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isGameRunning)
        {
            LPCon.GetComponent<LeapPointerController>().enabled = false;
            LPointer.SetActive(false);
            bg.transform.Translate(Vector3.left * Time.deltaTime * birdSpeed * speedDif);
            bird.transform.Translate(Vector3.right * Time.deltaTime * birdSpeed);
            CameraXPosition();
            BirdControlWithLeap();        
        }
        else if(birdHitObstacle)
        {
            LPCon.GetComponent<LeapPointerController>().enabled = true;
            LPointer.SetActive(true);
            bird.GetComponent<Rigidbody2D>().AddForce(transform.right * 0.5f, ForceMode2D.Impulse);
            bird.transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
            
        }
        else
        {
            LPointer.SetActive(true);
            LPCon.GetComponent<LeapPointerController>().enabled = true;
        }
        if(isPreGameON)
        {
            preGameBG.transform.Translate(Vector3.left * Time.deltaTime * preGameSpeed);
            //preGameTextImage.transform.Translate(Vector3.left * Time.deltaTime * preGameSpeed);
        }

        if (!isMenuOn && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("FlappyBird");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void BirdControlWithLeap()
    {
        if (cube.activeInHierarchy)
        {
            float jot = (cube.transform.position.y * leapSens) + bottomBuffer + gameWorld.transform.position.y;
            if (isGameRunning && jot <= ceillingHeight)
            {
                birdY = jot;
                tempVector = bird.transform.position;
            }
            else if (isGameRunning && jot > ceillingHeight)
            {
                birdY = ceillingHeight;
            }
            bird.transform.position = new Vector3(bird.transform.position.x, birdY, bird.transform.position.z);
        }
        else if (leftCube.activeInHierarchy)
        {
            float jot = (leftCube.transform.position.y * leapSens) + bottomBuffer + gameWorld.transform.position.y;
            if (isGameRunning && jot <= ceillingHeight)
            {
                birdY = jot;
                tempVector = bird.transform.position;
            }
            else if (isGameRunning && jot > ceillingHeight)
            {
                birdY = ceillingHeight;
            }
            bird.transform.position = new Vector3(bird.transform.position.x, birdY, bird.transform.position.z);
        }
        else
        {
            BirdControlWithMouseWheel();
        }
    }
    public void BirdControlWithMouseWheel()
    {
        
        if (isGameRunning)
        {
            if (birdY != tempVector.y)
            {
                birdY = tempVector.y;                
            }
            if (isGameRunning && birdY <= ceillingHeight && Input.GetAxis("Mouse ScrollWheel") != 0.0f)
            {
                birdY = birdY + Input.GetAxis("Mouse ScrollWheel") * wheelSensitivity;
                tempVector = bird.transform.position;
            }
            else if (isGameRunning && birdY > ceillingHeight && Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            {
                birdY = ceillingHeight;
            }
            //Debug.Log(birdY);
            bird.transform.position = new Vector3(bird.transform.position.x, birdY, bird.transform.position.z);
            tempVector = bird.transform.position;
        }
    }

    public void BirdHitObstacle()
    {
        LBManager.SetEntry(nameIF.text, score);
        StartCoroutine(ShowPosition());
        isGameRunning = false;
        birdHitObstacle = true;
        mainCanvas.SetActive(true);
        opt.enabled = false;
        pipes.SetActive(false);
        gameUI.SetActive(false);
        GOWTextBG.SetActive(true);
        gameOverWinText.text = "Your score: " + score.ToString() + "\n "; 
        goImg.gameObject.SetActive(true);
        BGMaudioSource.Stop();
        SFXaudioSource.clip = audioClips[1];
        SFXaudioSource.Play();        
        scoreText.gameObject.SetActive(false);
        StartCoroutine(GameOverOrWin());
    }
    public void BirdHitGameWinObs()
    {
        LBManager.SetEntry(nameIF.text, score);
        StartCoroutine(ShowPosition());
        isGameRunning = false;
        opt.enabled = false;
        pipes.SetActive(false);        
        gwImg.gameObject.SetActive(true);
        gameUI.SetActive(false);
        score += 20;
        rightIndicator.GetComponent<TMP_Text>().text = "+20";
        Sequence rightSequence = DOTween.Sequence();
        rightSequence.Append(rightIndicator.DOFade(1f, 0.3f));
        rightSequence.Append(rightIndicator.DOFade(0f, 0.3f));
        GOWTextBG.SetActive(true);
        gameOverWinText.text = "Your score: " + score.ToString() + "\n ";
        BGMaudioSource.clip = audioClips[1];
        BGMaudioSource.Play();
        StartCoroutine(GameOverOrWin());
    }
    public void BirdHitOption(bool isRight)
    {
        if(isRight)
        {
            SFXaudioSource.clip = audioClips[1];
            SFXaudioSource.Play();
            score += 5;
            Sequence rightSequence = DOTween.Sequence();
            rightSequence.Append(rightIndicator.DOFade(1f, 0.3f));
            rightSequence.Append(rightIndicator.DOFade(0f, 0.3f));
        }
        else
        {
            SFXaudioSource.clip = audioClips[1];
            SFXaudioSource.Play();
            score += 2;
            Sequence wrongSequence = DOTween.Sequence();
            wrongSequence.Append(wrongIndicator.DOFade(1f, 0.3f));
            wrongSequence.Append(wrongIndicator.DOFade(0f, 0.3f));
        }
        scoreText.text = score.ToString();
    }

    public void BirdHitSpeedMarker()
    {
        birdSpeed += 10f;
        controlInstruction.GetComponent<TMP_Text>().text = "Speed up++";
        controlInstruction.GetComponent<TMP_Text>().fontSize = 50;
        StartCoroutine(ShowControlInstruction(1f));
    }
    IEnumerator ShowControlInstruction(float t)
    {        
        if(!controlInstruction.activeInHierarchy)
        {
            controlInstruction.SetActive(true);
        }
        controlInstruction.GetComponent<TMP_Text>().DOFade(1f, 0.2f);
        yield return new WaitForSeconds(t);
        controlInstruction.GetComponent<TMP_Text>().DOFade(0f, 0.2f);
        StopCoroutine(ShowControlInstruction(0f));
    }
    IEnumerator GameOverOrWin()
    {
        //bird.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        yield return new WaitForSeconds(gameOverPanelTime);
        //SceneManager.LoadScene("FlappyBird");
        gameUI.SetActive(false);
        mainGame.SetActive(false);
        mainMenu.SetActive(false);
        gameUI.SetActive(false);
        Destroy(mainGame);
        LBManager.GenerateLeaderboard();
    }
    public void StartButton()
    {
        if(nameIF.text == "")
        {
            nameIF.placeholder.GetComponent<TMP_Text>().text = "Enter your name here\nbefore proceeding";
        }
        else
        {
            StartCoroutine(PreGameMenu());
        }
    }
    IEnumerator PreGameMenu()
    {
        //mainCanvas.SetActive(false);
        preGame.SetActive(true);
        isPreGameON = true;
        mainMenu.SetActive(false);
        preGameCountDownText.text = countDown.ToString() + " Second(s)";

        yield return new WaitForSeconds(1f);

        countDown--;
        if(countDown > 0)
        {
            StartCoroutine(PreGameMenu());
        }
        else if (countDown == 0)
        {
            gameUI.SetActive(true);
            StartCoroutine(ShowControlInstruction(instructionTime));
            opt.enabled = true;
            preGame.SetActive(false);
            isPreGameON = false;
            mainGame.SetActive(true);
            BGMaudioSource.gameObject.SetActive(true);
            SFXaudioSource.gameObject.SetActive(true);
            BGMaudioSource.clip = audioClips[0];
            BGMaudioSource.Play();
            isGameRunning = true;
            isMenuOn = false;
            bird.gameObject.SetActive(true);
            bird.transform.position = new Vector3(bird.transform.position.x, birdY, bird.transform.position.z);
            StopCoroutine(PreGameMenu());
        }
        
    }

  
    public void BirdSpeedSlider()
    {
        birdSpeed = speedSlider.value;
        speedSliderText.text = birdSpeed.ToString();
    }    

    public void CameraXPosition()
    {
        mainCamera.transform.position = new Vector3(bird.transform.position.x+60f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        //mainCamera.transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
    public void SetPlayerPos(int pos)
    {
        playerPos = pos;
    }
    IEnumerator ShowPosition()
    {
        yield return new WaitForSeconds(0.1f);
        if (playerPos == -2)
        {
            StartCoroutine(ShowPosition());
        }
        else
        {
            gameOverWinText.text = gameOverWinText.text + "Your position: " + playerPos.ToString();
            if(birdHitObstacle)
            {
                gameOverWinText.text = gameOverWinText.text + "\nTry Again!";
            }
            StopCoroutine(ShowPosition());
        }
    }

}

