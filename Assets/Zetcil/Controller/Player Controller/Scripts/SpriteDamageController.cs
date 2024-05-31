/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk deteksi damage pada/ke karakter menggunakan Keyboard
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;
using System;

namespace Zetcil
{

    public class SpriteDamageController : MonoBehaviour
    {
        public enum CSFXType { Instantiate, Activate }
        public enum CTranslate { VectorUp, VectorDown, VectorForward, VectorBack, VectorLeft, VectorRight }
        public enum CActionType { ActionAnimation, ActionEvent }
        public enum CShutdownType { ShutdownAnimation, ShutdownEvent, ShutdownMessage }
        public enum CShutdownMessage { None, BroadcastShutdownMessage }

        public enum CDamageType { DamageSender, DamageReceiver, AIDamageReceiver }
        public enum COperator { AddValue, SetValue, SubValue }

        [Space(10)]
        public bool isEnabled;
        public CDamageType DamagerType;
        public SpriteDamageController CurrentSpriteDamageController;
        public Rigidbody2D DamagerRigidbody;

        [System.Serializable]
        public class CSFX
        {
            public CSFXType SFXType;
            public GameObject TargetSFX;

            [Header("Hidden Settings")]
            public bool usingHiddenSettings;
            public float HiddenTime;

            [Header("Destroy Settings")]
            public bool usingDestroySettings;
            public float DestroyTime;
        }

        [System.Serializable]
        public class CDamageSender
        {
            [Header("This Sender Settings")]
            [Tag] public string SenderTag;
            public COperator SenderOperator;
            public float SenderDamageValue;

            [Header("That Receiver Settings")]
            [Tag] public List<string> DamageReceiverTag;

            [Header("Transform Settings")]
            public bool usingTransformSetting;
            public CTranslate SenderTranslateType;
            public Vector3 SenderRotation;
            public float SenderSpeed;

            [Header("Sound Settings")]
            public bool usingStartSound;
            [ConditionalField("usingStartSound")] public AudioSource AudioSFX;
            [ConditionalField("usingStartSound")] public AudioClip StartSound;
            public bool usingInBetweenSound;
            [ConditionalField("usingInBetweenSound")] public AudioClip InBetweenSound;
            public bool usingFinishSound;
            [ConditionalField("usingFinishSound")] public AudioClip FinishSound;

            [Header("SFX Settings")]
            public bool usingStartSFX;
            [ConditionalField("usingStartSFX")] public CSFX StartSFX;
            public bool usingInBetweenSFX;
            [ConditionalField("usingInBetweenSFX")] public CSFX InBetweenSFX;
            public bool usingFinishSFX;
            [ConditionalField("usingFinishSFX")] public CSFX FinishSFX;
            [Space(10)]

            [Header("Destroy Settings")]
            public bool DestroyAfterCollision;
        }

        [System.Serializable]
        public class CDamageReceiver
        {
            [Header("This Receiver Settings")]
            [Tag] public string ReceiverTag;

            [Header("Health Settings")]
            public bool usingVarHealth;
            [Tag] public List<string> HealthSenderTag;
            [ConditionalField("usingVarHealth")] public List<VarHealth> VarHealthReceiver;

            [Header("Mana Settings")]
            public bool usingVarMana;
            [Tag] public List<string> ManaSenderTag;
            [ConditionalField("usingVarMana")] public List<VarMana> VarManaReceiver;

            [Header("Exp Settings")]
            public bool usingVarExp;
            [Tag] public List<string> ExpSenderTag;
            [ConditionalField("usingVarExp")] public List<VarExp> VarExpReceiver;

            [Header("Score Settings")]
            public bool usingVarScore;
            [Tag] public List<string> ScoreSenderTag;
            [ConditionalField("usingVarScore")] public List<VarScore> VarScoreReceiver;

            [Header("Time Settings")]
            public bool usingVarTime;
            [Tag] public List<string> TimeSenderTag;
            [ConditionalField("usingVarTime")] public List<VarTime> VarTimeReceiver;

            [Header("Integer Settings")]
            public bool usingVarInteger;
            [Tag] public List<string> IntegerSenderTag;
            [ConditionalField("usingVarInteger")] public List<VarInteger> VarIntegerReceiver;

            [Header("Float Settings")]
            public bool usingVarFloat;
            [Tag] public List<string> FloatSenderTag;
            [ConditionalField("usingVarFloat")] public List<VarFloat> VarFloatReceiver;

