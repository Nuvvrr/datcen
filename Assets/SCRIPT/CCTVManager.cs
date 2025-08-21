using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CCTVManager : MonoBehaviour
{
    public CCTVFeed[] feeds;
    public float minErrorDelay = 10f;   // waktu minimal sebelum CCTV rusak
    public float maxErrorDelay = 30f;   // waktu maksimal sebelum CCTV rusak
    public int maxBrokenCCTV = 2;       // maksimal CCTV rusak bersamaan

    void Start()
    {
        // Set nama lokasi di UI
        foreach (var feed in feeds)
        {
            if (feed.locationText != null)
                feed.locationText.text = feed.locationName;
        }

        // Mulai random error loop
        StartCoroutine(RandomErrorLoop());
    }

    void Update()
    {
        foreach (var feed in feeds)
        {
            if (feed.isBroken)
            {
                if (feed.display != null && feed.offlineTexture != null)
                    feed.display.texture = feed.offlineTexture;
            }
            else
            {
                if (feed.display != null &&
                    feed.cctvCamera != null &&
                    feed.cctvCamera.targetTexture != null)
                {
                    feed.display.texture = feed.cctvCamera.targetTexture;
                }
            }
        }
    }

    //Coroutine buat bikin CCTV random error
    IEnumerator RandomErrorLoop()
    {
        while (true)
        {
            float delay = Random.Range(minErrorDelay, maxErrorDelay);
            yield return new WaitForSeconds(delay);

            // Hitung CCTV yang sedang rusak
            int brokenCount = 0;
            foreach (var feed in feeds)
                if (feed.isBroken) brokenCount++;

            // Kalau masih kurang dari maxBrokenCCTV, bikin CCTV random rusak
            if (brokenCount < maxBrokenCCTV)
            {
                // pilih CCTV random yang masih sehat
                int tries = 0;
                while (tries < 10)
                {
                    int index = Random.Range(0, feeds.Length);
                    if (!feeds[index].isBroken)
                    {
                        feeds[index].isBroken = true;
                        Debug.Log("CCTV " + feeds[index].locationName + " error!");
                        break;
                    }
                    tries++;
                }
            }
        }
    }

    //Dipanggil kalau player perbaiki CCTV
    public void RepairCCTV(string location)
    {
        foreach (var feed in feeds)
        {
            if (feed.locationName == location)
            {
                feed.isBroken = false;
                Debug.Log("CCTV di " + location + " diperbaiki!");
            }
        }
    }
}
