using System;
using UnityEngine;

public class GameManager
{
    public event Action spawnScoreFlagWhenBallTouchedGroundEvent;

    public RubberController rubberController;
    public BallsController ballController;
    private SaveData _saveData;
    public LoadData loadData;
    private GameObject _groupStingShot;
    private Transform _launchDirection, _rotationPoint;
    private GameObject _parentOfDotsTrajectory, _centerOfStingShot;
    private Collider2D _groundCollider;
    private Camera _mainCam;
    private Vector2 _xPos; // Possition of ball by X axis
    private Vector3 _addForceDirection;
    private GameObject _pointPref;
    private Transform[] _points;
    private Vector3 _startRotationAngle;
    private float _pullBackVariable;

    private int _numOfPoints = 5;

    // Variables for calculating launchDirection
    private float _launchDirX;
    private float _launchDirY;
    private float _posX;
    private float _posY;

    public const float angleMin = 0f, angleMax = 180f;
    public const float pullBackMin = 0f, pullBackMax = 30f;
    public float currentPullBack, currentLaunchAngle;
    public bool isBallLaunched, isTimerStart;

    private float _timer = 1f;

    public GameManager()
    {
        _pointPref = Resources.Load<GameObject>("dot");

        _groupStingShot = GameObject.Find("Game/StingShot").gameObject;
        _rotationPoint = _groupStingShot.transform.Find("RottationPoint").GetComponent<Transform>();

        _launchDirection = _rotationPoint.transform.Find("LaunchDirection").GetComponent<Transform>();
        _parentOfDotsTrajectory = _groupStingShot.transform.Find("ParentOfDotsTrajectory").gameObject;
        _centerOfStingShot = _groupStingShot.transform.Find("CenterOfStingShot").gameObject;
        _groundCollider = GameObject.Find("Game/GroundTrigger").GetComponent<Collider2D>();
        _mainCam = Camera.main;
    }

    public void Start()
    {
        rubberController = new RubberController(_groupStingShot);
        ballController = new BallsController();
        _saveData = new SaveData();
        loadData = new LoadData();

        ballController.Start(_rotationPoint);
        rubberController.Start();
        rubberController.LineRendererUpdate(ballController.ball);
        _startRotationAngle = _rotationPoint.transform.position;
        isTimerStart = false;
        _groundCollider.enabled = false;
        ballController.ball.isKinematic = true;
        ballController.ballRotationSpeed = 0;
        isBallLaunched = false;
        _launchDirX = _launchDirection.position.x;
        _launchDirY = _launchDirection.position.y;

        _points = new Transform[_numOfPoints];
        for(int i = 0; i < _numOfPoints; i++)
        {
            _points[i] = UnityEngine.Object.Instantiate(_pointPref, _centerOfStingShot.transform.position, Quaternion.identity, _parentOfDotsTrajectory.transform).transform;
        }
    }

