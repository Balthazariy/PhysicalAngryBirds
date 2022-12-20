using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data
{
    public int distanceMeters { get; set; }
    public int distanceCentimeters { get; set; }
    public int distanceTravel { get; set; }

    public float pullBack { get; set; }
    public float launchAngle { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Enums.BallType ballType { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Enums.RubberType rubberType { get; set; }
}

[Serializable]
public class SaveDataItems
{
    public int maxDistanceMeters { get; set; }
    public int maxDistanceCentimeters { get; set; }
    public int maxDistance { get; set; }
}

[Serializable]
public class GameDataSerializeHelper
{
    public int maxMeters;
    public int maxCentimeters;
    public int maxDistance;
}

public class SaveData
{
    private string _savePathOne = @"Assets/Resources/Json/save.json";
    private List<string> paths = new List<string>
    {
        @"Assets/Resources/Json/save",
        @"Assets/Resources/Json/load"
    };

    public void SaveDataSimulation(int distance, int travelDistanceMeters, int travelDistanceCentimeters, float pullBackValue, float launchAngleValue, Enums.BallType balltypeEnum, Enums.RubberType rubberTypeEnum)
    {
        var dataToSave = new Data
        {
            distanceTravel = distance,
            distanceMeters = travelDistanceMeters,
            distanceCentimeters = travelDistanceCentimeters,
            pullBack = pullBackValue,
            launchAngle = launchAngleValue,
            ballType = balltypeEnum,
            rubberType = rubberTypeEnum
        };
        Serialize(dataToSave);
    }

    public void SaveDataForLoading(int maxDistanceTravel, int travelDistanceMeters, int travelDistanceCentimeters)
    {
        var dataForLoad = new SaveDataItems
        {
            maxDistance = maxDistanceTravel,
            maxDistanceMeters = travelDistanceMeters,
            maxDistanceCentimeters = travelDistanceCentimeters
        };
        Serialize(dataForLoad);
    }

    public void Serialize(object obj)
    {
        var serializer = new JsonSerializer();
        serializer.Formatting = Formatting.Indented;

        using (var sw = new StreamWriter(_savePathOne))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, obj);
        }
    }
}

public class LoadData
{
    public int maxDistanceLoad;
    public int maxDistanceMetersLoad;
    public int maxDistanceCentimetersLoad;

    public LoadData()
    {
        var json = Resources.Load<TextAsset>($"Json/load").text;
        GameDataSerializeHelper helper = JsonConvert.DeserializeObject<GameDataSerializeHelper>(json);
        maxDistanceLoad = helper.maxDistance;
        maxDistanceMetersLoad = helper.maxMeters;
        maxDistanceCentimetersLoad = helper.maxCentimeters;
    }
}
