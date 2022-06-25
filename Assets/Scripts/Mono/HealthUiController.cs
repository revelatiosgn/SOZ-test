using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo.Lite;

public class HealthUiController : MonoBehaviour
{
    [SerializeField] private ConvertToEntity _convertToEntity;
    [SerializeField] private Slider _healthProgress;

    private void Update()
    {
        ref HealthData healthData = ref Startup.World.GetPool<HealthData>().Get(_convertToEntity.TryGetEntity().Value);

        if (healthData.Health == healthData.MaxHealth || healthData.IsDead)
        {
            _healthProgress.gameObject.SetActive(false);
        }
        else
        {
            _healthProgress.gameObject.SetActive(true);
            _healthProgress.maxValue = healthData.MaxHealth;
            _healthProgress.value = healthData.Health;
        }
    }
}