    public void Update()
    {
        if(isBallLaunched)
        {
            _mainCam.transform.position = new Vector2(ballController.ball.transform.position.x, 0);
            ballController.ball.transform.Rotate(0, 0, ballController.ballRotationSpeed);
        }
        if(isTimerStart)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                _timer = 1f;
                _groundCollider.enabled = true;
                ballController.ball.isKinematic = false;
                rubberController.HideRubber();
                ballController.SetRottSpeed(_pullBackVariable);
                ballController.ball.AddForce(_addForceDirection * ballController.ballAddForce);
                isTimerStart = false;
            }
        }
    }

    public void RestartGame()
    {
        SaveDataOfSimulation();

        isBallLaunched = false;
        _groundCollider.enabled = false;
        ballController.ballAddForce = 0;
        _rotationPoint.transform.eulerAngles = new Vector3(0, 0, 0);
        ballController.ballRotationSpeed = 0;
        _mainCam.transform.position = new Vector2(0, 0);
        ballController.ball.transform.position = new Vector3(_rotationPoint.transform.position.x, _rotationPoint.transform.position.y, _rotationPoint.transform.position.z);
        ballController.ball.transform.eulerAngles = new Vector3(0, 0, 0);
        rubberController.LineRendererUpdate(ballController.ball);
        rubberController.ShowRubber();
        currentPullBack = 0;
    }

    public void SaveDataOfSimulation()
    {
        _saveData.SaveDataSimulation(Main.Instance.uiManager.currentScore, Main.Instance.uiManager.currentScoreMeters, Main.Instance.uiManager.currentScoreCentimeters, currentPullBack, currentLaunchAngle, Main.Instance.gameManager.ballController.ballModel.ballTypeEnum, Main.Instance.gameManager.rubberController.rubberModel.rubberTypeEnum);

        _saveData.SaveDataForLoading(Main.Instance.uiManager.maximumScore, Main.Instance.uiManager.saveScoreMeters, Main.Instance.uiManager.saveScoreCentimeters);
    }

    public void LaunchBall()
    {
        isBallLaunched = true;
        isTimerStart = true;
    }

    private void SetForceAndDirection()
    {
        rubberController.LineRendererUpdate(ballController.ball);
        _posX = ballController.ball.transform.position.x;
        _posY = ballController.ball.transform.position.y;
        _addForceDirection = new Vector3(_launchDirX - _posX, _launchDirY - _posY);
        ballController.ballAddForce = ballController.CalculateAddForce(_pullBackVariable);

        UpdateTrajectoryPos();
    }

    private void UpdateTrajectoryPos()
    {
        if(currentLaunchAngle != 0 && currentPullBack != 0)
        {
            Vector3 velocity = GetForceFrom(ballController.ball.transform.position, _rotationPoint.transform.position);
            SetTrajectoryPos(ballController.ball.transform.position, velocity / 3.095f);
        }
    }

    private void SetTrajectoryPos(Vector3 pStartPos, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;

        fTime += 0.1f;
        for(int i = 0; i < _numOfPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPos.x + dx, pStartPos.y + dy, 2);
            _points[i].transform.position = pos;
            _points[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }

    private Vector3 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector3(toPos.x, toPos.y, toPos.z) - new Vector3(fromPos.x, fromPos.y, fromPos.z)) * ballController.ballAddForce;
    }

    public void SetAngle(float temp)
    {
        currentLaunchAngle = Mathf.Clamp(temp, angleMin, angleMax);
        _rotationPoint.transform.position = _startRotationAngle;
        _rotationPoint.transform.eulerAngles = new Vector3(0, 0, currentLaunchAngle * -1 + 90);
        SetForceAndDirection();
    }

    public void SetPullBack(float temp)
    {
        currentPullBack = Mathf.Clamp(temp, pullBackMin, pullBackMax);
        ballController.ball.transform.position = _rotationPoint.transform.position;
        _xPos.x = currentPullBack / 10;
        ballController.ball.transform.Translate(_xPos * -1);
        _pullBackVariable = currentPullBack / 10;
        SetForceAndDirection();
    }

    public void SetRubberType(int item)
    {
        rubberController.RubberTypeChange((Enums.RubberType)item);
        SetForceAndDirection();
    }

    public void SetBallType(int item)
    {
        ballController.ChangeBall(_pullBackVariable, (Enums.BallType)item);
        rubberController.LineRendererUpdate(ballController.ball);
        SetForceAndDirection();
    }

    public int CalcDistance()
    {
        float startPosX = _rotationPoint.transform.position.x;
        float posX = ballController.ball.transform.position.x;
        int distance = -1 * ((int)startPosX - (int)posX);
        return distance;
    }

    public void SpawnScoreFlagWhenBallTouchedGround()
    {
        spawnScoreFlagWhenBallTouchedGroundEvent?.Invoke();
        isBallLaunched = false;
    }
}