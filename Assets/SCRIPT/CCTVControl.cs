using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CCTVControl : MonoBehaviour
{
    [Header("Status CCTV")]
    public bool isBroken = false;
    private bool isHacking = false;
    private bool isPlayerNearby = false;

    [Header("Lampu Indikator")]
    public Light cctvLight;       // Lampu CCTV (hijau/merah)

    [Header("UI Hacking")]
    public GameObject hackingUIPanel; // Panel UI hacking
    public Slider hackingProgress;    // Slider progress hacking
    public float hackingTime = 3f;    // Lama hacking

    void Start()
    {
        // CCTV rusak otomatis setelah 10 detik
        Invoke("BreakCCTV", 10f);

        if (hackingUIPanel != null)
            hackingUIPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && isBroken && !isHacking)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(HackCCTV());
            }
        }
    }

    void BreakCCTV()
    {
        isBroken = true;
        Debug.Log("CCTV Rusak!");

        if (cctvLight != null)
            cctvLight.color = Color.red; // indikator rusak
    }

    IEnumerator HackCCTV()
    {
        isHacking = true;
        hackingUIPanel.SetActive(true);
        hackingProgress.value = 0;

        float elapsed = 0f;

        while (elapsed < hackingTime)
        {
            elapsed += Time.deltaTime;
            hackingProgress.normalizedValue = elapsed / hackingTime;
            yield return null;
        }

        // selesai hacking
        hackingUIPanel.SetActive(false);
        isBroken = false;
        isHacking = false;

        Debug.Log("CCTV berhasil diperbaiki!");

        if (cctvLight != null)
            cctvLight.color = Color.green; // balik ke normal
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = false;
    }
}
