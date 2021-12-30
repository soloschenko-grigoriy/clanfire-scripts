using System;
using GS.Hex;
using UnityEngine;

namespace GS
{
    public class Level : MonoBehaviour
    {
        private HexMapSaver _mapSaver;

        private void Awake()
        {
            _mapSaver = GetComponent<HexMapSaver>();
        }

        private void Start()
        {
            _mapSaver.LoadMap();
        }
    }
}
