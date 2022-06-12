using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int Health = 5;

    [Header("Iframes")]
    [SerializeField] private float IframeTime = 0.5f;
    [SerializeField] private Collider2D Hitbox;
    [SerializeField] private List<SpriteRenderer> Renderers;
    [Space]
    [SerializeField] private UnityIntEvent OnHealthChange;
    [Space]
    [SerializeField] private UnityVoidEvent OnDeath;

    private int MaxHealth;

    private void Start()
    {
        MaxHealth = Health;
        OnHealthChange?.Invoke(Health);
    }

    public void TakeDamage(int damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        OnHealthChange?.Invoke(Health);

        if (Health <= 0)
        {
            Death();
        }
        else
        {
            StartCoroutine(_Iframes());
        }
    }

    public void Heal(int heal)
    {
        Health = Mathf.Clamp(Health + heal, 0, MaxHealth);
        OnHealthChange?.Invoke(Health);
    }

    private void Death()
    {
        //To do: Death cutscene
        print("Death");
        OnDeath?.Invoke();
    }

    private IEnumerator _Iframes()
    {
        Hitbox.enabled = false;

        float progress = 0;
        while (progress < IframeTime)
        {
            //Toggle the renderer
            foreach (SpriteRenderer renderer in Renderers)
            {
                renderer.enabled = !renderer.enabled;
            }

            //Wait a bit, change blinking speed depending on the amount of iframes left
            if (progress < IframeTime * 0.75f)
            {
                //Slow blinking
                yield return new WaitForSeconds(0.1f);
                progress += 0.1f;
            }
            else
            {
                //Fast blinking
                yield return new WaitForSeconds(0.05f);
                progress += 0.05f;
            }
        }

        //Make sure the renderers are enabled once the Iframes are over
        foreach (SpriteRenderer renderer in Renderers)
        {
            renderer.enabled = true;
        }

        //Wait a little bit until you enable the hitbox to make it feel more fair
        yield return new WaitForSeconds(0.1f);
        Hitbox.enabled = true;
    }
}