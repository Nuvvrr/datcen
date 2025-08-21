using UnityEngine;

public class ComputerInteract : MonoBehaviour
{
    public GameObject cctvUI;   // Panel Canvas yang berisi grid CCTV
    private bool isPlayerNearby = false;

    void Start()
    {
        if (cctvUI != null)
            cctvUI.SetActive(false); // pastikan UI mati di awal
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            bool state = !cctvUI.activeSelf;
            cctvUI.SetActive(state);

            if (state)
                Debug.Log("CCTV Computer Dibuka");
            else
                Debug.Log("CCTV Computer Ditutup");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player dekat komputer. Tekan [E] untuk akses CCTV.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
