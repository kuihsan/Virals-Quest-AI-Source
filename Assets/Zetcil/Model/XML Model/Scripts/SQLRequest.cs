using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TechnomediaLabs;

namespace Zetcil
{
    public class SQLRequest : MonoBehaviour
    {
        public enum CSQLDataType { Native, CodeIgniter }
        public enum CSQLConnect { ByAwake, ByEvent }
        public enum CSQLCommand { ByStatement, ByParameter }
        public enum CSQLCompare { LessThan, Equal, GreaterThan }
        public enum CSQLOrderBy { ASC, DESC }

        [System.Serializable]
        public class CSQLCondition
        {
            public string FieldName;
            public CSQLCompare FieldCompare;
            public VarString FieldValue;
        }

        [Space(10)]
        public bool isEnabled;
        [Header("Main URL Settings")]
        public CSQLConnect RequestType;
        public string MainURL;
        [HideInInspector] public string SubmitURL;

        [Header("SQL Settings")]
        public CSQLDataType SQLDataType;
        public string SQLHost;
        public string SQLUsername;
        public string SQLPassword;
        public string SQLDatabase;

        [Header("Query Settings")]
        public CSQLCommand SQLCommand;
        public string SQLStatement;

        [Header("Table Settings")]
        public string SQLTableName;
        public string[] SQLFieldName;
        [Header("Condition Settings")]
        public bool usingSQLCondition;
        public CSQLCondition[] SQLCondition;
        [Header("Order Settings")]
        public bool usingSQLOrderBy;
        public string[] SQLOrderBy;
        public CSQLOrderBy SQLOrderType;

        [Header("Output Settings")]
        public VarString JSONString;
        public bool PrintDebugConsole;

