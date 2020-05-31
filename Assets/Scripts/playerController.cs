using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed;

    public float gravityMultiplier;

    public float jumpPower;

    public float runSpeed = 12f;

    public CharacterController characterCon;

    private Vector3 moveInput;

    public Transform camTransform;

    public float mouseSensitivity;

    public bool invertX;

    public bool invertY;

    private bool canJump;

    private bool canDoubleJump;


    public Transform groundCheckPoint;

    public LayerMask whatIsGround;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        // moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // store y velocity
        float yStore = moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horzMove = transform.right * Input.GetAxis("Horizontal");


        // moveInput = vertMove * moveSpeed * Time.deltaTime;
        moveInput = horzMove + vertMove;
        moveInput.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput = moveInput * moveSpeed;

        }


        moveInput.y = yStore;

        moveInput.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

        if (characterCon.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        canJump = characterCon.isGrounded || Physics.OverlapSphere(groundCheckPoint.position, .75f, whatIsGround).Length > 0;

        if (canJump)
        {
            canDoubleJump = false;

        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveInput.y = jumpPower;
            canDoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            moveInput.y = jumpPower;
            canDoubleJump = false;

        }
        characterCon.Move(moveInput * Time.deltaTime);

        // controll camera rotation
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        camTransform.rotation = Quaternion.Euler(camTransform.rotation.eulerAngles - new Vector3(mouseInput.y, 0f, 0f));

        anim.SetFloat("moveSpeed", moveInput.magnitude);
    }
}
