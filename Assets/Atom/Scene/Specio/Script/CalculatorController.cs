using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    public class CalculatorController : MonoBehaviour
    {
        public InputField PrimaryField;
        public InputField SecondaryField;
        public VarString TargetString;

        public void Add0()
        {
            PrimaryField.text += "0";
            SecondaryField.text += "0";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddPlus()
        {
            PrimaryField.text += "+";
            SecondaryField.text += "+";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddMinus()
        {
            PrimaryField.text += "-";
            SecondaryField.text += "-";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddCross()
        {
            PrimaryField.text += "x";
            SecondaryField.text += "x";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddDivide()
        {
            PrimaryField.text += "/";
            SecondaryField.text += "/";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddLessThan()
        {
            PrimaryField.text += "<";
            SecondaryField.text += "<";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddBiggerThan()
        {
            PrimaryField.text += ">";
            SecondaryField.text += ">";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddRaw()
        {
            PrimaryField.text += "#";
            SecondaryField.text += "#";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddPercent()
        {
            PrimaryField.text += "%";
            SecondaryField.text += "%";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddEqual()
        {
            PrimaryField.text += "=";
            SecondaryField.text += "=";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddAt()
        {
            PrimaryField.text += "@";
            SecondaryField.text += "@";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddAnd()
        {
            PrimaryField.text += "&";
            SecondaryField.text += "&";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void AddString(string aCharacter)
        {
            PrimaryField.text += aCharacter;
            SecondaryField.text += aCharacter;
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void SetString(string aCharacter)
        {
            PrimaryField.text = aCharacter;
            SecondaryField.text = aCharacter;
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void SetNotEqual()
        {
            PrimaryField.text = "\u2260";
            SecondaryField.text = "\u2260";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Add1()
        {
            PrimaryField.text += "1";
            SecondaryField.text += "1";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Add2()
        {
            PrimaryField.text += "2";
            SecondaryField.text += "2";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Add3()
        {
            PrimaryField.text += "3";
            SecondaryField.text += "3";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Add4()
        {
            PrimaryField.text += "4";
            SecondaryField.text += "4";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Add5()
        {
            PrimaryField.text += "5";
            SecondaryField.text += "5";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Add6()
        {
            PrimaryField.text += "6";
            SecondaryField.text += "6";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Add7()
        {
            PrimaryField.text += "7";
            SecondaryField.text += "7";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Add8()
        {
            PrimaryField.text += "8";
            SecondaryField.text += "8";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Add9()
        {
            PrimaryField.text += "9";
            SecondaryField.text += "9";
            TargetString.CurrentValue = PrimaryField.text;
        }

        public void Clear()
        {
            PrimaryField.text = "";
            SecondaryField.text = "";
            TargetString.CurrentValue = PrimaryField.text;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
