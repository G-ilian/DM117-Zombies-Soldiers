using UnityEngine;
using UnityEngine.UI;

public class HPComponent : MonoBehaviour
{
    [SerializeField] Slider hpBar;

    public float HpValue
    {
        get { return hpBar.value; }
    }

    public void TakeDamage(int damage)
    {
        hpBar.value -= damage;
    }

    public void RecoverLife(int amount)
    {
        hpBar.value += amount;
    }
}
