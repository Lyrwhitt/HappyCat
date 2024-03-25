using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    private static CooldownManager instance;

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

    public static CooldownManager Instance
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

    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();

    public bool SkillUsable(string skillName, float cooldown)
    {
        if (cooldowns.ContainsKey(skillName))
        {
            if (Time.time < cooldowns[skillName])
            {
                return false;
            }
        }

        cooldowns[skillName] = Time.time + cooldown;

        return true;
    }
}
