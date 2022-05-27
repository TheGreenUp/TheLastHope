using System.Collections;
using UnityEngine;

public class FirstPersonContoller : MonoBehaviour
{
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool shouldJump => canJump && Input.GetKeyDown(jumpKey) && characterController.isGrounded;
    private bool shouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;
    public bool CanMove { get; private set; } = true;

    #region FunctionalOptions
    [Header("Functional Options")]
    [SerializeField] public bool canSprint = true;
    [SerializeField] public bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadBob = true;
    [SerializeField] private bool willSlideOnSlopes = true;
    //[SerializeField] private bool canZoom = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool useFootSteps = true;
    [SerializeField] private bool enableStaminaBar = true;
    #endregion

    #region Controls
    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    //[SerializeField] private KeyCode zoomKey = KeyCode.Mouse1; //right button
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0; //left button
    #endregion

    #region MovementParametrs
    [Header("Movement Parametrs")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float slopeSpeed = 5f;
    [SerializeField] private float sprintStaminaUsage = 0.1f;
    [SerializeField] private float jumpStaminaUsage = 10f;
    #endregion

    #region LookParametrs
    [Header("Look Parametrs")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;
    #endregion

    #region JumpParametrs
    [Header("Jump Parametrs")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;
    #endregion

    #region CrouchParametrs
    [Header("Crouch Parametrs")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0.0f, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;
    #endregion

    #region HeadBobParametrs
    [Header("Headbob Parametrs")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;
    #endregion

    #region ZoomParametrs
    //[Header("Zoom Parametrs")]
    //[SerializeField] private float timeToZoom = 0.3f;
    //[SerializeField] private float zoomFOV = 30f;
    //private float defaultFOV;
    //private Coroutine zoomRoutine;
    #endregion

    #region FootstepsParametrs

    [Header("Footsteps Parametrs")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private AudioClip[] bedClips = default;
    private float footstepTimer = 0;
    private float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultiplier : isSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;
    #endregion

    #region AnimController

    [Header("Animation controller")]
    [SerializeField] private GameObject dummy;
    private Animator animator;
    #endregion


    //SLIDER PARAMETRS
    private Vector3 hitPointNormal;
    private Vector3 realSlopeDirection;
    private bool isSliding
    {
        get
        {
            // Debug.DrawRay(transform.position, Vector3.down, Color.red);
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2.0f))
            {
                hitPointNormal = slopeHit.normal;
                realSlopeDirection = Vector3.Cross(Vector3.Cross(slopeHit.normal, Vector3.down), slopeHit.normal);
                return Vector3.Angle(realSlopeDirection, Vector3.up) > characterController.slopeLimit;
            }
            else return false;
        }
    }

    #region Interaction
    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactLayer = default;
    private Interactable currentInteractable;
    #endregion



    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.localPosition.y;//for headbob, we want to return original camera position
        //defaultFOV = playerCamera.fieldOfView; //Uncomment for zoom 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = dummy.GetComponent<Animator>();
    }


    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();//ввод передвижения
            HandleMouseLock();//мышкой крутим ци не
            if (canJump)//прыжок
                HandleJump();

            if (canCrouch)
                HandleCrouch();
            if (canUseHeadBob)
                HandleHeadBob();
            //if (canZoom)
            //    HandleZoom();
            if (useFootSteps)
                HandleFootsteps();
            if (canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }

            ApplyFinalMovements(); //подсчитываем что сделали и выполняем передвижение
        }
    }
    //==================MOVEMENT====================
    private void HandleMovementInput()
    {
        float moveStatement = isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : walkSpeed;
        if (isSprinting && !isCrouching)//если бежим, надо проверить, если у персонажа выносливость
        {
            currentInput = new Vector2(sprintSpeed * Input.GetAxis("Vertical"), moveStatement * Input.GetAxis("Horizontal"));//говорим персонажу, что бежим
            if (enableStaminaBar) StaminaBar.instance.UseStamina(sprintStaminaUsage);//отнимаем стамину, переменная в инспекторе
        }
        else
        {
            currentInput = new Vector2(moveStatement * Input.GetAxis("Vertical"), moveStatement * Input.GetAxis("Horizontal"));
        }
        if (currentInput == Vector2.zero)
        {
        }
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)//is character is not on the ground, we fall
            moveDirection.y -= gravity * Time.deltaTime;

        if (willSlideOnSlopes && isSliding)//if we on slope, we must fall
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;

        characterController.Move(moveDirection * Time.deltaTime); //move into direction
    }
    //=================MOUSE ROTATION====================
    private void HandleMouseLock()//Управление мышой
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY; //rotation on x (up & down);
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);// НУЖНО РАЗОБРАТЬСЯ ЧЕ ЗА КЛАМП
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);//хз
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);//хз
    }
    //==================JUMPING========================
    private void HandleJump()//прыжок
    {
        if (shouldJump)
        {
            if (enableStaminaBar) StaminaBar.instance.UseStamina(jumpStaminaUsage);//отнимаем стамину, переменная в инспекторe
            moveDirection.y = jumpForce;
        }
    }
    //================CROUCHING=======================
    private void HandleCrouch()
    {
        if (shouldCrouch)
            StartCoroutine(CrouchStand());
    }
    private IEnumerator CrouchStand()
    {

        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))//чтобы нельзя было встать, сидя под столом
            yield break;

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        //Эти строчки в случае того, если поставим время приседа = 0
        characterController.height = targetHeight;
        characterController.center = targetCenter;
        isCrouching = !isCrouching;
        duringCrouchAnimation = false;
    }
    //=========================HEAD BOB=======================
    private void HandleHeadBob()
    {
        if (!characterController.isGrounded) return;//if we are not on the ground, we should`t bob
        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)//return abs way |move.direction|
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : isSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x, //x pos
                defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : isSprinting ? sprintBobAmount : walkBobAmount),//Y + sin(timer) (синус либо 1, либо -1, поэтому камеру будет качать вверх и вниз)
                playerCamera.transform.localPosition.z);//y pos
        }
    }
    //=================ZOOM===========================
    //private void HandleZoom()
    //{
    //    if (Input.GetKeyDown(zoomKey))//нажать зум
    //    {
    //        if(zoomRoutine != null)
    //        {
    //            StopCoroutine(zoomRoutine);
    //            zoomRoutine = null;
    //        }
    //        zoomRoutine = StartCoroutine(ToggleZoom(true));
    //    }

    //    if (Input.GetKeyUp(zoomKey))//отжать зум
    //    {
    //        if (zoomRoutine != null)
    //        {
    //            StopCoroutine(zoomRoutine);
    //            zoomRoutine = null;
    //        }
    //        zoomRoutine = StartCoroutine(ToggleZoom(false));
    //    }
    //}
    //private IEnumerator ToggleZoom(bool isEnter)
    //{
    //    float targetFOV = isEnter ? zoomFOV : defaultFOV;//needed fov
    //    float startingFOV = playerCamera.fieldOfView;//our fov
    //    float timeElapsed = 0;//time  elapsed

    //    while (timeElapsed < startingFOV)
    //    {
    //        playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed/timeToZoom);
    //        timeElapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    playerCamera.fieldOfView = targetFOV;
    //    zoomRoutine = null;
    //}
    //=========================INTERACTION===============
    private void HandleInteractionCheck()//const raycast look for objects
    {
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer == 6 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))//layer with interactable and check for update interactable objects
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                {
                    currentInteractable.OnFocus();
                }
            }
        }

        //On loose focus
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }
    private void HandleInteractionInput()//hit interact key
    {
        if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactLayer))
        {
            currentInteractable.OnInteract();
        }

    }
    //=======================FOOTSTEPS=======
    private void HandleFootsteps()
    {
        if (!characterController.isGrounded) return;

        if (currentInput == Vector2.zero) return;

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            if (Physics.Raycast(characterController.transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch (hit.collider.tag)
                {
                    case "footsteps/WOOD":
                        footstepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                        break;
                    case "footsteps/GRASS":
                        footstepAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);
                        break;
                    case "footsteps/METAL":
                        footstepAudioSource.PlayOneShot(metalClips[Random.Range(0, metalClips.Length - 1)]);
                        break;
                    case "footsteps/BED":
                        Debug.Log(hit.collider.tag);
                        footstepAudioSource.PlayOneShot(bedClips[Random.Range(0, bedClips.Length - 1)]);
                        break;
                    default:
                        Debug.Log(hit.collider.tag);
                        footstepAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);//если нет никакой поверхности, пускай будет дерево
                        break;
                }
            }

            footstepTimer = GetCurrentOffset;
        }
    }
    //================================
}