            [Header("Event Settings")]
            public bool usingEventSettings;
            public UnityEvent EventSettings;
        }

        [Header("Damage Sender Settings")]
        public CDamageSender DamageSender;

        [Header("Damage Receiver Settings")]
        public CDamageReceiver DamageReceiver;

        [Header("Action Settings")]
        public bool usingAction;
        public CActionType ActionType;
        public Animator ActionAnimator;
        public string ActionAnimationName;
        public UnityEvent ActionEventGroup;

        [Header("Debug Value")]
        [ReadOnly] public string TriggerTag;
        [ReadOnly] public COperator TriggerOperator;
        [ReadOnly] public float TriggerReceived;

        // Use this for initialization
        void Start()
        {
            if (DamageSender.usingStartSound)
            {
                DamageSender.AudioSFX.clip = DamageSender.StartSound;
                DamageSender.AudioSFX.Play();
            }
            //-- START SFX
            if (DamageSender.StartSFX.SFXType == CSFXType.Instantiate)
            {
                ExecuteActivateSFX();
            }
            if (DamageSender.StartSFX.SFXType == CSFXType.Instantiate)
            {
                ExecuteInstantiateSFX();
            }
            //-- INBETWEEN SFX
            if (DamageSender.InBetweenSFX.SFXType == CSFXType.Activate)
            {
                ExecuteActivateSFX();
            }
            if (DamageSender.InBetweenSFX.SFXType == CSFXType.Instantiate)
            {
                ExecuteInstantiateSFX();
            }
            //-- FINISH SFX
            if (DamageSender.FinishSFX.SFXType == CSFXType.Activate)
            {
                ExecuteActivateSFX();
            }
            if (DamageSender.FinishSFX.SFXType == CSFXType.Instantiate)
            {
                ExecuteInstantiateSFX();
            }
        }

        void HiddenMe()
        {
            if (DamageSender.usingStartSFX)
            {
                DamageSender.StartSFX.TargetSFX.SetActive(false);
            }
            if (DamageSender.usingInBetweenSFX)
            {
                DamageSender.InBetweenSFX.TargetSFX.SetActive(false);
            }
        }

        public void ExecuteActivateSFX()
        {
            if (DamageSender.StartSFX.SFXType == CSFXType.Activate)
            {
                if (DamageSender.usingStartSFX)
                {
                    DamageSender.StartSFX.TargetSFX.SetActive(true);
                    if (DamageSender.StartSFX.usingHiddenSettings)
                    {
                        Invoke("HiddenMe", DamageSender.StartSFX.HiddenTime);
                    }
                    if (DamageSender.StartSFX.usingDestroySettings)
                    {
                        Destroy(DamageSender.StartSFX.TargetSFX, DamageSender.StartSFX.DestroyTime);
                    }
                }
            }
            if (DamageSender.InBetweenSFX.SFXType == CSFXType.Activate)
            {
                if (DamageSender.usingInBetweenSFX)
                {
                    DamageSender.InBetweenSFX.TargetSFX.SetActive(true);
                    if (DamageSender.InBetweenSFX.usingHiddenSettings)
                    {
                        Invoke("HiddenMe", DamageSender.InBetweenSFX.HiddenTime);
                    }
                    if (DamageSender.InBetweenSFX.usingDestroySettings)
                    {
                        Destroy(DamageSender.InBetweenSFX.TargetSFX, DamageSender.InBetweenSFX.DestroyTime);
                    }
                }
            }
            if (DamageSender.FinishSFX.SFXType == CSFXType.Activate)
            {
                if (DamageSender.usingFinishSFX)
                {
                    DamageSender.FinishSFX.TargetSFX.SetActive(true);
                    if (DamageSender.FinishSFX.usingHiddenSettings)
                    {
                        Invoke("HiddenMe", DamageSender.FinishSFX.HiddenTime);
                    }
                    if (DamageSender.FinishSFX.usingDestroySettings)
                    {
                        Destroy(DamageSender.FinishSFX.TargetSFX, DamageSender.FinishSFX.DestroyTime);
                    }
                }
            }
        }

