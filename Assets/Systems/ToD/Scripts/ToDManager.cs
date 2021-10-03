using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDManager : MonoBehaviour
{
    [Tooltip("Length of a day in hours")]
    [SerializeField] float _DayLength = 24f;

    [Tooltip("Starting time in hours relative to midnight")]
    [SerializeField] float _StartingTime = 6f;

    [Tooltip("Controls how fast time advances (eg. 60 = 1 real world second = 1 game minute")]
    [SerializeField] float _TimeFactor = 60f;

    [Tooltip("Time bridges to send data to")]
    [SerializeField] List<TimeBridge_Base> Bridges;

    public float DayLength => _DayLength;

    public float DeltaTime => Time.deltaTime * _TimeFactor;

    public static ToDManager Instance { get; private set; } = null;

    float CurrentTime = 0f;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplicate ToDManager found on " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        CurrentTime = _StartingTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // advance the time
        CurrentTime = (DayLength + CurrentTime + (Time.deltaTime * _TimeFactor / 3600f)) % DayLength;

        // update the time
        foreach(var bridge in Bridges)
            bridge.OnTick(CurrentTime);
    }
}
