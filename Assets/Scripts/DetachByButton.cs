using UnityEngine;

[RequireComponent(typeof(RopeAttachment))]
public class DetachByButton : MonoBehaviour
{
    private RopeAttachment _attachment;

    private void Awake()
    {
        _attachment = GetComponent<RopeAttachment>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(InputConstants.DetachButtonName))
        {
            if (_attachment.IsAttached)
            {
                _attachment.Detach();
            }
        }
    }
}
