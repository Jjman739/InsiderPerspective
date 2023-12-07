using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    [SerializeField] private TrapToggle toggler;

    private DoorpointManager doorpointManager;
    private CameraShaderRandomizer cameraShaderRandomizer;

    public bool damage = true;
    public bool selfDelete = true;
    public bool alertGuard = false;
    public bool addShader = false;

    public float shaderRobotChance = 0.2f;

    void OnTriggerEnter(Collider other)
    {
        ThiefManager thiefManager = other.gameObject.GetComponent<ThiefManager>();
        if (thiefManager != null)
        {
            if (damage)
            {
                thiefManager.TakeDamage();
                DialogueManager.Instance.PlayDialogue(Enumerations.DialogueEvent.TRAP_SHOCK);
            }
            if (selfDelete)
            {
                toggler.TrapDisable();
            }
            if (alertGuard)
            {
                doorpointManager.SendGuardToRoom();
                DialogueManager.Instance.PlayDialogue(Enumerations.DialogueEvent.TRAP_GUARD);
            }
            if (addShader)
            {
                cameraShaderRandomizer.applyRandomShader();
                DialogueManager.Instance.PlayDialogue(Enumerations.DialogueEvent.TRAP_CAMERA);
                float rval = Random.value;
                if (rval <= shaderRobotChance)
                {
                    Debug.Log("Apply shader to robot");
                    cameraShaderRandomizer.applyRandomShader(thiefManager.GetCameraObject());
                    thiefManager.needsRepair = true;
                }
            }
        }
    }

    public void SetDoorPointManager(DoorpointManager dm)
    {
        doorpointManager = dm;
    }

    public void SetCameraManager(CameraShaderRandomizer csr)
    {
        cameraShaderRandomizer = csr;
    }
}
