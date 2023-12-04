using System;
using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkTimer : MonoBehaviourPunCallbacks
{
    private const string TimerKey = "StartTime";

    [Header("References")]
    public TextMeshProUGUI text;
    public Image image;

    public event Action OnTimerExpired;

    private double _timer = 0;
    private double _timerIncrementValue = 0;
    private double _startTime = 0;
    private bool _isTimerRunning = false;

    public void SetTimer(double timer)
    {
        _timer = timer;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Routine_Initialize());
    }

    private IEnumerator Routine_Initialize()
    {
        _startTime = PhotonNetwork.Time;

        if (PhotonNetwork.IsMasterClient)
        {
            var props = new Hashtable
            {
                { TimerKey, _startTime }
            };

            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }

        yield return new WaitForSeconds(2.0f);

        _startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties[TimerKey].ToString());

        yield return new WaitForSeconds(2.0f);

        _isTimerRunning = true;
    }

    private void Update()
    {
        if (!_isTimerRunning) { return; }

        _timerIncrementValue = PhotonNetwork.Time - _startTime;

        var seconds = _timer - _timerIncrementValue;

        text.text = $"{seconds / 60:00}:{seconds % 60:00}";

        if (_timerIncrementValue >= _timer)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                OnTimerExpired?.Invoke();
            }

            _isTimerRunning = false;
        }
    }
}