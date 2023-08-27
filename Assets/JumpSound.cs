using UnityEngine;

public class JumpSound : MonoBehaviour
{
    public GameObject Feet;
    AudioSource JumpSource;
    private void Start()
    {
        JumpSource = Feet.GetComponents<AudioSource>()[1];
    }
    public void PlayLightJump()
    {
        if (!PilotPanelInteractable.Instance.controlSub && !TurretInteractable.Instance.controlTurret)
        {
            JumpSource.pitch = 1.5f;
            JumpSource.Play();
        }
    }
    public void PlayHeavyJump()
    {

        if (!PilotPanelInteractable.Instance.controlSub && !TurretInteractable.Instance.controlTurret)
        {
            JumpSource.pitch = 1f;
            JumpSource.Play();
        }
    }
}
