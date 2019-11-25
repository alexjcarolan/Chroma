//=============================================================================
//
// Purpose: Add player ability on SteamVR CameraRig to:
//
// * Trackpad press down: project laser pointer from Touch Controller
// * Trackpad release: teleport player with blink to laser point destination
// * Trigger click: grab any object that has a custom "Grabbable" tag applied
// * Trigger release: release the current grabbed object with relative force
// * Application Menu: reset the position of last grabbed object to controller
//
// Tutorial on usage at: https://youtu.be/6uYaK_T46z0
//
//=============================================================================

using UnityEngine;
using System.Collections;

public class SteamVR_FirstPersonController : MonoBehaviour
{
    public enum AxisType
    {
        XAxis,
        ZAxis
    }

    public Color pointerColor;
    public float pointerThickness = 0.002f;
    public AxisType pointerFacingAxis = AxisType.ZAxis;
    public float pointerLength = 100f;
    public bool showPointerTip = true;
    public bool teleportWithPointer = true;
    public float blinkTransitionSpeed = 0.6f;

    public bool highlightGrabbableObject = true;
    public Color grabObjectHightlightColor;

    private SteamVR_TrackedObject trackedController;
    private SteamVR_Controller.Device device;

    private GameObject pointerHolder;
    private GameObject pointer;
    private GameObject pointerTip;

    private Vector3 pointerTipScale = new Vector3(0.05f, 0.05f, 0.05f);

    private float pointerContactDistance = 0f;
    private Transform pointerContactTarget = null;

    private Rigidbody controllerAttachPoint;
    private FixedJoint controllerAttachJoint;
    private GameObject canGrabObject;
    private Color[] canGrabObjectOriginalColors;
    private GameObject previousGrabbedObject;

    private Transform HeadsetCameraRig;
    private float HeadsetCameraRigInitialYPosition;
    private Vector3 TeleportLocation;

    void Awake()
    {
        trackedController = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        InitController();
        InitPointer();
        InitHeadsetReferencePoint();
    }

    void InitController()
    {
        controllerAttachPoint = transform.GetChild(0).Find("tip").GetChild(0).GetComponent<Rigidbody>();

        BoxCollider collider = this.gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(0.1f, 0.1f, 0.2f);
        collider.isTrigger = true;
    }

    void InitPointer()
    {
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", pointerColor);

        pointerHolder = new GameObject();
        pointerHolder.transform.parent = this.transform;
        pointerHolder.transform.localPosition = Vector3.zero;

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = pointerHolder.transform;
        pointer.GetComponent<MeshRenderer>().material = newMaterial;

        pointer.GetComponent<BoxCollider>().isTrigger = true;
        pointer.AddComponent<Rigidbody>().isKinematic = true;
        pointer.layer = 2;

        pointerTip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        pointerTip.transform.parent = pointerHolder.transform;
        pointerTip.GetComponent<MeshRenderer>().material = newMaterial;
        pointerTip.transform.localScale = pointerTipScale;

        pointerTip.GetComponent<SphereCollider>().isTrigger = true;
        pointerTip.AddComponent<Rigidbody>().isKinematic = true;
        pointerTip.layer = 2;

        SetPointerTransform(pointerLength, pointerThickness);
        TogglePointer(false);
    }

    void InitHeadsetReferencePoint()
    {
        Transform eyeCamera = GameObject.FindObjectOfType<SteamVR_Camera>().GetComponent<Transform>();
        // The referece point for the camera is two levels up from the SteamVR_Camera
        HeadsetCameraRig = eyeCamera.parent.parent;
        HeadsetCameraRigInitialYPosition = HeadsetCameraRig.transform.position.y;
    }

    void SetPointerTransform(float setLength, float setThicknes)
    {
        //if the additional decimal isn't added then the beam position glitches
        float beamPosition = setLength / (2 + 0.00001f);

        if (pointerFacingAxis == AxisType.XAxis)
        {
            pointer.transform.localScale = new Vector3(setLength, setThicknes, setThicknes);
            pointer.transform.localPosition = new Vector3(beamPosition, 0f, 0f);
            pointerTip.transform.localPosition = new Vector3(setLength - (pointerTip.transform.localScale.x / 2), 0f, 0f);
        }
        else
        {
            pointer.transform.localScale = new Vector3(setThicknes, setThicknes, setLength);
            pointer.transform.localPosition = new Vector3(0f, 0f, beamPosition);
            pointerTip.transform.localPosition = new Vector3(0f, 0f, setLength - (pointerTip.transform.localScale.z / 2));
        }

        TeleportLocation = pointerTip.transform.position;
    }

    float GetPointerBeamLength(bool hasRayHit, RaycastHit collidedWith)
    {
        float actualLength = pointerLength;

        //reset if beam not hitting or hitting new target
        if (!hasRayHit || (pointerContactTarget && pointerContactTarget != collidedWith.transform))
        {
            pointerContactDistance = 0f;
            pointerContactTarget = null;
        }

        //check if beam has hit a new target
        if (hasRayHit)
        {
            if (collidedWith.distance <= 0)
            {

            }
            pointerContactDistance = collidedWith.distance;
            pointerContactTarget = collidedWith.transform;
        }

        //adjust beam length if something is blocking it
        if (hasRayHit && pointerContactDistance < pointerLength)
        {
            actualLength = pointerContactDistance;
        }

        return actualLength; ;
    }

