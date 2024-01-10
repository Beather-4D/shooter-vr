using UnityEngine;
using UnityEngine.InputSystem;
//Script importé
[RequireComponent(typeof(Animator))]
public class HandScript : MonoBehaviour
{
    [SerializeField] private InputActionReference gripAction;
    [SerializeField] private InputActionReference pinchAction;
    private Animator animator;

    private void OnEnable()
    {
        gripAction.action.performed += Gripping;
        gripAction.action.canceled += GripRelease;

        pinchAction.action.performed += Pinching;
        pinchAction.action.canceled += PinchRelease;
    }

    private void Awake() => animator = GetComponent<Animator>();

    private void Gripping(InputAction.CallbackContext obj) => animator.SetFloat("Grip", obj.ReadValue<float>());

    private void GripRelease(InputAction.CallbackContext obj) => animator.SetFloat("Grip", 0f);

    private void Pinching(InputAction.CallbackContext obj) => animator.SetFloat("Pinch", obj.ReadValue<float>());

    private void PinchRelease(InputAction.CallbackContext obj) => animator.SetFloat("Pinch", 0f);

}
