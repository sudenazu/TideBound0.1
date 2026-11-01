
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [Header("Evler")]
    public GameObject ev;
    public GameObject ev1;
    public GameObject ev2;

    void Start()
    {
        UpdateHouse();
    }

    public void UpdateHouse()
    {
        bool purchased = GameManager.sunPanelPurchased;

        Debug.Log("Güneş paneli satın alındı mı: " + purchased);

        if (ev != null)
            ev.SetActive(!purchased);

        if (ev1 != null)
            ev1.SetActive(purchased);

        if (ev2 != null)
            ev2.SetActive(false);

        if (ev1 == null)
            Debug.LogError("Inspector'da ev1 bağlantısı yapılmamış!");
    }
}
