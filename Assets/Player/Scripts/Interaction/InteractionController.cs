using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public int InteractionCount = 0;
    [Space]
    /// <summary>
    /// Interaction that happens the next time the
    /// </summary>
    public List<Interaction> Data;

    public void Loop(Interacter interacter)
    {
        StartCoroutine(_Loop(interacter));
    }

    private IEnumerator _Loop(Interacter interacter)
    {
        interacter.Interacting = true;

        //Lock movement controls
        Player.Instance.Moving = false;

        //Get the current interaction
        //Loop while interaction is happening
        int index = 0;
        Interaction interaction = Data[InteractionCount];
        while (index < interaction.Components.Count)
        {
            //Get the current component, and activate it based on what it is
            InteractionComponent Component = interaction.Components[index];
            switch (Component.Type)
            {
                case InteractionType.Dialogue:
                    DialogueWriter.Instance.Write((Dialogue)Component);
                    break;
                default:
                    throw new System.NotImplementedException("Interaction component not implemented!");
            }

            //Wait until the component is no longer being used
            while (Component.Active)
            {
                yield return null;
            }

            yield return null;
            index++;
        }

        //Up the amount of interactions, so that the next interaction has a different line
        InteractionCount++;
        //Clamp the counter so the last interaction gets looped
        InteractionCount = Mathf.Clamp(InteractionCount, 0, Data.Count - 1);

        //Unlock movement controls
        Player.Instance.Moving = true;
        interacter.Interacting = false;
    }
}