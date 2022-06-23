using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class NPCBaseCharacter : BaseCharacter
{
    protected Vector3 originPosition;  // 初始位置
    protected Transform target;  // 接近或攻击的目标
    protected Transform defaultTarget;  // 默认目标
    protected bool isControlled = false;  // 是否被其它对象控制
    // 血条相关
    private RectTransform healthBar;  // 血槽
    private RectTransform healthRemain;  // 剩余血条
    private float healthRemainOriginWidth;  // 血条初始长度

    public Vector3 OriginPosition { get { return originPosition; } set { originPosition = value; } }
    public Transform Target { get { return target; } set { target = value; } }
    public Transform DefaultTarget { get { return defaultTarget; } set { defaultTarget = value; } }
    public Vector3 DistanceToTarget { get { return target == null ? Vector3.negativeInfinity : target.position - transform.position; } }
    public bool IsControlled { get { return isControlled; } set { isControlled = value; } }

    protected override void Awake()
    {
        base.Awake();
        NPCManager.Instance.RegistNPC(this);
        // 初始化血条
        GameObject healthBarPrefab = ResourceManager.Instance.FindResource(StringUIPanelPath.npcHealthBar);
        healthBar = Instantiate(healthBarPrefab).GetComponent<RectTransform>();
        healthBar.name = healthBar.name.Replace("(Clone)", "");
        healthBar.SetParent(UIManager.Instance.WorldCanvas);
        healthBar.gameObject.SetActive(false);
        healthRemain = healthBar.GetChild(0).GetComponent<RectTransform>();
        healthRemainOriginWidth = healthRemain.rect.width;
    }

    public abstract void Initial();

    protected virtual void Update()
    {

    }

    public void SetHealthBarActive(bool active) 
    {
        healthBar.gameObject.SetActive(active);
    }

    public void UpdateHealthBar(int health, int maxHealth)
    {
        if (!healthBar.gameObject.activeInHierarchy && health < maxHealth)
        {
            healthBar.gameObject.SetActive(true);
        }
        healthRemain.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, healthRemainOriginWidth * health / maxHealth);
        healthBar.position = transform.position + Vector3.up * 2;
    }

    protected virtual void OnDestroy()
    {
        NPCManager.Instance.UnRegistNPC(this);
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }
        if (healthRemain != null)
        {
            Destroy(healthRemain.gameObject);
        }
    }
}