using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string MoveAxis = "Vertical";
    public static readonly string RotateAxis = "Horizontal";
    public static readonly string FireButton = "Fire1";
    public float Move {  get; private set; }
    public float Rotate {  get; private set; }
    public bool Fire {  get; private set; }
    void Update()
    {
        Move = Input.GetAxis(MoveAxis);
        Rotate = Input.GetAxis(RotateAxis);
        Fire = Input.GetButton(FireButton);

    }
}
