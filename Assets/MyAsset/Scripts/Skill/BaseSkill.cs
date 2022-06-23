using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public abstract class BaseSkill
{
    protected int index;
    protected int value;
    protected string name;
    protected string description;
    protected float cooldownTime;
    protected string skillImagePath;
    protected Sprite skillImage;
    protected float timerCooldown;

    public int Index { get { return index; } }
    public int Value { get { return value; } }
    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public float CooldownTime { get { return cooldownTime; } }
    public bool IsCooldown { get { return timerCooldown > 0f; } }
    public string SkillImagePath { get { return skillImagePath; } }
    public Sprite SkillImage { get { return skillImage; } }
    public float TimerCooldown { get { return timerCooldown; } set { timerCooldown = value; } }

    //public BaseSkill(int index, int value, string name, float cooldownTime, string skillImagePath)
    //{
    //    this.index = index;
    //    this.value = value;
    //    this.name = name;
    //    this.cooldownTime = cooldownTime;
    //    this.skillImagePath = skillImagePath;
    //    skillImage = ResourceManager.Instance.FindResource<Image>(skillImagePath);
    //}

    public virtual void Effect(Transform attacker, Transform target)
    {
        timerCooldown = cooldownTime;
        Main.Instance.StartCoroutine(FloatSkillTextCoro());
    }

    private readonly float floatTime = 2f;
    private float timerFloat = 0f;
    private readonly float floatSpeed = 2f;
    private IEnumerator FloatSkillTextCoro()
    {
        // ʵ����һ�����ֶ���
        GameObject textPrefab = ResourceManager.Instance.FindResource("Prefab/UI/SkillName");
        TextMeshProUGUI text = Object.Instantiate(textPrefab).GetComponent<TextMeshProUGUI>();
        // ���ø����󡢽Ƕȡ��ı�
        text.transform.SetParent(UIManager.Instance.WorldCanvas, false);
        text.transform.localEulerAngles = new(45, 0, 0);
        text.text = name + ": " + description + "��";
        // ��ʼ������
        Vector3 pos = PlayerManager.Instance.PlayerCharacter.transform.position + Vector3.up + Vector3.back;
        timerFloat = floatTime;
        // ��ʼ�ϸ�
        while (timerFloat > 0f)
        {
            // ��͸��
            text.alpha = Mathf.Max(floatTime, timerFloat * 2) / floatTime;
            timerFloat = Mathf.Max(0f, timerFloat - Time.deltaTime);
            // λ������
            text.transform.position = pos;
            pos.y += floatSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Object.Destroy(text.gameObject);
    }
}
