using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Enumerations;

public class FixedTrapManager : MonoBehaviour
{
    private Dictionary<TrapType, bool> trapTypes = new Dictionary<TrapType, bool>
    {
        {TrapType.SHOCK, false},
        {TrapType.ALERT_GUARD, false},
        {TrapType.ADD_SHADER, false}
    };

    private RoomModifiers roomModifiers;
    [SerializeField] private DoorpointManager doorpointManager;
    [SerializeField] private CameraShaderRandomizer cameraShaderRandomizer;
    
    void Start()
    {
        roomModifiers = transform.parent.GetComponent<RoomModifiers>();

        bool extraEffect = roomModifiers.GetModifierLevelByType(typeof(ExtraTrapEffect)) > 0;

        switch(Random.Range(0, trapTypes.Count))
        {
            case 0:
                trapTypes[TrapType.SHOCK] = true;
                break;

            case 1:
                trapTypes[TrapType.ALERT_GUARD] = true;
                break;

            case 2:
                trapTypes[TrapType.ADD_SHADER] = true;
                break;
            
            default:
                break;
        }

        if (extraEffect)
        {
            Dictionary<TrapType, bool> inactiveTrapTypes = trapTypes.Where(d => d.Value == false).ToDictionary(d => d.Key, d => d.Value);
            TrapType extraTrapType = inactiveTrapTypes.ElementAt(Random.Range(0,inactiveTrapTypes.Count)).Key;
            trapTypes[extraTrapType] = true;
        }

        foreach (Transform trap in transform)
        {
            TrapToggle toggler = trap.gameObject.GetComponent<TrapToggle>();
            if (toggler != null)
            {
                toggler.TrapDisable();
                toggler.trapCollider.damage = trapTypes[TrapType.SHOCK];
                toggler.trapCollider.alertGuard = trapTypes[TrapType.ALERT_GUARD];
                toggler.trapCollider.addShader = trapTypes[TrapType.ADD_SHADER];
                toggler.TrapEnable();
            }
            toggler.trapCollider.GetComponent<TrapCollider>().SetDoorPointManager(doorpointManager);
            toggler.trapCollider.GetComponent<TrapCollider>().SetCameraManager(cameraShaderRandomizer);
        }
    }
}
