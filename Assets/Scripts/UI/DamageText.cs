using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText;
    private Color originalColor;

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
        damageText.rectTransform.position = Camera.main.WorldToScreenPoint(position);
        damageText.text = Mathf.FloorToInt(damage).ToString();

        StartCoroutine(FadeOutDamageText());
    }

    private IEnumerator FadeOutDamageText()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1.5f);

        yield return waitForSeconds; // 1.5�� ���

        float fadeDuration = 1.0f; // ������ �����ϰ� �Ǵ� �ð�
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // ������ �����ϰ� �����
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeDuration);
            damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, alpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        ObjectPool.Instance.ReturnToPool("DamageText", this.gameObject);
    }
}
