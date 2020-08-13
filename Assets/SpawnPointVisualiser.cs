using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointVisualiser : MonoBehaviour
{
    [SerializeField]
    private Color drawColour = Color.white;

    [SerializeField]
    private float drawRadius = 0.5f;

    private void OnDrawGizmos()
    {
        //Draw Spawn Points
        Transform[] childTransforms = transform.GetComponentsInChildren<Transform>();
        for(int i = 0; i < childTransforms.Length; ++i)
        {
            Gizmos.color = drawColour; //Set colour
            Gizmos.DrawSphere(childTransforms[i].position, drawRadius);
            Gizmos.color = Color.white; //Reset Colour so that we don't mess up anything else
        }
    }
}
