using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance { get; private set; }

    [SerializeField]
    private TMP_Text _scoreText, _endScoreText,_highScoreText,_highScoreTextForVicPanel,_endScoreTextForVictoryPanel;

    private int score;
    [SerializeField] private int required_Charge;

    [SerializeField]
    private Animator _scoreAnimator;

    [SerializeField]
    private AnimationClip _scoreClip;

    [SerializeField]
    private GameObject _scorePrefab;

    [SerializeField]
    private float _maxSpawnOffset;

    [SerializeField]
    private Vector3 _startPos;

    [SerializeField]
    private GameObject _endPanel,victory;

    [SerializeField]
    private Image _soundImage;

    [SerializeField]
    private Sprite _activeSoundSprite, _inactiveSoundSprite;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Evolution_2_AudioManager.Instance.AddButtonSound();
        score = 0;
        _scoreText.text = score.ToString();
        _scoreAnimator.Play(_scoreClip.name, -1, 0f);
        UpdateScorePrefab();
    }

    private void UpdateScorePrefab()
    {
        
        float currentRotation = _scorePrefab.transform.rotation.eulerAngles.z;
        currentRotation = Mathf.Abs(currentRotation) < 0.01f ? 180f : 0f;
        Vector3 newRotation = new Vector3(0, 0, currentRotation);
        _scorePrefab.transform.rotation = Quaternion.Euler(newRotation);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Constants.DATA.MAIN_MENU_SCENE);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Evolution_3_Menu_Scene()
    {
        SceneManager.LoadScene(GameManager.Evolution_3_Menu);
    }

    public void ToggleSound()
    {
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND)
            : 1) == 1;
        sound = !sound;
        PlayerPrefs.SetInt(Constants.DATA.SETTINGS_SOUND, sound ? 1 : 0);
        _soundImage.sprite = sound ? _activeSoundSprite : _inactiveSoundSprite;
        Evolution_2_AudioManager.Instance.ToggleSound();
    }

    public void EndGame()
    {
        _endPanel.SetActive(true);
        _endScoreText.text = score.ToString();
        int highScore;
        if (score >= required_Charge)
        {
            victory.SetActive(true);
           
            
            _endScoreTextForVictoryPanel.text = score.ToString();
            
            highScore = PlayerPrefs.HasKey(Constants.DATA.HIGH_SCORE)
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

        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ?
          PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND) : 1) == 1;
        _soundImage.sprite = sound ? _activeSoundSprite : _inactiveSoundSprite;

        highScore = PlayerPrefs.HasKey(Constants.DATA.HIGH_SCORE) ? PlayerPrefs.GetInt(Constants.DATA.HIGH_SCORE) : 0;
        if(score > highScore)
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

    public void UpdateScore()
    {
        score++;
        _scoreText.text = score.ToString();
        _scoreAnimator.Play(_scoreClip.name, -1, 0f);
       
        UpdateScorePrefab();
    }
}
