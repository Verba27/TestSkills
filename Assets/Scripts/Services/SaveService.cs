using System;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

public class SaveService
{
    private GameData _gameData;
    private readonly IScoreService _scoreService;
    private readonly IDistancePassedService _distancePassedService;

    public SaveService(GameData gameData, IScoreService scoreService, IDistancePassedService distancePassedService)
    {
        _gameData = gameData;
        _scoreService = scoreService;
        _distancePassedService = distancePassedService;
    }

    private string FilePath => Path.Combine(Application.persistentDataPath, "gameData.json");

    public void SaveGameData()
    {
        _gameData.Score = _scoreService.Score.Value;
        _gameData.Distance = _distancePassedService.Distance.Value;
        string json = JsonConvert.SerializeObject(_gameData);
        File.WriteAllText(FilePath, json);
    }

    public void LoadGameData()
    {
        if (File.Exists(FilePath))
        {
            _gameData = new GameData();
            string json = File.ReadAllText(FilePath);
            _gameData = JsonConvert.DeserializeObject<GameData>(json);
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.LogWarning("No save file found!");
        }
    }

    public GameData TryGetGameData()
    {
        try
        {
            LoadGameData();
            return _gameData;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}