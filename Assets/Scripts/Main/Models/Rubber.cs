using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubber
{
    public Material rubberMaterial;
    public float stiffness;
    public Enums.RubberType rubberTypeEnum;

    public Rubber(float rubberStiffness, Enums.RubberType type)
    {
        rubberTypeEnum = type;
        stiffness = rubberStiffness;
        rubberMaterial = Resources.Load("Material/" + rubberTypeEnum) as Material;
    }
}
