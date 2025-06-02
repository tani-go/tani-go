    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlantPlot : MonoBehaviour
    {    
        public SoilState soilState = SoilState.Empty;
        public PlantData plantData;
        public int stage = 0;
        private float timer = 0f;
        private int daysWaited = 0;
        private GameObject currentPlantObj;

        public bool isDead = false;
        private int prepDayCounter = 0;
        private bool isPreparing = false;
        private bool isWatered = false; // Untuk jagung
        private bool hasWateredToday = false;
        private bool hasRemovedPestToday = false;
        bool matiKarenaAir = false;
        bool matiKarenaHama = false;

        public GameObject tanahNormal;
        public GameObject tanahBajak;
        public GameObject tanahBerair;
        public GameObject tanahBajakBasah;
        public enum SoilState
        {
            Empty,
            Bajak,
            BajakBasah,
            Air,    
            Growing
        }

        public void Plant(PlantData newPlant)
        {
        if (plantData != null && !isDead && stage < plantData.growthPrefabs.Length - 1)
            {
                Debug.Log("Plot masih ada isinya");
                return;
            }
            else if (isDead)
            {
                Debug.Log("Tanaman mati harus dibersihkan dulu!");
                return;
            }


            plantData = newPlant;
            stage = 0;
            timer = 0f;
            daysWaited = 0;
            isDead = false;
            isPreparing = true;
            prepDayCounter = 0;
            isWatered = false;
            // Hari pertama: bajak
            soilState = SoilState.Bajak;
            UpdateVisual();
        }

        void Start()
        {
            GameTimeManager timeManager = FindObjectOfType<GameTimeManager>();
            if (timeManager != null)
            {
                timeManager.onDayPassed.AddListener(GrowPerDay);
            }
                UpdateVisual();
        }

        void UpdateVisual()
        {
            // 🔁 Hapus tanaman lama dulu
            if (currentPlantObj != null)
                Destroy(currentPlantObj);

            // 🌱 Update visual tanaman
            if (plantData != null)
            {
                if (isDead && plantData.wiltedPrefab != null)
                {
                    currentPlantObj = Instantiate(plantData.wiltedPrefab, transform.position + Vector3.up * 2f, Quaternion.identity, transform);
                }
                else if (stage < plantData.growthPrefabs.Length)
                {
                    currentPlantObj = Instantiate(plantData.growthPrefabs[stage], transform.position + Vector3.up * 2f, Quaternion.identity, transform);
                }
            }

            // 🎨 Update visual tanah
            tanahNormal.SetActive(false);
            tanahBajak.SetActive(false);
            tanahBerair.SetActive(false);
            tanahBajakBasah.SetActive(false); 

        switch (soilState)
        {
            case SoilState.Empty:
                tanahNormal.SetActive(true);
                Debug.Log("Tanah Normal Aktif");
                break;
            case SoilState.Bajak:
                tanahBajak.SetActive(true);
                Debug.Log("Tanah Bajak Aktif");
                break;
            case SoilState.Air:
                tanahBerair.SetActive(true);
                Debug.Log("Tanah Berair Aktif");
                break;
            case SoilState.BajakBasah:
                tanahBajakBasah.SetActive(true);
                Debug.Log("Tanah Bajak Basah Aktif");
                break;
        }

        }


        public void GrowPerDay()
        {
            if (plantData == null || isDead || stage >= plantData.growthPrefabs.Length) return;
            if (isPreparing)
            {
                prepDayCounter++;

                if (plantData.plantType == PlantData.PlantType.Padi)
                {
                    if (prepDayCounter == 2)
                    {
                        soilState = SoilState.Air; // Hari ke-2: dialiri air
                        UpdateVisual();
                        return;
                    }
                    else if (prepDayCounter == 3)
                    {
                        isPreparing = false;
                        stage = 0;
                        UpdateVisual();
                        return;
                    }
                }
                else if (plantData.plantType == PlantData.PlantType.Jagung)
                {
                    if (prepDayCounter == 2)
                    {
                        isPreparing = false;
                        stage = 0;
                        UpdateVisual();
                        return;
                    }
                }

                return; // jangan tumbuh selama masa persiapan
            }

            if (stage == 1)
            {
                var currentSeason = FindObjectOfType<SeasonManager>().GetCurrentSeason();
                bool salahMusim =
                    (plantData.plantType == PlantData.PlantType.Padi && currentSeason == SeasonManager.Season.Kemarau) ||
                    (plantData.plantType == PlantData.PlantType.Jagung && currentSeason == SeasonManager.Season.Hujan);

                if (salahMusim)
                {
                    isDead = true;
                    Debug.Log("Tanaman gagal panen di stage 1 karena musim tidak sesuai");
                    UpdateVisual();
                    return;
                }
            }

            bool hasStageJustChanged = false; // ❗ Dideklarasikan hanya sekali

            if (plantData.plantType == PlantData.PlantType.Jagung)
            {
                if (!hasWateredToday)
                {
                    matiKarenaAir = true;
                }

                if (!hasRemovedPestToday)
                {
                    matiKarenaHama = true;
                }
            }
            else if (plantData.plantType == PlantData.PlantType.Padi)
            {
                if (hasStageJustChanged && soilState != SoilState.Air)
                {
                    matiKarenaAir = true;
                }

                if (!hasRemovedPestToday)
                {
                    matiKarenaHama = true;
                }
            }

            if (matiKarenaAir || matiKarenaHama)
            {
                isDead = true;
                Debug.Log($"Tanaman mati! Air: {matiKarenaAir}, Hama: {matiKarenaHama}");
                UpdateVisual();
                return;
            }

            daysWaited++;
            if (daysWaited >= plantData.growthTimes[stage])
            {
                stage++;
                daysWaited = 0;
                hasStageJustChanged = true;
                UpdateVisual();
            }

            // Reset harian
            hasWateredToday = false;
            hasRemovedPestToday = false;
        }

        public void Harvest(){
            if(plantData ==  null || isDead || stage < plantData.growthPrefabs.Length-1)
            {
                Debug.Log("Belum bisa dipanen/Mati");
                return;
            }

            Debug.Log("Tanaman dipanen");
                plantData = null;
                stage = 0;
                timer = 0f;
                daysWaited = 0;
                isDead = false;
                UpdateVisual(); // Bersihkan visual
        }
        public void ClearDeadPlant()
        {
            if (isDead)
            {
                Debug.Log("Tanaman mati dibersihkan.");
                plantData = null;
                stage = 0;
                timer = 0f;
                daysWaited = 0;
                isDead = false;
                soilState = SoilState.Empty; // Kembalikan tanah ke kosong
                UpdateVisual();
            }
            else
            {
                Debug.Log("Tidak ada tanaman mati yang perlu dibersihkan.");
            }
        }

        public void Water()
        {
            if (plantData != null && plantData.plantType == PlantData.PlantType.Jagung && soilState == SoilState.Bajak)
            {
                soilState = SoilState.BajakBasah;
                isWatered = true;
                UpdateVisual();
            }
        }
        public void WaterPlantToday()
        {
            hasWateredToday = true;
            Debug.Log("Tanaman disiram hari ini.");
        }

        public void RemovePestToday()
        {
            hasRemovedPestToday = true;
            Debug.Log("Hama dihapus hari ini.");
        }

    }
