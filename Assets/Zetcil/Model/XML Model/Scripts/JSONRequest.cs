using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class JSONRequest : MonoBehaviour
    {
        public enum CJSONParsing { None, ByDelay, ByInputKey, ByEvent }

        [Space(10)]
        public bool isEnabled;

        [Header("Input Settings")]
        public VarString StringInput;

        [Header("Parsing Settings")]
        public CJSONParsing JSONParsing;
        public float ParsingDelay;
        [SearchableEnum] public KeyCode ParsingTriggerKey;

        [Header("Output Settings")]
        public VarJSON JSONList;

        // Start is called before the first frame update
        void Start()
        {
            if (JSONParsing == CJSONParsing.ByDelay)
            {
                Invoke("ExecuteJSONParsing", ParsingDelay);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (JSONParsing == CJSONParsing.ByInputKey)
            {
                if (Input.GetKeyDown(ParsingTriggerKey))
                {
                    ExecuteJSONParsing();
                }
            }
        }

        public void ExecuteJSONParsing()
        {
            JSONObject VarJSON = new JSONObject(StringInput.CurrentValue);
            ExtractJSON(VarJSON);
        }

        void ExtractJSON(JSONObject obj)
        {
            if (obj.type == JSONObject.Type.ARRAY)
            {
                foreach (JSONObject json_data in obj.list)
                {
                    JSONList.AddNewJSONRoot();
                    for (int i = 0; i < json_data.list.Count; i++)
                    {
                        string key = (string) json_data.keys[i];
                        string value = (string) json_data.list[i].str;
                        JSONList.AddNewJSONItem(key, value);
                    }
                }

                JSONList.SetDefaultValue();
            }
        }

        void AccessData(JSONObject obj)
        {
            switch (obj.type)
            {
                case JSONObject.Type.OBJECT:
                    for (int i = 0; i < obj.list.Count; i++)
                    {
                        string key = (string)obj.keys[i];
                        JSONObject j = (JSONObject)obj.list[i];
                        Debug.Log(key);
                        AccessData(j);
                    }
                    break;
                case JSONObject.Type.ARRAY:
                    foreach (JSONObject j in obj.list)
                    {
                        AccessData(j);
                    }
                    break;
                case JSONObject.Type.STRING:
                    Debug.Log(obj.str);
                    break;
                case JSONObject.Type.NUMBER:
                    Debug.Log(obj.n);
                    break;
                case JSONObject.Type.BOOL:
                    Debug.Log(obj.b);
                    break;
                case JSONObject.Type.NULL:
                    Debug.Log("NULL");
                    break;

            }
        }
    }
}
