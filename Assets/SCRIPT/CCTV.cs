using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CCTV : MonoBehaviour
{
    public bool isBroken = false;
    public GameObject hackingUIPanel; // Panel UI untuk hacking
    public Slider hackingProgress;    // Slider progress hacking
    public float hackingTime = 3f;    // Lama waktu perbaikan

    private bool isPlayerNearby = false;
    private bool isHacking = false;

    void Update()
    {
        if (isPlayerNearby && isBroken && !isHacking)
        {
            if (Input.GetKeyDown(KeyCode.E)) // Tombol interaksi
            {
                StartCoroutine(HackCCTV());
            }
        }
    }

    private IEnumerator HackCCTV()
    {
        isHacking = true;
        hackingUIPanel.SetActive(true);
        hackingProgress.value = 0;

        float elapsed = 0f;
        while (elapsed < hackingTime)
        {
            elapsed += Time.deltaTime;
            hackingProgress.value = elapsed / hackingTime;
            yield return null;
        }

        hackingUIPanel.SetActive(false);
        isBroken = false;
        isHacking = false;

        Debug.Log("CCTV berhasil diperbaiki!");
        // Tambahkan efek lain, misalnya lampu kembali hijau
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
