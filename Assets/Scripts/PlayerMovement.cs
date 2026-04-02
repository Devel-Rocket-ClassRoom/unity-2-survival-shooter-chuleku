using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static readonly int HashMove = Animator.StringToHash("Move");
    public float moveSpeed = 5f;

    private Animator playerAnimator;
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    public Gun gun;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();


    }

    private void Update()
    {
        if(playerInput.Fire)
        {
            gun.Fire();
        }
        float moveAmount = Mathf.Max(Mathf.Abs(playerInput.Move), Mathf.Abs(playerInput.Rotate));
        playerAnimator.SetFloat(HashMove, moveAmount);
        Rotate();
    }
    private void FixedUpdate()
    {
        Vector3 moveDirection = (Vector3.forward * playerInput.Move) + (Vector3.right * playerInput.Rotate);
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        Vector3 delta = moveDirection * moveSpeed * Time.fixedDeltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + delta);
    }

    private void Rotate()
    {
        Ray _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(plane.Raycast(_cameraRay, out rayLength))
        {
            Vector3 pointToLook = _cameraRay.GetPoint(rayLength);
            Debug.DrawLine(_cameraRay.origin, pointToLook, Color.red);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
