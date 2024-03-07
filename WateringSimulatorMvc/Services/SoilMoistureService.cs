using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WateringSimulatorMvc.Models;

namespace WateringSimulatorMvc.Services;

public class SoilMoistureService
{
    private const string StatusFilePath = "wwwroot/SprinklerStatus.txt";
    private const string LevelFilePath = "wwwroot/SoilMoistureLevel.txt";
    private readonly HttpClient _httpClient;

    public SoilMoistureService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public bool Status
    {
        get => GetSprinklerStatusFromFile();
        set => SetSprinklerStatusToFile(value);
    }

    public double Level
    {
        get => GetSoilMoistureLevelFromFile();
        set => SetSoilMoistureLevelToFile(value);
    }

    public WateringViewModel Get()
    {
        return new WateringViewModel
        {
            HumidityData = Level,
            SprinklerStatus = Status
        };
    }

    public async void TurnOn(bool turnOn)
    {
        Status = turnOn;

        if (turnOn)
        {
            await SoilMoistureLevelOn(DateTime.Now, Level);
        }
        else
        {
            await SoilMoistureLevelOff();
        }
    }

    private async Task SoilMoistureLevelOn(DateTime now, double level)
    {
        while (Status && Level <= 60)
        {
            await Task.Delay(2000); //60000
            Level++;
        }
        await DataInfo(new InfoViewModel
        {
            StartDate = now,
            EndDate = DateTime.Now,
            StartLevel = level,
            EndLevel = Level,
        });
        Status = false;
        await SoilMoistureLevelOff();
    }

    private async Task SoilMoistureLevelOff()
    {
        while (!Status && Level >= 1)
        {
            await Task.Delay(300000);
            Level--;
        }
    }

    private double GetSoilMoistureLevelFromFile()
    {
        if(File.Exists(LevelFilePath))
        {
            string levelText = File.ReadAllText(LevelFilePath);
            return Convert.ToDouble(levelText);
        }
        throw new Exception("File SoilMoistureLevel.txt not exist!");
    }

    private bool GetSprinklerStatusFromFile()
    {
        if (File.Exists(StatusFilePath))
        {
            string statusText = File.ReadAllText(StatusFilePath);
            return bool.TryParse(statusText, out bool status) ? status : false;
        }
        throw new Exception("File SprinklerStatus.txt not exist!");
    }

    private void SetSoilMoistureLevelToFile(double level)
    {
        File.WriteAllText(LevelFilePath, level.ToString());
    }

    private void SetSprinklerStatusToFile(bool status)
    {
        File.WriteAllText(StatusFilePath, status.ToString());
    }

    private async Task DataInfo(InfoViewModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("https://localhost:7265/api/SmartWatering/sensor-information", content);
    }
}
