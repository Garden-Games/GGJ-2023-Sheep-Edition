using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //For Character Controllers and PlayerInput Actions
    private CharacterController cc;
    private PlayerInput playerInput;
    private InputAction moveController;
    private InputAction dogThrowingController;
    private InputAction yellController;
    private Vector3 MoveDirection = Vector3.zero;

    [Header("Movement Controls")]
    [Range(0.0f, 100.0f)]public float MoveSpeed = 5f;
    [Range(0.0f, 1.0f)] public float RotationSpeed = 0.5f;

    [Header("Dog Stuff")]
    public GameObject DogPrefab;
    [Range(1.0f,10.0f)]public float throwForce = 8f;
    [Range(1.0f,10.0f)]public float longThrowForceMultiplier = 2f;
    //[Range(1.0f, 3.0f)]public float dogSpawnDistance = 2.0f;
    [Range(1.0f, 10.0f)] public float upwardThrowForce = 1.3f;
    public GameObject dogSpawner;
 

    private void OnEnable()
    {
        //Grab the character controller and the player input schema fromt he parent gameObject and activate them
        cc = gameObject.GetComponent<CharacterController>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        playerInput.ActivateInput();

        //Grab the action sets from the player input schema
        moveController = playerInput.actions["Move"];
        dogThrowingController = playerInput.actions["ThrowDog"];
        yellController = playerInput.actions["Yell"];

        //Turn em On
        moveController.Enable();
        dogThrowingController.Enable();
        yellController.Enable();

        // Assign the functions to the specific actions 
        dogThrowingController.started += ThrowDog;
        yellController.started += Yell;


    }

    public void FixedUpdate()
    {
        Move();
        RotateTowardsDirection();
        cc.Move(MoveDirection * MoveSpeed);
    }

    private void Move()
    {
        Vector3 playerInputVector = moveController.ReadValue<Vector2>();
        MoveDirection.x = playerInputVector.x;
        MoveDirection.z = playerInputVector.y;
        MoveDirection.y = 0;
    }

    private void Yell(InputAction.CallbackContext obj)
    {
        print("Yelling");
    }

    private void ThrowDog(InputAction.CallbackContext obj)
    {
        Quaternion playerRotation = gameObject.transform.rotation;
        Vector3 dogSpawingPosition = dogSpawner.transform.position;
        GameObject dog = Instantiate(DogPrefab, dogSpawingPosition, playerRotation);
        Rigidbody dogrb = dog.GetComponent<Rigidbody>();

        Vector3 throwVector = Vector3.forward;
        throwVector.y += upwardThrowForce;
        throwVector.x *= throwForce;
        throwVector.z *= throwForce;
        dogrb.AddRelativeForce(throwVector, ForceMode.Impulse);
    }
    private void RotateTowardsDirection()
    {

        if (MoveDirection.x != 0 || MoveDirection.z != 0)
        {
            transform.LookAt(transform.position + MoveDirection);
        }
    }

    private IEnumerable WaitForEnable()
    {
        yield return new WaitForEndOfFrame();
    }

 


}
