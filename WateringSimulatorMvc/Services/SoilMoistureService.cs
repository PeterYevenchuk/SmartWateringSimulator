using WateringSimulatorMvc.Models;

namespace WateringSimulatorMvc.Services;

public class SoilMoistureService
{
    private const string StatusFilePath = "wwwroot/SprinklerStatus.txt";

    public bool Status
    {
        get => GetSprinklerStatusFromFile();
        set => SetSprinklerStatusToFile(value);
    }

    public WateringViewModel Get()
    {
        var random = new Random();
        double soilMoisture = random.NextDouble() * 100;
        string formattedSoilMoisture = soilMoisture.ToString("0.00");
        double humidityData = Convert.ToDouble(formattedSoilMoisture);
        var result = new WateringViewModel
        {
            HumidityData = humidityData,
            SprinklerStatus = Status
        };
        return result;
    }

    public void TurnOn(bool turnOn)
    {
        Status = turnOn;
    }

    private bool GetSprinklerStatusFromFile()
    {
        if (File.Exists(StatusFilePath))
        {
            string statusText = File.ReadAllText(StatusFilePath);
            return bool.TryParse(statusText, out bool status) ? status : false;
        }
        return false;
    }

    private void SetSprinklerStatusToFile(bool status)
    {
        File.WriteAllText(StatusFilePath, status.ToString());
    }
}
