using System;
using System.Collections;
using Photon.Pun;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkTimer : MonoBehaviour
{
    private const string LogClassName = "NetworkTimer";
    private const string NetworkTimerKey = "NetworkStartTime";

    public event Action OnTimerTriggered;

    [Header("Attributes")]
    public bool useLocalDeltaTime = true;

    [Header("References")]
    public TextMeshProUGUI text;
    public Image image;

    [ReadOnly] [SerializeField] private double _timerTarget = 0;
    [ReadOnly] [SerializeField] private double _timeRemaining = 0;
    [ReadOnly] [SerializeField] private double _timeElapsedSinceStart = 0;
    [ReadOnly] [SerializeField] private double _startTimeStamp = 0;
    [ReadOnly] [SerializeField] private bool _isTimerRunning = false;

    public void SetTimer(double timer)
    {
        _timerTarget = timer;

        DebugX.Log($"{LogClassName} : Timer Set at value {timer}.", LogFilters.None, gameObject);

        StartCoroutine(Routine_Initialize());
    }

    private IEnumerator Routine_Initialize()
    {
        _startTimeStamp = (PhotonNetwork.ServerTimestamp / 1000.0f).Abs();

        if (PhotonNetwork.IsMasterClient)
        {
            var props = new Hashtable
            {
                { NetworkTimerKey, _startTimeStamp }
            };

            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }

        yield return new WaitForSeconds(2.0f);

        _startTimeStamp = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties[NetworkTimerKey].ToString());

        yield return new WaitForSeconds(2.0f);

        _isTimerRunning = true;

        DebugX.Log($"{LogClassName} : Timer Start at value {_startTimeStamp}.", LogFilters.None, gameObject);
    }

    private void Update()
    {
        if (!_isTimerRunning) { return; }

        var currentTimeStamp = (PhotonNetwork.ServerTimestamp / 1000.0f).Abs();

        if (useLocalDeltaTime)
        {
            _timeElapsedSinceStart += Time.deltaTime;
        }
        else
        {
            _timeElapsedSinceStart = currentTimeStamp - _startTimeStamp;
        }

        _timeRemaining = _timerTarget - _timeElapsedSinceStart;

        text.text = $"{(int)_timeRemaining / 60:00}:{(int)_timeRemaining % 60:00}";

        if (_timeElapsedSinceStart >= _timerTarget)
        {
            _isTimerRunning = false;
            if (PhotonNetwork.IsMasterClient) { OnTimerTriggered?.Invoke(); }
        }
    }
}