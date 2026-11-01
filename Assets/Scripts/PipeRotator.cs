using UnityEngine;
using System.Collections;

public class PipeRotator : MonoBehaviour
{
    private bool isRotating = false;

    // Bu butonun ID'sini inspector'dan seçeceğiz (Örn: 3, 4, 5, 7 veya 8)
    public int pipeID;

    public void RotatePipeSmooth()
    {
        if (isRotating) return;

        StartCoroutine(RotateSmoothly(-90f, 0.2f));

        // GameManager'a bu borunun döndüğünü haber ver
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CheckPipeRotation(pipeID);
        }
    }

    IEnumerator RotateSmoothly(float angle, float duration)
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, angle);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        transform.rotation = endRotation;
        isRotating = false;
    }
}