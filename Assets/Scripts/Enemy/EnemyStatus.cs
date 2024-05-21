using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Health
{
    protected override void Start()
    {
        base.Start();

        AddEvent();
    }

    private void AddEvent()
    {
        onDie += OnDie;
    }


    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        DamageText damageText;
        damageText = ObjectPool.Instance.SpawnFromPool("DamageText", Vector3.zero, Quaternion.identity,
            UIManager.Instance.canvas.transform).GetComponent<DamageText>();

        damageText.ShowDamageText(this.transform.position, damage);
    }

    private void OnDie()
    {
        Debug.Log("�ֱऱ");
    }
}
