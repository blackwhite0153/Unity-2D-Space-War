using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    private Slider _bossHp;

    private void OnEnable()
    {
        _bossHp = GetComponent<Slider>();

        _bossHp.minValue = 0;
        _bossHp.maxValue = 100;

        _bossHp.value = _bossHp.maxValue;
    }
}