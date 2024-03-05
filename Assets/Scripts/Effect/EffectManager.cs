using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static EffectManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public EffectSO effectData;
    public Dictionary<string, GameObject> effects;

    private void Start()
    {
        effects = new Dictionary<string, GameObject>();

        for (int i = 0; i < effectData.effects.Count; i++)
        {
            Effect effect = effectData.effects[i];
            effects.Add(effect.tag, effect.prefab);
        }
    }

    public void PlayEffect(string tag, Vector3 position)
    {
        GameObject newEffect = Instantiate(effects[tag], position, Quaternion.identity);
    }
}
