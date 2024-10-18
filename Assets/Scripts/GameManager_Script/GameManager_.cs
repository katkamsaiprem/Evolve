using System.Collections;
using UnityEngine;
using TMPro;
using System;//to use events
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_ : MonoBehaviour
{
    public static GameManager_ Instance { get; private set; }

    [SerializeField]
    private TMP_Text _scoreText, _endScoreText,_highScoreText,_highScoreTextForVicPanel,_endScoreTextForVictoryPanel;

    private int score;
    [SerializeField] private int required_Charge;

    [SerializeField]
    private Animator _scoreAnimator;

    [SerializeField]
    private AnimationClip _scoreClip;

    [SerializeField]
    private GameObject _endPanel,victory;

    [SerializeField]
    private Image _soundImage;

    [SerializeField]
    private Sprite _activeSoundSprite, _inactiveSoundSprite;

    [SerializeField] private GameObject scorePrefab, enemiesPrefab;

    public static event Action gameStarted, gameEnded;//unity action,start and end game

    private Levels _levels;


   // public Buttons_Ref _buttonsRef;
   

    private void Awake()
    {
        Instance = this;
    }

    //when game starts ,we call game start action,which say the player to play the ani
    private void Start()
    {
        gameStarted ?.Invoke();//call the event
       AudioManager.Instance.AddButtonSound();
       _levels = new Levels();
        score = 0;
        _scoreText.text = score.ToString();
        _scoreAnimator.Play(_scoreClip.name, -1, 0f);
        
       InvokeRepeating(nameof(SpawnEnemies),0f,1f);//Spawns every sec
        
    
       
    }

    private void SpawnEnemies()
    {
        Instantiate(UnityEngine.Random.Range(0, 1f) > 0.3f ? enemiesPrefab : scorePrefab, Vector3.zero,
            Quaternion.identity);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Constants.DATA.MAIN_MENU_SCENE);
    }
    public void Evolution_2_Menu_Scene()
    {
        SceneManager.LoadScene(Constants.DATA.MENU_SCENE_2);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleSound()
    {
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND)
            : 1) == 1;
        sound = !sound;
        PlayerPrefs.SetInt(Constants.DATA.SETTINGS_SOUND, sound ? 1 : 0);
        _soundImage.sprite = sound ? _activeSoundSprite : _inactiveSoundSprite;
        AudioManager.Instance.ToggleSound();
    }
 
    //when game end we are going call unity game over func,which is unity action
    public void EndGame()
    {
        StartCoroutine(IEndGame());
    }

    private IEnumerator IEndGame()
    {
        gameEnded?.Invoke();//call the event
        yield return new WaitForSeconds(1.6f);
        if (score >= required_Charge)
        {
            victory.SetActive(true);
           
            
         
           
           _endScoreTextForVictoryPanel.text = score.ToString();
            
            int highScore = PlayerPrefs.HasKey(Constants.DATA.HIGH_SCORE)
                ? PlayerPrefs.GetInt(Constants.DATA.HIGH_SCORE)
                : 0;
            if (score > highScore)
            {
                _highScoreTextForVicPanel.text = "NEW BEST";
                highScore = score;
                PlayerPrefs.SetInt(Constants.DATA.HIGH_SCORE, highScore);
            }
            else
            {
                _highScoreTextForVicPanel.text = "BEST " + highScore.ToString();
            }
            
        }
        else
        {
            _endPanel.SetActive(true);
            _endScoreText.text = score.ToString();

            bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND)
                ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND)
                : 1) == 1;
            _soundImage.sprite = sound ? _activeSoundSprite : _inactiveSoundSprite;

            int highScore = PlayerPrefs.HasKey(Constants.DATA.HIGH_SCORE)
                ? PlayerPrefs.GetInt(Constants.DATA.HIGH_SCORE)
                : 0;
            if (score > highScore)
            {
                _highScoreText.text = "NEW BEST";
                highScore = score;
                PlayerPrefs.SetInt(Constants.DATA.HIGH_SCORE, highScore);
            }
            else
            {
                _highScoreText.text = "BEST " + highScore.ToString();
            }
        }

    }

    public void UpdateScore()
    {
        score++;
        _scoreText.text = score.ToString();
        _scoreAnimator.Play(_scoreClip.name, -1, 0f);
       
    }
    
    //if player collects certain amount of charges+++++
    //unlock evolution_2 panel--char charged--required charge,--collected charge--evolution_2 unlocked_pic--main_menu--next
    //next->levels scene..unlock evolution_2 button,and disable evolution_1 button
}
