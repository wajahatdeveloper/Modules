using System.Collections;
using System.Collections.Generic;
using Ilumisoft.RadarSystem;
using UnityEngine;

namespace GameFeatures
{
    public class MinimapSystem : SingletonBehaviour<MinimapSystem>
    {
        [Header("References")]
        public Radar radar;
        public GameObject minimapCanvas;

        [Header("Attributes")]
        public bool autoShowMinimapOnInit;

        public void Init(GameObject localPlayerObject)
        {
            radar.Player = localPlayerObject;

            if (autoShowMinimapOnInit)
            {
                ShowRadar();
            }
        }

        public void ShowRadar()
        {
            minimapCanvas.SetActive(true);
        }

        public void HideRadar()
        {
            minimapCanvas.SetActive(false);
        }
    }
}