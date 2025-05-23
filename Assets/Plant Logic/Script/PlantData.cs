using System.Collections;
using System.Collections.Generic;
using UnityEngine;

        [CreateAssetMenu(fileName = "NewPlant", menuName = "Tanaman")]
        public class PlantData : ScriptableObject 
    {
        public enum PlantType { Padi, Jagung }

        public PlantType plantType;
        public GameObject[] growthPrefabs; // prefab untuk tiap tahap (tanam, tumbuh, panen)
        public float[] growthTimes; // waktu tumbuh per tahap (dalam detik)
    }
