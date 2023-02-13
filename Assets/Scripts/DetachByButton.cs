using UnityEngine;

public class DetachByButton : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown(InputConstants.DetachButtonName))
        {
            DetachCharacter();
        }
    }

    private void DetachCharacter()
    {
        var joint = GetComponent<HingeJoint2D>();
        if (joint)
        {
            Destroy(joint);
        }
    }
}