    void TogglePointer(bool state)
    {
        pointer.gameObject.SetActive(state);
        bool tipState = (showPointerTip ? state : false);
        pointerTip.gameObject.SetActive(tipState);
    }

    void Teleport()
    {
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, blinkTransitionSpeed);
        HeadsetCameraRig.position = new Vector3(TeleportLocation.x, HeadsetCameraRigInitialYPosition, TeleportLocation.z);
    }

    void UpdatePointer()
    {
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            TogglePointer(true);
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (pointerContactTarget != null && teleportWithPointer)
            {
                Teleport();
            }
            TogglePointer(false);
        }

        if (pointer.gameObject.activeSelf)
        {
            Ray pointerRaycast = new Ray(transform.position, transform.forward);
            RaycastHit pointerCollidedWith;
            bool rayHit = Physics.Raycast(pointerRaycast, out pointerCollidedWith);
            float pointerBeamLength = GetPointerBeamLength(rayHit, pointerCollidedWith);
            SetPointerTransform(pointerBeamLength, pointerThickness);
        }
    }

    void SnapCanGrabObjectToController(GameObject obj)
    {
        obj.transform.position = controllerAttachPoint.transform.position;

        controllerAttachJoint = obj.AddComponent<FixedJoint>();
        controllerAttachJoint.connectedBody = controllerAttachPoint;
        ToggleGrabbableObjectHighlight(false);
    }

    Rigidbody ReleaseGrabbedObjectFromController()
    {
        var jointGameObject = controllerAttachJoint.gameObject;
        var rigidbody = jointGameObject.GetComponent<Rigidbody>();
        Object.DestroyImmediate(controllerAttachJoint);
        controllerAttachJoint = null;

        return rigidbody;
    }

    void ThrowReleasedObject(Rigidbody rb)
    {
        var origin = trackedController.origin ? trackedController.origin : trackedController.transform.parent;
        if (origin != null)
        {
            rb.velocity = origin.TransformVector(device.velocity);
            rb.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            rb.velocity = device.velocity;
            rb.angularVelocity = device.angularVelocity;
        }

        rb.maxAngularVelocity = rb.angularVelocity.magnitude;
    }

    void RecallPreviousGrabbedObject()
    {
        if (previousGrabbedObject != null && device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            previousGrabbedObject.transform.position = controllerAttachPoint.transform.position;
            previousGrabbedObject.transform.rotation = controllerAttachPoint.transform.rotation;
            var rb = previousGrabbedObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.maxAngularVelocity = 0f;
        }
    }

    void UpdateGrabbableObjects()
    {
        if (canGrabObject != null)
        {
            if (controllerAttachJoint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                previousGrabbedObject = canGrabObject;
                SnapCanGrabObjectToController(canGrabObject);
            }
            else if (controllerAttachJoint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Rigidbody releasedObjectRigidBody = ReleaseGrabbedObjectFromController();
                ThrowReleasedObject(releasedObjectRigidBody);
            }
        }
    }

    Renderer[] GetObjectRendererArray(GameObject obj)
    {
        return (obj.GetComponents<Renderer>().Length > 0 ? obj.GetComponents<Renderer>() : obj.GetComponentsInChildren<Renderer>());
    }

    Color[] BuildObjectColorArray(GameObject obj, Color defaultColor)
    {
        Renderer[] rendererArray = GetObjectRendererArray(obj);

        int length = rendererArray.Length;

        Color[] colors = new Color[length];
        for (int i = 0; i < length; i++)
        {
            colors[i] = defaultColor;
        }
        return colors;
    }

    Color[] StoreObjectOriginalColors(GameObject obj)
    {
        Renderer[] rendererArray = GetObjectRendererArray(obj);

        int length = rendererArray.Length;
        Color[] colors = new Color[length];

        for (int i = 0; i < length; i++)
        {
            var renderer = rendererArray[i];
            colors[i] = renderer.material.color;
        }

        return colors;
    }

    void ChangeObjectColor(GameObject obj, Color[] colors)
    {
        Renderer[] rendererArray = GetObjectRendererArray(obj);
        int i = 0;
        foreach (Renderer renderer in rendererArray)
        {
            renderer.material.color = colors[i];
            i++;
        }
    }

    void ToggleGrabbableObjectHighlight(bool highlightObject)
    {
        if (highlightGrabbableObject && canGrabObject != null)
        {
            if (highlightObject)
            {
                var colorArray = BuildObjectColorArray(canGrabObject, grabObjectHightlightColor);
                ChangeObjectColor(canGrabObject, colorArray);
            }
            else
            {
                ChangeObjectColor(canGrabObject, canGrabObjectOriginalColors);
            }
        }
    }

    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedController.index);

        RecallPreviousGrabbedObject();
        UpdateGrabbableObjects();
    }

    void Update()
    {
        UpdatePointer();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Grabbable")
        {
            if (canGrabObject == null)
            {
                canGrabObjectOriginalColors = StoreObjectOriginalColors(collider.gameObject);
            }
            canGrabObject = collider.gameObject;
            ToggleGrabbableObjectHighlight(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Grabbable")
        {
            ToggleGrabbableObjectHighlight(false);
            canGrabObject = null;
        }
    }
}