using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsController
{
    public Ball ballModel;
    private GameObject _ballObject;

    public Rigidbody2D ball;
    private SpriteRenderer _ballSpriteRenderer;
    public float ballRotationSpeed;
    public float ballAddForce = 0;
    private List<Ball> _balls;
    private Enums.BallType _ballTypeEnum;


    public void Start(Transform rotationPoint)
    {
        _balls = new List<Ball>()
        {
            new Ball(0.0585f, Enums.BallType.TennisBall),
            new Ball(0.41f, Enums.BallType.SoccerBall),
            new Ball(0.141f, Enums.BallType.Baseball)
        };

        _ballObject = Object.Instantiate(Resources.Load<GameObject>("Ball"), rotationPoint.position, Quaternion.identity, rotationPoint.transform);
        ballModel = GetBallDataByType(Enums.BallType.TennisBall);

        _ballSpriteRenderer = _ballObject.GetComponent<SpriteRenderer>();
        ball = _ballObject.GetComponent<Rigidbody2D>();
        ball.GetComponent<BallEvenArgs>().BallIsGroundedEvent += BallTouchedGroundEventHandler;
    }

    private Ball GetBallDataByType(Enums.BallType ballTypeEnum)
    {
        return _balls.Find(x => x.ballTypeEnum == ballTypeEnum);
    }

    public void SetRottSpeed(float pullBackVariable)
    {
        ballRotationSpeed = ballAddForce * pullBackVariable + 10;
    }

    public float CalculateAddForce(float pullBackVariable)
    {
        float velocity = ((ballModel.ballMassa / ball.mass) * Time.fixedDeltaTime) + (pullBackVariable + Main.Instance.gameManager.rubberController.rubberModel.stiffness); // velocity for ball
        velocity = velocity + velocity * (1 / ballModel.ballMassa); // like ForceMode.impulse
        return velocity;
    }

    public void ChangeBall(float pullBackVariable, Enums.BallType ballEnum)
    {
        ballModel = GetBallDataByType(ballEnum);
        _ballSpriteRenderer.sprite = ballModel.ballSprite;
        CalculateAddForce(pullBackVariable);
    }

    public void BallTouchedGroundEventHandler()
    {
        ball.velocity = Vector3.zero;
        ball.isKinematic = true;
        ballRotationSpeed = 0;
        Main.Instance.gameManager.SpawnScoreFlagWhenBallTouchedGround();
    }
}
