using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIDragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isEnabled;

    [Header("Drag Setting")]
    public string DragID = "DragID";
    public string DropID = "DropID";

    [Header("Canvas Setting")]
    public RectTransform UIDragElement;
    public RectTransform UICanvas;

    [Header("True Events")]
    public bool usingTrueEvent;
    public bool SnapPosition;
    public bool DisableAfterSnap;
    public UnityEvent DropTrueEvent;

    [Header("False Events")]
    public bool usingFalseEvent;
    public bool ResetPosition;
    public UnityEvent DropFalseEvent;
    bool RepeatSnap = false;
    bool Cooldown = false;
    Collider2D TargetCollider;

    private Vector2 mOriginalLocalPointerPosition;
    private Vector3 mOriginalPanelLocalPosition;
    private Vector2 mOriginalPosition;

    private void Start()
    {
        mOriginalPosition = UIDragElement.localPosition;
    }

    void Update()
    {
        if (RepeatSnap)
        {
            UIDragElement.GetComponent<RectTransform>().position = TargetCollider.GetComponent<RectTransform>().position;
            if (DisableAfterSnap)
            {
                isEnabled = false;
            }
            if (!Cooldown)
            {
                Invoke("StopSnap", 1);
                Cooldown = true;
            }
        }
    }

    void StopSnap()
    {
        Cooldown = false;
        RepeatSnap = false;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        if (isEnabled)
        {
            mOriginalPanelLocalPosition = UIDragElement.localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
              UICanvas,
              data.position,
              data.pressEventCamera,
              out mOriginalLocalPointerPosition);
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (isEnabled)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
              UICanvas,
              data.position,
              data.pressEventCamera,
              out localPointerPosition))
            {
                Vector3 offsetToOriginal =
                  localPointerPosition -
                  mOriginalLocalPointerPosition;

                UIDragElement.localPosition =
                  mOriginalPanelLocalPosition +
                  offsetToOriginal;
            }
        }

        //ClampToArea();
    }

    public IEnumerator Coroutine_MoveUIElement(
      RectTransform r,
      Vector2 targetPosition,
      float duration = 0.1f)
    {
        float elapsedTime = 0;
        Vector2 startingPos = r.localPosition;
        while (elapsedTime < duration)
        {
            r.localPosition =
              Vector2.Lerp(
                startingPos,
                targetPosition,
                (elapsedTime / duration));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        r.localPosition = targetPosition;
    }

    // Clamp panel to dragArea
    private void ClampToArea()
    {
        Vector3 pos = UIDragElement.localPosition;

        Vector3 minPosition =
          UICanvas.rect.min -
          UIDragElement.rect.min;

        Vector3 maxPosition =
          UICanvas.rect.max -
          UIDragElement.rect.max;

        pos.x = Mathf.Clamp(
          UIDragElement.localPosition.x,
          minPosition.x,
          maxPosition.x);

        pos.y = Mathf.Clamp(
          UIDragElement.localPosition.y,
          minPosition.y,
          maxPosition.y);

        UIDragElement.localPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isEnabled)
        {
            /*
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(
              Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                Vector3 worldPoint = hit.point;
            }
            */
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (DropID == collider.name)
        {
            if (usingTrueEvent)
            {
                if (SnapPosition)
                {
                    TargetCollider = collider;
                    RepeatSnap = true;
                }
                DropTrueEvent.Invoke();
            }
        }
        else
        {
            if (usingFalseEvent)
            {
                if (ResetPosition)
                {
                    StartCoroutine(Coroutine_MoveUIElement(UIDragElement,mOriginalPosition,0.5f));
                }
                DropFalseEvent.Invoke();
            }
        }
    }

}