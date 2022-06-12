using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Sprite FullHealthContainer;
    [SerializeField] private Sprite EmptyHealthContainer;
    [Space]
    [SerializeField] private List<Image> HealthContainers;

    public void UpdateHealth(int health)
    {
        for (int i = 0; i < HealthContainers.Count; i++)
        {
            if (i < health)
            {
                HealthContainers[i].sprite = FullHealthContainer;
            }
            else
            {
                HealthContainers[i].sprite = EmptyHealthContainer;
            }
        }
    }

    public void UpdateMaxHealth(int health)
    {
        for (int i = 0; i < HealthContainers.Count; i++)
        {
            if (i < health)
            {
                HealthContainers[i].enabled = true;
            }
            else
            {
                HealthContainers[i].enabled = false;
            }
        }
    }
}
