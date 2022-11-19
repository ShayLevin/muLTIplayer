using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MoveCapsule : MonoBehaviourPunCallbacks
{
    public CharacterController CapsulePlayer;
    public float speed = 6f;
    public float rotateSpeed = 60f;
    public float gravity = 20f;
    public Vector3 moveDirection = Vector3.zero;
    Vector3 jumpDirection = Vector2.zero;
    public float jumpSpeed = 10f;
    public Animator anim;
    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;
    public GameObject bullet;
    public GameObject GunPos;

    public Vector3 moveJoystick = Vector3.zero;
    public joyStick joystick;

    private bool isJumping = false;


    void mobileMove()
    {
        float vertical;
        float horizontal;
        if (joystick.direction.z != 0)
        {
            vertical = joystick.direction.z * speed * Time.deltaTime;
            moveJoystick = new Vector3(0, 0, vertical);
            moveJoystick = transform.TransformDirection(moveJoystick);
        }
        else
        {
            moveJoystick = Vector3.zero;
        }
        if (joystick.direction.x != 0)
        {
            horizontal = joystick.direction.x * rotateSpeed * Time.deltaTime;
            this.transform.Rotate(0, horizontal, 0);
        }
        if (moveDirection.z != 0 || moveJoystick.z != 0)
        {
            anim.SetInteger("move", 1);
        }
        else
        {
            anim.SetInteger("move", 0);
        }
    }
    public void jump()
    {
        Debug.Log("jump");
        if (!isJumping)
        {
            isJumping = true;
            Debug.Log("Beginning jump");
            jumpDirection.y = jumpSpeed;
            anim.SetInteger("move", 3);
        }
    }
    public void Fire()
    {
        GameObject obj = PhotonNetwork.Instantiate(bullet.name, GunPos.transform.position,
        Quaternion.identity);
        obj.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * -1000);
        obj.name = "bullet";
        Destroy(obj, 5);
    }
    public void quitgame()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gun2.SetActive(false);
        gun3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Shooting

        mobileMove();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject obj = PhotonNetwork.Instantiate(bullet.name, GunPos.transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * -1000);
            obj.name = "bullet";

            Destroy(obj, 5);

        }



        //Switch Guns with Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (gun1.activeSelf == true)
            {
                gun1.SetActive(false);
                gun2.SetActive(true);
                gun3.SetActive(false);
            }
            else if (gun2.activeSelf == true)
            {
                gun1.SetActive(false);
                gun2.SetActive(false);
                gun3.SetActive(true);

            }
            else if (gun3.activeSelf == true)
            {
                gun1.SetActive(true);
                gun2.SetActive(false);
                gun3.SetActive(false);

            }
        }

        // movement ------

        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        moveDirection = new Vector3(0, 0, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        this.transform.Rotate(0, horizontal, 0);



        //animations
        if (moveDirection.z != 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                anim.SetInteger("move", 2);
                speed = 10f;
            }
            else
            {
                anim.SetInteger("move", 1);
                speed = 6f;
            }
        }
        else
        {
            anim.SetInteger("move", 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jump();
        }

        CapsulePlayer.Move(moveDirection 
            + jumpDirection * Time.deltaTime 
            + moveJoystick);

        if (!CapsulePlayer.isGrounded)
        {
            jumpDirection.y -= gravity * Time.deltaTime;
        } else {
            isJumping = false;
            jumpDirection = Vector3.zero;
            anim.SetInteger("move", 0);
        }
    }

}
