using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText;
    private Color originalColor;
    private float randomRange = 0.5f;

    private float moveSpeed = 50.0f;

    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        originalColor = damageText.color;
    }

    private void OnEnable()
    {
        damageText.color = originalColor;
    }

    public void ShowDamageText(Vector3 position, float damage)
    {
        Vector3 randomPosition = new Vector3(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange), 0f);
        Vector3 spawnPosition = position + randomPosition;

        damageText.rectTransform.position = Camera.main.WorldToScreenPoint(spawnPosition);
        damageText.SetText(Mathf.FloorToInt(damage).ToString());

        StartCoroutine(FadeOutDamageText());
    }

    private IEnumerator FadeOutDamageText()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.4f);

        yield return waitForSeconds;

        float fadeDuration = 0.6f; // 서서히 투명하게 되는 시간
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // 서서히 투명하게 만들기
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeDuration);
            damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, alpha);

            damageText.rectTransform.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        ObjectPool.Instance.ReturnToPool("DamageText", this.gameObject);
    }
}
