using UnityEngine;

public class CCTVViewSweep : MonoBehaviour
{
    public Camera cctvCamera;
    public float speed = 30f;
    public float angle = 45f;
    private float baseY;

    void Start()
    {
        baseY = cctvCamera.transform.eulerAngles.y;
    }

    void Update()
    {
        float y = baseY + Mathf.Sin(Time.time * speed * Mathf.Deg2Rad) * angle;
        cctvCamera.transform.rotation = Quaternion.Euler(0, y, 0);
    }
}