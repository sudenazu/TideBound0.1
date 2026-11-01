using UnityEngine;
using UnityEngine.EventSystems;

public enum PipeType { Straight, Elbow }

// Her boru parçasına (UI Image) eklenir. Tıklayınca 90° döner.
public class PipeTile : MonoBehaviour, IPointerClickHandler
{
    public PipeType type = PipeType.Straight;
    [Range(0, 3)] public int rotation = 0;   // 0..3  (her adım 90° saat yönü)

    [HideInInspector] public PipeGameManager manager;

    // Yönler: 0 = Yukarı, 1 = Sağ, 2 = Aşağı, 3 = Sol
    // Düz boru: Yukarı + Aşağı  |  Dirsek: Yukarı + Sağ  (rotation = 0 iken)
    bool BaseOpen(int side)
    {
        if (type == PipeType.Straight) return side == 0 || side == 2;
        return side == 0 || side == 1;
    }

    public bool IsOpen(int side)
    {
        int baseSide = (side - rotation + 4) % 4;
        return BaseOpen(baseSide);
    }

    public void SetRotation(int r)
    {
        rotation = ((r % 4) + 4) % 4;
        transform.localRotation = Quaternion.Euler(0, 0, -90f * rotation);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (manager == null || !manager.IsPlaying) return;
        SetRotation(rotation + 1);
        manager.CheckWin();
    }
}