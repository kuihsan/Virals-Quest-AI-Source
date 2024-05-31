using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class CardController : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        public Camera TargetCamera;

        [System.Serializable]
        public class CCardSettings
        {
            public string GroupID;

            [Header("Front Card Settings")]
            [Tag] public string FrontCardTag;
            public GameObject FrontCard;

            [Header("Back Card Settings")]
            [Tag] public string BackCardTag;
            public GameObject BackCard;

            [Header("Animation Settings")]
            public bool usingFlipAnimation;
            public float FlipSpeed;

            [Header("Card Status")]
            [ReadOnly] public bool CurrentFace;
            [ReadOnly] public bool StartAnimation;
        }

        [Header("Card Settings")]
        public List<CCardSettings> CardCollection;

        [Header("True Events")]
        public bool usingTrueCompareEvent;
        public UnityEvent TrueCompareEvent;

        [Header("False Events")]
        public bool usingFalseCompareEvent;
        public UnityEvent FalseCompareEvent;

        [Header("Finish Events")]
        public bool usingFinishCompareEvent;
        public UnityEvent FinishCompareEvent;

        [Header("Compare Settings")]
        [ReadOnly] public GameObject FirstCard;
        [ReadOnly] public GameObject SecondCard;
        [ReadOnly] public bool CompareStatus;

        [Header("Selected Value Settings")]
        [ReadOnly] public int SelectedObjectType = 0;
        [ReadOnly] public string SelectedObjectTag;
        [ReadOnly] public string SelectedObjectName;
        [ReadOnly] public int CurrentIndex;

        // Use this for initialization
        void Start()
        {
            CurrentIndex = -1;
            InvokeRepeating("IsMechanicFinished", 1, 1);
        }

        // Update is called once per frame
        void Update()
        {
            CastingRay();
            FlipCardAnimation();
        }

        void ClearCurrentIndex()
        {
            if (CardCollection[CurrentIndex].CurrentFace)
            {
                CardCollection[CurrentIndex].FrontCard.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (!CardCollection[CurrentIndex].CurrentFace)
            {
                CardCollection[CurrentIndex].FrontCard.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            CurrentIndex = -1;
        }

        public void CloseAllCard()
        {
            for (int i = 0; i < CardCollection.Count; i++)
            {
                CardCollection[i].FrontCard.transform.eulerAngles = new Vector3(0, 180, 0);
                CardCollection[i].CurrentFace = false;
            }
        }

        public void FlipCardAnimation()
        {
            if (CurrentIndex != -1)
            {
                if (CardCollection[CurrentIndex].usingFlipAnimation)
                {
                    if (!CardCollection[CurrentIndex].CurrentFace && CardCollection[CurrentIndex].StartAnimation)
                    {
                        CardCollection[CurrentIndex].FrontCard.transform.eulerAngles =
                            Vector3.Lerp(CardCollection[CurrentIndex].FrontCard.transform.eulerAngles,
                            new Vector3(0, 180, 0),
                            CardCollection[CurrentIndex].FlipSpeed * Time.deltaTime);
                    }
                    else if (CardCollection[CurrentIndex].CurrentFace && CardCollection[CurrentIndex].StartAnimation)
                    {
                        CardCollection[CurrentIndex].FrontCard.transform.eulerAngles =
                        Vector3.Lerp(CardCollection[CurrentIndex].FrontCard.transform.eulerAngles,
                        Vector3.zero,
                        CardCollection[CurrentIndex].FlipSpeed * Time.deltaTime);
                    }

                    if (!CardCollection[CurrentIndex].CurrentFace && Vector3.Distance(CardCollection[CurrentIndex].FrontCard.transform.eulerAngles, new Vector3(0, 180, 0)) < 1)
                    {
                        CardCollection[CurrentIndex].FrontCard.transform.eulerAngles = new Vector3(0, 180, 0);
                        CardCollection[CurrentIndex].StartAnimation = false;
                    }
                    else if (CardCollection[CurrentIndex].CurrentFace && Vector3.Distance(CardCollection[CurrentIndex].FrontCard.transform.eulerAngles, Vector3.zero) < 1)
                    {
                        CardCollection[CurrentIndex].FrontCard.transform.eulerAngles = Vector3.zero;
                        CardCollection[CurrentIndex].StartAnimation = false;
                    }

                }
            }
        }

        public void SetThisObject(GameObject thisGameObject)
        {
            if (FirstCard == null)
            {
                FirstCard = thisGameObject;
            }
            else if (SecondCard == null)
            {
                SecondCard = thisGameObject;
            }
        }

        public bool IsStillAnimation()
        {
            bool result = false;
            for (int i = 0; i < CardCollection.Count; i++)
            {
                if (CardCollection[i].StartAnimation)
                {
                    result = true;
                }
            }
            return result;
        }

        public string GetGroupID(string Cardname)
        {
            string result = "";
            for (int i = 0; i < CardCollection.Count; i++)
            {
                if (CardCollection[i].BackCard.name == Cardname)
                {
                    result = CardCollection[i].GroupID;
                }
            }
            return result;
        }

        public void HideThisObjectByID(string aID)
        {
            for (int i = 0; i < CardCollection.Count; i++)
            {
                if (CardCollection[i].GroupID == aID)
                {
                    CardCollection[i].FrontCard.SetActive(false);
                    CardCollection[i].BackCard.SetActive(false);
                }
            }
        }


        public void IsMechanicFinished()
        {
            bool result = true;
            for (int i = 0; i < CardCollection.Count; i++)
            {
                if (CardCollection[i].FrontCard.activeSelf)
                {
                    result = false;
                }
                else if (CardCollection[i].BackCard.activeSelf)
                {
                    result = false;
                }
            }
            if (result)
            {
                if (usingFinishCompareEvent)
                {
                    FinishCompareEvent.Invoke();
                }
            }
        }


        void Comparation()
        {
            if (FirstCard != null && SecondCard != null)
            {
                string Card1 = GetGroupID(FirstCard.name);
                string Card2 = GetGroupID(SecondCard.name);
                if (Card1 == Card2)
                {
                    CompareStatus = true;
                    if (usingTrueCompareEvent)
                    {
                        TrueCompareEvent.Invoke();

                        HideThisObjectByID(Card1);
                        HideThisObjectByID(Card2);

                        FirstCard = null;
                        SecondCard = null;
                        CompareStatus = false;
                    } else
                    {
                        FirstCard = null;
                        SecondCard = null;
                        CompareStatus = false;
                    }
                }
                else
                {
                    if (usingFalseCompareEvent)
                    {
                        FalseCompareEvent.Invoke();

                        HideThisObjectByID(Card1);
                        HideThisObjectByID(Card2);

                        FirstCard = null;
                        SecondCard = null;
                        CompareStatus = false;
                    }
                    else
                    {
                        FirstCard = null;
                        SecondCard = null;
                        CompareStatus = false;
                        CloseAllCard();
                    }
                }
            }
        }


        public void CastingRay()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                //-- cek tabrakan dengan objeck 2d
                Ray ray = TargetCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D raycastHit2D = Physics2D.GetRayIntersection(ray);

                if (raycastHit2D.collider != null)
                {
                    SelectedObjectName = raycastHit2D.collider.gameObject.name;
                    SelectedObjectTag = raycastHit2D.collider.gameObject.tag;

                    if (FirstCard == null)
                    {
                        FirstCard = raycastHit2D.collider.gameObject;
                    }
                    else if (SecondCard == null)
                    {
                        SecondCard = raycastHit2D.collider.gameObject;
                    }
                }
                else
                {
                    //-- cek tabrakan dengan objeck 3d
                    ray = TargetCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit raycastHit3D;

                    if (Physics.Raycast(ray, out raycastHit3D))
                    {
                        SelectedObjectName = raycastHit3D.collider.gameObject.name;
                        SelectedObjectTag = raycastHit3D.collider.gameObject.tag;

                        if (!IsStillAnimation())
                        {
                            for (int i = 0; i < CardCollection.Count; i++)
                            {
                                if (SelectedObjectTag == CardCollection[i].BackCardTag &&
                                    SelectedObjectName == CardCollection[i].BackCard.name)
                                {
                                    CurrentIndex = i;
                                    CardCollection[CurrentIndex].CurrentFace = true;
                                    if (CardCollection[i].usingFlipAnimation)
                                    {
                                        //-- do animation
                                        CardCollection[i].StartAnimation = true;
                                    }
                                    else
                                    {
                                        CardCollection[i].FrontCard.transform.eulerAngles = new Vector3(0, 0, 0);
                                    }

                                    //-- only if back card click
                                    if (FirstCard == null)
                                    {
                                        FirstCard = raycastHit3D.collider.gameObject;
                                    }
                                    else if (SecondCard == null)
                                    {
                                        SecondCard = raycastHit3D.collider.gameObject;
                                    }

                                }
                                else if (SelectedObjectTag == CardCollection[i].FrontCardTag &&
                                         SelectedObjectName == CardCollection[i].FrontCard.name)
                                {
                                    
                                    /** do nothing 
                                    CurrentIndex = i;
                                    CardCollection[CurrentIndex].CurrentFace = false;
                                    if (CardCollection[i].usingFlipAnimation)
                                    {
                                        //-- do animation
                                        CardCollection[i].StartAnimation = true;
                                    }
                                    else
                                    {
                                        CardCollection[i].FrontCard.transform.eulerAngles = new Vector3(0, 180, 0);
                                    }

                                    //-- only if back card click
                                    if (FirstCard != null)
                                    {
                                        FirstCard = null;
                                    }
                                    else if (SecondCard != null)
                                    {
                                        SecondCard = null;
                                    }
                                    **/
                                }
                            }

                            Invoke("Comparation", 1);
                        }

                    }
                }

            }
        }
    }
}