        // Start is called before the first frame update
        void Start()
        {
            if (RequestType == CSQLConnect.ByAwake)
            {
                StartCoroutine(StartSQLRequest());
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ExecuteSQLRequest()
        {
            StartCoroutine(StartSQLRequest());
        }

        IEnumerator StartSQLRequest()
        {
            SubmitURL = MainURL;
            //===================================================================================== NATIVE PHP
            if (SQLDataType == CSQLDataType.Native)
            {
                SubmitURL += "?host=" + SQLHost + "&username=" + SQLUsername + "&password=" + SQLPassword + "&database=" + SQLDatabase;
                if (SQLCommand == CSQLCommand.ByStatement)
                {
                    string sqlStatement = "&sql=" + SQLStatement;
                    SubmitURL += sqlStatement;
                }
                else if (SQLCommand == CSQLCommand.ByParameter)
                {
                    string sqlField = "";
                    foreach (string temp in SQLFieldName)
                    {
                        sqlField += temp + ",";
                    }
                    sqlField = sqlField.Substring(0, sqlField.Length - 1);

                    string sqlCondition = "";
                    if (usingSQLCondition)
                    {
                        sqlCondition = " WHERE ";
                        foreach (CSQLCondition temp in SQLCondition)
                        {
                            if (temp.FieldCompare == CSQLCompare.Equal)
                            {
                                sqlCondition += temp.FieldName + " = " + temp.FieldValue.CurrentValue + " AND ";
                            }
                            if (temp.FieldCompare == CSQLCompare.LessThan)
                            {
                                sqlCondition += temp.FieldName + " <= " + temp.FieldValue.CurrentValue + " AND ";
                            }
                            if (temp.FieldCompare == CSQLCompare.GreaterThan)
                            {
                                sqlCondition += temp.FieldName + " >= " + temp.FieldValue.CurrentValue + " AND ";
                            }
                        }
                        sqlCondition = sqlCondition.Substring(0, sqlCondition.Length - 5);
                    }

                    string sqlOrder = "";
                    if (usingSQLOrderBy)
                    {
                        sqlOrder = " ORDER BY ";
                        foreach (string temp in SQLOrderBy)
                        {
                            sqlOrder += temp + ",";
                        }
                        sqlOrder = sqlOrder.Substring(0, sqlOrder.Length - 1);

                        if (SQLOrderType == CSQLOrderBy.ASC)
                        {
                            sqlOrder += " ASC";
                        }
                        if (SQLOrderType == CSQLOrderBy.DESC)
                        {
                            sqlOrder += " DESC";
                        }
                    }

                    SQLStatement = "SELECT " + sqlField + " FROM " + SQLTableName + sqlCondition + sqlOrder;
                    //SQLStatement = SQLStatement.Replace("*", "ZET_STAR");

                    string sqlStatement = "&sql=" + SQLStatement;
                    SubmitURL += sqlStatement;

                }
            }
            //===================================================================================== CODE IGNITER
            else if (SQLDataType == CSQLDataType.CodeIgniter)
            {
                if (SQLCommand == CSQLCommand.ByStatement)
                {
                    SQLStatement = SQLStatement.Replace("*", "ZET_STAR");
                    string sqlStatement = "/" + SQLStatement;
                    SubmitURL += sqlStatement;
                }
                else if (SQLCommand == CSQLCommand.ByParameter)
                {
                    string sqlField = "";
                    foreach (string temp in SQLFieldName)
                    {
                        sqlField += temp + ",";
                    }
                    sqlField = sqlField.Substring(0, sqlField.Length - 1);

                    string sqlCondition = "";
                    if (usingSQLCondition)
                    {
                        sqlCondition = " WHERE ";
                        foreach (CSQLCondition temp in SQLCondition)
                        {
                            if (temp.FieldCompare == CSQLCompare.Equal)
                            {
                                sqlCondition += temp.FieldName + " ZET_EQUAL " + temp.FieldValue.CurrentValue + " AND ";
                            }
                            if (temp.FieldCompare == CSQLCompare.LessThan)
                            {
                                sqlCondition += temp.FieldName + " ZET_LESS_THAN " + temp.FieldValue.CurrentValue + " AND ";
                            }
                            if (temp.FieldCompare == CSQLCompare.GreaterThan)
                            {
                                sqlCondition += temp.FieldName + " ZET_GREATER_THAN " + temp.FieldValue.CurrentValue + " AND ";
                            }
                        }
                        sqlCondition = sqlCondition.Substring(0, sqlCondition.Length - 5);
                    }

                    string sqlOrder = "";
                    if (usingSQLOrderBy)
                    {
                        sqlOrder = " ORDER BY ";
                        foreach (string temp in SQLOrderBy)
                        {
                            sqlOrder += temp + ",";
                        }
                        sqlOrder = sqlOrder.Substring(0, sqlOrder.Length - 1);

                        if (SQLOrderType == CSQLOrderBy.ASC)
                        {
                            sqlOrder += " ASC";
                        }
                        if (SQLOrderType == CSQLOrderBy.DESC)
                        {
                            sqlOrder += " DESC";
                        }
                    }

                    SQLStatement = "SELECT " + sqlField + " FROM " + SQLTableName + sqlCondition + sqlOrder;
                    SQLStatement = SQLStatement.Replace("*", "ZET_STAR");

                    string sqlStatement = "/" + SQLStatement;
                    SubmitURL += sqlStatement;

                }
            }

            UnityWebRequest webRequest = UnityWebRequest.Get(SubmitURL);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debugger.Save(webRequest.error);
                if (PrintDebugConsole)
                {
                    Debug.Log(SubmitURL);
                    Debug.Log(webRequest.error);
                }
            }
            else
            {
                JSONString.CurrentValue = webRequest.downloadHandler.text;
                Debugger.Save(webRequest.downloadHandler.text);
                if (PrintDebugConsole)
                {
                    Debug.Log(SubmitURL);
                    Debug.Log(webRequest.downloadHandler.text);
                }
            }
        }
    }
}
