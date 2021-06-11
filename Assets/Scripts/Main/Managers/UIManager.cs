using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    private Text _flagText, _maxScoreText, _currScoreText;
    private InputField _pullBack, _launchAngle;
    private Dropdown _ballType, _rubberType;
    private Button _simulateBtn, _restartBtn, _defineLaunchBtn;
    private Canvas _canvas;
    private GameObject _groupUI, _groupScoreFlag;
    private GameManager _gameManager;
    public int currentScore = 0;
    public int maximumScore = 0;
    public int currentScoreCentimeters = 0, currentScoreMeters = 0;
    public int saveScoreCentimeters = 0, saveScoreMeters = 0, saveMaxScore = 0;

    public UIManager()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _groupUI = _canvas.transform.Find("Group_Settings").gameObject;

        _pullBack = _groupUI.transform.Find("Group_PullBack/InputField_PullBack").GetComponent<InputField>();
        _launchAngle = _groupUI.transform.Find("Group_Angle/InputField_Angle").GetComponent<InputField>();
        _ballType = _groupUI.transform.Find("Group_BallType/Dropdown_BallType").GetComponent<Dropdown>();
        _rubberType = _groupUI.transform.Find("Group_RubberBandType/Dropdown_RubberBandType").GetComponent<Dropdown>();

        _maxScoreText = _canvas.transform.Find("Group_Score/Text_MaxScore").GetComponent<Text>();
        _currScoreText = _canvas.transform.Find("Group_Score/Text_CurrentScore").GetComponent<Text>();

        _groupScoreFlag = _canvas.transform.Find("Group_ScoreFlag").gameObject;
        _flagText = _groupScoreFlag.transform.Find("Text_MetersCount").GetComponent<Text>();

        _simulateBtn = _canvas.transform.Find("Button_Simulate").GetComponent<Button>();
        _restartBtn = _canvas.transform.Find("Button_Restart").GetComponent<Button>();
        _defineLaunchBtn = _canvas.transform.Find("Button_DefineLaunch").GetComponent<Button>();

        _restartBtn.onClick.AddListener(RestartGame);
        _simulateBtn.onClick.AddListener(LaunchBall);
        _defineLaunchBtn.onClick.AddListener(DefineLauch);

        SetBallTypeDropDownOptions();
        SetRubberTypeDropDownOptions();

        _pullBack.onValueChanged.AddListener(PullBackChanged);
        _launchAngle.onValueChanged.AddListener(AngleChanged);
        _ballType.onValueChanged.AddListener(BallTypeChanged);
        _rubberType.onValueChanged.AddListener(RubberTypeChanged);

    }

    private void SetBallTypeDropDownOptions()
    {
        _ballType.ClearOptions();
        _ballType.AddOptions(Enum.GetNames(typeof(Enums.BallType)).ToList());
    }

    private void SetRubberTypeDropDownOptions()
    {
        _rubberType.ClearOptions();
        _rubberType.AddOptions(Enum.GetNames(typeof(Enums.RubberType)).ToList());
    }

    public void DefineLauch()
    {
        _groupUI.SetActive(true);
        _defineLaunchBtn.gameObject.SetActive(false);
        _simulateBtn.gameObject.SetActive(true);
    }

    public void LaunchBall()
    {
        if(_pullBack.text != "" && _launchAngle.text != "")
        {
            _simulateBtn.gameObject.SetActive(false);
            _groupUI.SetActive(false);
            _gameManager.LaunchBall();
        }
    }

    public void BallTypeChanged(int item)
    {
        _gameManager.SetBallType(item);
    }

    public void RubberTypeChanged(int item)
    {
        _gameManager.SetRubberType(item);
    }

    public void Start()
    {
        LoadScoreFromJson();
        _groupUI.SetActive(false);
        _simulateBtn.gameObject.SetActive(false);
        _gameManager = Main.Instance.gameManager;
        _gameManager.spawnScoreFlagWhenBallTouchedGroundEvent += PreferencesForSpawnScoreFlag;
    }

    public void LoadScoreFromJson()
    {
        maximumScore = Main.Instance.gameManager.loadData.maxDistanceLoad;
        _maxScoreText.text = "Max score: " + Main.Instance.gameManager.loadData.maxDistanceMetersLoad.ToString() + " m " + Main.Instance.gameManager.loadData.maxDistanceCentimetersLoad.ToString() + " cm";
    }

    private void ScoreUpdate()
    {
        if(_gameManager.isBallLaunched)
        {
            currentScore = _gameManager.CalcDistance();
            if(currentScore < 0) currentScore = 0;
            currentScoreCentimeters = currentScore % 100;
            currentScoreMeters = currentScore / 100;

            _currScoreText.text = "Current score : " + currentScoreMeters.ToString() + " m " + currentScoreCentimeters.ToString() + " cm";

            if(currentScore > maximumScore)
            {
                _maxScoreText.text = "Max score: " + currentScoreMeters.ToString() + " m " + currentScoreCentimeters.ToString() + " cm";
                saveMaxScore = maximumScore;
                saveScoreCentimeters = currentScoreCentimeters;
                saveScoreMeters = currentScoreMeters;
            }
        }
    }

    public void Update()
    {
        ScoreUpdate();
    }

    private void RestartGame()
    {
        _pullBack.text = "";
        _launchAngle.text = "";

        _defineLaunchBtn.gameObject.SetActive(true);
        _simulateBtn.gameObject.SetActive(false);

        _groupScoreFlag.SetActive(false);
        _restartBtn.gameObject.SetActive(false);
        _gameManager.RestartGame();
    }

    private void PreferencesForSpawnScoreFlag()
    {
        maximumScore = currentScore;
        _groupScoreFlag.SetActive(true);
        _restartBtn.gameObject.SetActive(true);
        _flagText.text = _currScoreText.text.Replace("Current score : ", "");
    }

    private void PullBackChanged(string value)
    {
        if(value != "" && float.TryParse(_pullBack.text, out float temp) && temp != _gameManager.currentPullBack)
        {
            _gameManager.SetPullBack(temp);
            if(temp != _gameManager.currentPullBack)
            {
                _pullBack.text = _gameManager.currentPullBack.ToString();
            }
        }
    }

    private void AngleChanged(string value)
    {
        if(value != "" && float.TryParse(_launchAngle.text, out float temp) && temp != _gameManager.currentLaunchAngle)
        {
            _gameManager.SetAngle(temp);
            if(temp != _gameManager.currentLaunchAngle)
            {
                _launchAngle.text = _gameManager.currentLaunchAngle.ToString();
            }
        }
    }
}