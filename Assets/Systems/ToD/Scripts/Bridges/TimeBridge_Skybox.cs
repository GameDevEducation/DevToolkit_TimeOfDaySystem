using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBridge_Skybox : TimeBridge_Base
{
    [SerializeField] float SunriseTime = 6f;
    [SerializeField] float SunsetTime = 18f;

    [SerializeField] Gradient SkyTint_Day;
    [SerializeField] AnimationCurve Exposure_Day;

    [SerializeField] Gradient SkyTint_Night;
    [SerializeField] AnimationCurve Exposure_Night;

    float DaylightLength;
    float NightLength;

    void Start()
    {
        DaylightLength = SunsetTime - SunriseTime;
        NightLength = SunriseTime + (ToDManager.Instance.DayLength - SunsetTime);
    }

    public override void OnTick(float CurrentTime)
    {
        float progress = -1f;
        bool isNight = true;
        if (CurrentTime < SunriseTime) // pre-dawn
            progress = (CurrentTime + (ToDManager.Instance.DayLength - SunsetTime)) / NightLength;
        else if (CurrentTime < SunsetTime) // during the day
        {
            progress = (CurrentTime - SunriseTime) / DaylightLength;
            isNight = false;
        }
        else // post sunset
            progress = (CurrentTime - SunsetTime) / NightLength;

        RenderSettings.skybox.SetFloat("_Exposure", isNight ? Exposure_Night.Evaluate(progress) : Exposure_Day.Evaluate(progress));
        RenderSettings.skybox.SetColor("_SkyTint", isNight ? SkyTint_Night.Evaluate(progress) : SkyTint_Day.Evaluate(progress));
    }
}
