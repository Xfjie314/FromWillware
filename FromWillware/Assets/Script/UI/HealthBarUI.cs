using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider hpSlider;
    public Character targetCharacter; // 拖入你的敌人对象

    void Start()
    {
        if (hpSlider == null) hpSlider = GetComponent<Slider>();

        // 如果没有手动拖入，尝试从父物体获取
        if (targetCharacter == null)
        {
            targetCharacter = GetComponentInParent<Character>();
        }

        if (targetCharacter != null)
        {
            hpSlider.maxValue = targetCharacter.MaxHP;
            hpSlider.value = targetCharacter.CurrentHP;
        }
    }

    void Update()
    {
        if (targetCharacter != null)
        {
            // 实时同步血量（因为你不能改Enemy脚本，所以在这里每帧监听是最保险的）
            hpSlider.value = targetCharacter.CurrentHP;

            // 如果敌人死亡，隐藏血量条
            if (targetCharacter.CurrentHP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}