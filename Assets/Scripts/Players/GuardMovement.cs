using Unity.Netcode;
using UnityEngine;

public class GuardMovement : MonoBehaviour {
    private CharacterController controller;

    [SerializeField] private float turnSpeed = 5f;
    private bool interacting = false;
    private AudioListener audioListener;
    private Camera mainCamera;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        audioListener = GetComponent<AudioListener>();
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        audioListener.enabled = mainCamera.enabled;

        if (Cursor.lockState != CursorLockMode.Locked) return;

        Vector3 euler = transform.rotation.eulerAngles;
        euler += turnSpeed * new Vector3(Input.GetAxis("GuardY"), Input.GetAxis("GuardX"), 0);
        if (euler.x > 45 && euler.x < 180) {
            euler.x = 45;
        } else if (euler.x < 315 && euler.x > 180) {
            euler.x = 315;
        }
        transform.rotation = Quaternion.Euler(euler.x, euler.y, 0);

        if (Input.GetButton("GuardInteract") != interacting) {
            interacting = Input.GetButton("GuardInteract");
            if (interacting) {

                Vector3 origin = gameObject.transform.position;
                Vector3 direction = gameObject.transform.forward;
                RaycastHit hitInfo;
                float maxDistance = 100f;
                if (Physics.Raycast(origin, direction, out hitInfo, maxDistance)) {
                      Pressable target = hitInfo.collider.gameObject.GetComponent<Pressable>();
                      if (target != null) {
                           target.Press();
                      }
                }
            }
        }
    }

    public void SetTurnSpeed(float speed) { turnSpeed = speed; }
}
