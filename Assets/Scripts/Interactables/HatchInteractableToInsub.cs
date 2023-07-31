using System.Collections;
using UnityEngine;

public class HatchInteractableToInsub : MonoBehaviour, IInteractable
{
    PlayerScript playerScript;
    public GameObject teleporter;
    public GameObject player;

    //added for auto access to player in prefab
    public SubController controller;

    public bool canLeave;
    public bool animBlock;
    public GameObject detectionPoint;
    public GameObject Ladder;
    public AnimationClip fadeClip;

    public static HatchInteractableToInsub instance;
    public static HatchInteractableToInsub Instance => instance;
    void Awake()
    {
        instance = this;
        player = controller.Player;
        playerScript = player.GetComponent<PlayerScript>();
        Ladder.SetActive(false);
    }

    public void Interact(GameObject player)
    {
        if (!GameManager.Instance.isFading)
        {
            if (!animBlock && GameManager.Instance.playerHealth > 0)
            {
                playerScript.ResetMoveVector();
                GameManager.Instance.isFading = true;
                StartCoroutine("Teleport");
                playerScript.GetComponent<Transform>().SetParent(SubController.instance.GetComponent<Transform>());
            }
        }
    }

    public IEnumerator Teleport()
    {
        playerScript.frozen = true; //removes player control...
        CanvasController.Instance.PlayQuickFade();
        yield return new WaitForSeconds(fadeClip.length); //for this long

        playerScript.inSub = true;
        GlobalSoundsManager.instance.PlaySubAmbience();
        GlobalSoundsManager.instance.StopWaterAmbience();
        Ladder.SetActive(false);
        PlayerScript.instance.HarpoonGun.SetActive(false);

        GlobalSoundsManager.instance.CutAmbientSounds();

        player.transform.rotation = Quaternion.Euler(0, 0, 0);

        playerScript.transform.position = teleporter.transform.position; //takes player to this position
        playerScript.frozen = false; //to restore player control
        yield return new WaitForSeconds(fadeClip.length); //waits this long...
        GameManager.Instance.isFading = false;
    }

    // a method that does all the hatch stuff but without the wait time. for use in loading checkpoints.
    public void SendToInsub()
    {
        playerScript.ResetMoveVector();
        playerScript.GetComponent<Transform>().SetParent(SubController.instance.GetComponent<Transform>());

        playerScript.inSub = true;
        Ladder.SetActive(false);
        PlayerScript.instance.HarpoonGun.SetActive(false);


        player.transform.rotation = Quaternion.Euler(0, 0, 0);

        playerScript.transform.position = teleporter.transform.position; //takes player to this position
        GameManager.Instance.isFading = false;
    }
}
