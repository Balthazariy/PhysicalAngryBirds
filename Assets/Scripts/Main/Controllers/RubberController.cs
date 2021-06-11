using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RubberController
{
    private LineRenderer _catapultLineFront;
    private LineRenderer _catapultLineBack;
    private Ray _leftCatapultToProjectile;
    public Rubber rubberModel;
    private List<Rubber> _rubbers;


    public RubberController(GameObject groupStingShot)
    {
        _catapultLineFront = groupStingShot.transform.Find("FrontLineRenderer").GetComponent<LineRenderer>();
        _catapultLineBack = groupStingShot.transform.Find("BackLineRenderer").GetComponent<LineRenderer>();

    }

    public void Start()
    {
        _rubbers = new List<Rubber>()
        {
            new Rubber(0.3f, Enums.RubberType.YellowRubber),
            new Rubber(0.5f, Enums.RubberType.BlueRubber),
            new Rubber(1f, Enums.RubberType.RedRubber)
        };

        rubberModel = GetRubberDataByType(Enums.RubberType.YellowRubber);
        _catapultLineFront.material = rubberModel.rubberMaterial;
        _catapultLineBack.material = rubberModel.rubberMaterial;

        LineRendererSetup();
        _leftCatapultToProjectile = new Ray(_catapultLineFront.transform.position, Vector3.zero);
    }

    private Rubber GetRubberDataByType(Enums.RubberType rubberTypeEnum)
    {
        return _rubbers.Find(x => x.rubberTypeEnum == rubberTypeEnum);
    }

    public void LineRendererSetup()
    {
        _catapultLineFront.SetPosition(0, _catapultLineFront.transform.position);
        _catapultLineBack.SetPosition(0, _catapultLineBack.transform.position);

        _catapultLineFront.sortingLayerName = "Foreground";
        _catapultLineBack.sortingLayerName = "Foreground";

        _catapultLineFront.sortingOrder = 4;
        _catapultLineBack.sortingOrder = 1;
    }

    public void LineRendererUpdate(Rigidbody2D ball)
    {
        Vector2 catapultToProjectile = ball.transform.position - _catapultLineFront.transform.position;
        _leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = _leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude);
        _catapultLineFront.SetPosition(1, holdPoint);
        _catapultLineBack.SetPosition(1, holdPoint);
    }

    public void ShowRubber()
    {
        _catapultLineBack.gameObject.SetActive(true);
        _catapultLineFront.gameObject.SetActive(true);
    }

    public void HideRubber()
    {
        _catapultLineBack.gameObject.SetActive(false);
        _catapultLineFront.gameObject.SetActive(false);
    }

    public void RubberTypeChange(Enums.RubberType rubberEnum)
    {
        rubberModel = GetRubberDataByType(rubberEnum);
        _catapultLineFront.material = rubberModel.rubberMaterial;
        _catapultLineBack.material = rubberModel.rubberMaterial;
    }
}