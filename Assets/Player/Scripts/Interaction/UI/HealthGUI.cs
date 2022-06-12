using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGUI : MonoBehaviour
{
    [SerializeField] private Sprite FullHealthSprite;
    [SerializeField] private Sprite EmptyHealthSprite;
    [Space]
    [SerializeField] private List<Image> HealthContainers;

    public void OnHealthChange(int health)
    {
        for (int i = 0; i < HealthContainers.Count; i++)
        {
            if (i < health)
            {
                HealthContainers[i].sprite = FullHealthSprite;
            }
            else
            {
                HealthContainers[i].sprite = EmptyHealthSprite;
            }
        }
    }
}