        public void ExecuteInstantiateSFX()
        {
            if (DamageSender.usingFinishSFX)
            {
                GameObject temp = GameObject.Instantiate(DamageSender.StartSFX.TargetSFX, CurrentSpriteDamageController.transform.position, CurrentSpriteDamageController.transform.rotation);
                temp.SetActive(true);
                if (DamageSender.StartSFX.usingDestroySettings)
                {
                    Destroy(temp, DamageSender.StartSFX.DestroyTime);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            //************************************************************************************** DAMAGE SENDER
            if (DamagerType == CDamageType.DamageSender)
            {
                CurrentSpriteDamageController.gameObject.tag = DamageSender.SenderTag;

                if (DamageSender.usingTransformSetting)
                {
                    switch (DamageSender.SenderTranslateType)
                    {
                        case CTranslate.VectorUp:
                            CurrentSpriteDamageController.transform.Translate(Vector3.up * DamageSender.SenderSpeed * Time.deltaTime);
                            break;
                        case CTranslate.VectorDown:
                            CurrentSpriteDamageController.transform.Translate(Vector3.down * DamageSender.SenderSpeed * Time.deltaTime);
                            break;
                        case CTranslate.VectorForward:
                            CurrentSpriteDamageController.transform.Translate(Vector3.forward * DamageSender.SenderSpeed * Time.deltaTime);
                            break;
                        case CTranslate.VectorBack:
                            CurrentSpriteDamageController.transform.Translate(Vector3.back * DamageSender.SenderSpeed * Time.deltaTime);
                            break;
                        case CTranslate.VectorLeft:
                            CurrentSpriteDamageController.transform.Translate(Vector3.left * DamageSender.SenderSpeed * Time.deltaTime);
                            break;
                        case CTranslate.VectorRight:
                            CurrentSpriteDamageController.transform.Translate(Vector3.right * DamageSender.SenderSpeed * Time.deltaTime);
                            break;
                    }

                    CurrentSpriteDamageController.transform.Rotate(DamageSender.SenderRotation);
                }

                if (DamageSender.usingInBetweenSound)
                {
                    DamageSender.AudioSFX.clip = DamageSender.InBetweenSound;
                    DamageSender.AudioSFX.Play();
                }
                if (DamageSender.usingInBetweenSFX)
                {
                    DamageSender.InBetweenSFX.TargetSFX.transform.position = CurrentSpriteDamageController.transform.position;
                }

            }
            //********************************************************************************************* DAMAGE RECEIVER
            if (DamagerType == CDamageType.DamageReceiver)
            {
                CurrentSpriteDamageController.gameObject.tag = DamageReceiver.ReceiverTag;

                if (DamageReceiver.usingVarHealth)
                {
                    for (int i = 0; i < DamageReceiver.VarHealthReceiver.Count; i++)
                    {
                        if (DamageReceiver.VarHealthReceiver[i].GetCurrentValue() <= 0)
                        {

                        }
                    }
                }

            }
        }

        public void ExecuteActionAnimation()
        {
            if (usingAction && ActionType == CActionType.ActionAnimation)
            {
                ActionAnimator.Play(ActionAnimationName);
            }
            if (usingAction && ActionType == CActionType.ActionEvent)
            {
                ActionEventGroup.Invoke();
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {

            bool isHit = false;

            if (DamagerType == CDamageType.DamageSender) // check receiver
            {
                isHit = false;

                for (int i = 0; i < DamageSender.DamageReceiverTag.Count; i++)
                {
                    if (DamageSender.DamageReceiverTag[i] == collider.gameObject.tag)
                    {
                        isHit = true;
                        if (collider.gameObject.HasComponent<SpriteDamageController>())
                        {
                            TriggerReceived = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderDamageValue;
                            TriggerOperator = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderOperator;
                            TriggerTag = collider.gameObject.tag;
                        }
                    }
                }

                if (isHit)
                {
                    if (DamageSender.usingFinishSound)
                    {
                        DamageSender.AudioSFX.clip = DamageSender.FinishSound;
                        DamageSender.AudioSFX.Play();
                    }

                    if (DamageSender.usingFinishSFX)
                    {
                        if (DamageSender.FinishSFX.SFXType == CSFXType.Instantiate)
                        {
                            GameObject temp = GameObject.Instantiate(DamageSender.FinishSFX.TargetSFX, CurrentSpriteDamageController.transform.position, CurrentSpriteDamageController.transform.rotation);
                            if (DamageSender.FinishSFX.usingDestroySettings)
                            {
                                Destroy(temp, DamageSender.FinishSFX.DestroyTime);
                            }
                        }
                    }

                    if (DamageSender.DestroyAfterCollision)
                    {
                        if (CurrentSpriteDamageController.gameObject.HasComponent<Renderer>())
                        {
                            CurrentSpriteDamageController.GetComponent<Renderer>().enabled = false;
                            Destroy(this.gameObject);
                        }
                    }
                    else
                    {
                        //Destroy(this.gameObject, 5);
                    }
                }
            }

            if (DamagerType == CDamageType.DamageReceiver || DamagerType == CDamageType.AIDamageReceiver) // check sender
            {
                isHit = false;

                //******************************************************************************* HEALTH RECEIVER
                if (DamageReceiver.usingVarHealth)
                {
                    for (int x = 0; x < DamageReceiver.HealthSenderTag.Count; x++)
                    {
                        if (DamageReceiver.HealthSenderTag[x] == collider.gameObject.tag)
                        {
                            isHit = true;
                            if (collider.gameObject.HasComponent<SpriteDamageController>())
                            {
                                TriggerReceived = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderDamageValue;
                                TriggerOperator = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderOperator;
                                TriggerTag = collider.gameObject.tag;
                            }
                        }
                        if (isHit)
                        {
                            for (int j = 0; j < DamageReceiver.VarHealthReceiver.Count; j++)
                            {
                                if (TriggerOperator == COperator.AddValue)
                                {
                                    DamageReceiver.VarHealthReceiver[x].AddToCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SetValue)
                                {
                                    DamageReceiver.VarHealthReceiver[x].SetCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SubValue)
                                {
                                    DamageReceiver.VarHealthReceiver[x].SubFromCurrentValue(TriggerReceived);
                                }
                            }
                        }
                    }
                }
                //******************************************************************************* MANA RECEIVER
                if (DamageReceiver.usingVarMana)
                {
                    for (int x = 0; x < DamageReceiver.ManaSenderTag.Count; x++)
                    {
                        if (DamageReceiver.ManaSenderTag[x] == collider.gameObject.tag)
                        {
                            isHit = true;
                            if (collider.gameObject.HasComponent<SpriteDamageController>())
                            {
                                TriggerReceived = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderDamageValue;
                                TriggerOperator = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderOperator;
                                TriggerTag = collider.gameObject.tag;
                            }
                        }
                        if (isHit)
                        {
                            for (int j = 0; j < DamageReceiver.VarManaReceiver.Count; j++)
                            {
                                if (TriggerOperator == COperator.AddValue)
                                {
                                    DamageReceiver.VarManaReceiver[x].AddToCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SetValue)
                                {
                                    DamageReceiver.VarManaReceiver[x].SetCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SubValue)
                                {
                                    DamageReceiver.VarManaReceiver[x].SubtractFromCurrentValue(TriggerReceived);
                                }
                            }
                        }
                    }
                }
                //******************************************************************************* EXP RECEIVER
                if (DamageReceiver.usingVarExp)
                {
                    for (int x = 0; x < DamageReceiver.ExpSenderTag.Count; x++)
                    {
                        if (DamageReceiver.ExpSenderTag[x] == collider.gameObject.tag)
                        {
                            isHit = true;
                            if (collider.gameObject.HasComponent<SpriteDamageController>())
                            {
                                TriggerReceived = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderDamageValue;
                                TriggerOperator = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderOperator;
                                TriggerTag = collider.gameObject.tag;
                            }
                        }
                        if (isHit)
                        {
                            for (int j = 0; j < DamageReceiver.VarExpReceiver.Count; j++)
                            {
                                if (TriggerOperator == COperator.AddValue)
                                {
                                    DamageReceiver.VarExpReceiver[x].AddToCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SetValue)
                                {
                                    DamageReceiver.VarExpReceiver[x].SetCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SubValue)
                                {
                                    DamageReceiver.VarExpReceiver[x].SubtractFromCurrentValue(TriggerReceived);
                                }
                            }
                        }
                    }
                }
                //******************************************************************************* TIME RECEIVER
                if (DamageReceiver.usingVarTime)
                {
                    for (int x = 0; x < DamageReceiver.TimeSenderTag.Count; x++)
                    {
                        if (DamageReceiver.TimeSenderTag[x] == collider.gameObject.tag)
                        {
                            isHit = true;
                            if (collider.gameObject.HasComponent<SpriteDamageController>())
                            {
                                TriggerReceived = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderDamageValue;
                                TriggerOperator = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderOperator;
                                TriggerTag = collider.gameObject.tag;
                            }
                        }
                        if (isHit)
                        {
                            for (int j = 0; j < DamageReceiver.VarTimeReceiver.Count; j++)
                            {
                                if (TriggerOperator == COperator.AddValue)
                                {
                                    DamageReceiver.VarTimeReceiver[x].AddToCurrentValue(Mathf.RoundToInt(TriggerReceived));
                                }
                                if (TriggerOperator == COperator.SetValue)
                                {
                                    DamageReceiver.VarTimeReceiver[x].SetCurrentValue(Mathf.RoundToInt(TriggerReceived));
                                }
                                if (TriggerOperator == COperator.SubValue)
                                {
                                    DamageReceiver.VarTimeReceiver[x].SubFromCurrentValue(Mathf.RoundToInt(TriggerReceived));
                                }
                            }
                        }
                    }
                }
                //******************************************************************************* SCORE RECEIVER
                if (DamageReceiver.usingVarScore)
                {
                    for (int x = 0; x < DamageReceiver.ScoreSenderTag.Count; x++)
                    {
                        if (DamageReceiver.ScoreSenderTag[x] == collider.gameObject.tag)
                        {
                            isHit = true;
                            if (collider.gameObject.HasComponent<SpriteDamageController>())
                            {
                                TriggerReceived = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderDamageValue;
                                TriggerOperator = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderOperator;
                                TriggerTag = collider.gameObject.tag;
                            }
                        }
                        if (isHit)
                        {
                            for (int j = 0; j < DamageReceiver.VarScoreReceiver.Count; j++)
                            {
                                if (TriggerOperator == COperator.AddValue)
                                {
                                    DamageReceiver.VarScoreReceiver[x].AddToCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SetValue)
                                {
                                    DamageReceiver.VarScoreReceiver[x].SetCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SubValue)
                                {
                                    DamageReceiver.VarScoreReceiver[x].SubtractFromCurrentValue(TriggerReceived);
                                }
                            }
                        }
                    }
                }
                //******************************************************************************* INTEGER RECEIVER
                if (DamageReceiver.usingVarInteger)
                {
                    for (int x = 0; x < DamageReceiver.IntegerSenderTag.Count; x++)
                    {
                        if (DamageReceiver.IntegerSenderTag[x] == collider.gameObject.tag)
                        {
                            isHit = true;
                            if (collider.gameObject.HasComponent<SpriteDamageController>())
                            {
                                TriggerReceived = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderDamageValue;
                                TriggerOperator = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderOperator;
                                TriggerTag = collider.gameObject.tag;
                            }
                        }
                        if (isHit)
                        {
                            for (int j = 0; j < DamageReceiver.VarIntegerReceiver.Count; j++)
                            {
                                if (TriggerOperator == COperator.AddValue)
                                {
                                    DamageReceiver.VarIntegerReceiver[x].AddToCurrentValue(Mathf.RoundToInt(TriggerReceived));
                                }
                                if (TriggerOperator == COperator.SetValue)
                                {
                                    DamageReceiver.VarIntegerReceiver[x].SetCurrentValue(Mathf.RoundToInt(TriggerReceived));
                                }
                                if (TriggerOperator == COperator.SubValue)
                                {
                                    DamageReceiver.VarIntegerReceiver[x].SubtractFromCurrentValue(Mathf.RoundToInt(TriggerReceived));
                                }
                            }
                        }
                    }
                }
                //******************************************************************************* FLOAT RECEIVER
                if (DamageReceiver.usingVarFloat)
                {
                    for (int x = 0; x < DamageReceiver.FloatSenderTag.Count; x++)
                    {
                        if (DamageReceiver.FloatSenderTag[x] == collider.gameObject.tag)
                        {
                            isHit = true;
                            if (collider.gameObject.HasComponent<SpriteDamageController>())
                            {
                                TriggerReceived = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderDamageValue;
                                TriggerOperator = collider.gameObject.GetComponent<SpriteDamageController>().DamageSender.SenderOperator;
                                TriggerTag = collider.gameObject.tag;
                            }
                        }
                        if (isHit)
                        {
                            for (int j = 0; j < DamageReceiver.VarFloatReceiver.Count; j++)
                            {
                                if (TriggerOperator == COperator.AddValue)
                                {
                                    DamageReceiver.VarFloatReceiver[x].AddToCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SetValue)
                                {
                                    DamageReceiver.VarFloatReceiver[x].SetCurrentValue(TriggerReceived);
                                }
                                if (TriggerOperator == COperator.SubValue)
                                {
                                    DamageReceiver.VarFloatReceiver[x].SubFromCurrentValue(TriggerReceived);
                                }
                            }
                        }
                    }
                }


                if (DamageReceiver.usingEventSettings)
                {
                    DamageReceiver.EventSettings.Invoke();
                }

                ExecuteActionAnimation();
            }
        }

    }

}
