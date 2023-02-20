using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextDataLib.Editor
{
    public class TextDataEditorStorage
    {
        public TextData textData = null;

        public List<string> keysList = null;
        public string curKeyVal;

        public int popupIndex = 0;
        public int contentIndex = 0;

        public TextList contentList = null;

        public void UpdateKeyList() 
        {
            keysList = new List<string>(textData.Data.Keys);
        }

        public void CheckPopupIndex() 
        {
            if (popupIndex < 0
                || popupIndex >= keysList.Count)
                popupIndex = 0;
        }

        public void CheckContentIndex() 
        {
            if (contentIndex >= contentList.Count
                || contentIndex < 0)
                contentIndex = 0;
        }

        public void UpdateContentList() 
        {
            CheckPopupIndex();

            if (keysList.Count > 0)
                contentList = textData.Data[keysList[popupIndex]];
        }

        public void UpdateCurKeyVal() 
        {
            CheckPopupIndex();
            if (keysList.Count > 0)
                curKeyVal = keysList?[popupIndex];
            else
                curKeyVal = string.Empty;
        }

    }

}
