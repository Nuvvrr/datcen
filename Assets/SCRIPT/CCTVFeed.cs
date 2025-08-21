using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CCTVFeed
{
    public string locationName;      // Nama lokasi CCTV
    public Camera cctvCamera;        // Kamera di scene
    public RawImage display;         // UI RawImage di monitor
    public Text locationText;        // UI Text lokasi
    public Texture offlineTexture;   // Texture kalau rusak
    public bool isBroken = false;    // Status CCTV
}