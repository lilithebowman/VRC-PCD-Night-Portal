﻿
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace Texel
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public class TranslationManager : UdonSharpBehaviour
    {
        public TranslationTable translationTable;

        public Text[] textTargets;
        public string[] textKeys;
        int[] textIndexes;

        public VRC_Pickup[] pickupTargets;
        public string[] pickupInteractKeys;
        public string[] pickupUseKeys;
        int[] pickupInteractIndexes;
        int[] pickupUseIndexes;

        public GameObject[] behaviorTargets;
        public string[] behaviorInteractKeys;
        int[] behaviorInteractIndexes;

        int selectedLang = 0;

        Component[] handlers;
        int handlerCount = 0;
        string[] handlerEvents;

        void Start()
        {
            textIndexes = new int[textKeys.Length];
            for (int i = 0; i < textKeys.Length; i++)
                textIndexes[i] = _GetIndex(textKeys[i]);

            pickupInteractIndexes = new int[pickupInteractKeys.Length];
            for (int i = 0; i < pickupInteractKeys.Length; i++)
                pickupInteractIndexes[i] = _GetIndex(pickupInteractKeys[i]);

            pickupUseIndexes = new int[pickupUseKeys.Length];
            for (int i = 0; i < pickupUseKeys.Length; i++)
                pickupUseIndexes[i] = _GetIndex(pickupUseKeys[i]);

            behaviorInteractIndexes = new int[behaviorInteractKeys.Length];
            for (int i = 0; i < behaviorInteractKeys.Length; i++)
                behaviorInteractIndexes[i] = _GetIndex(behaviorInteractKeys[i]);

            _SelectLang(0);
        }

        public int SelectedLang
        {
            get { return selectedLang; }
            set
            {
                _SelectLang(value);
            }
        }

        public void _SelectLang(int id)
        {
            if (id < 0 || id >= translationTable.languages.Length)
                return;

            selectedLang = id;

            _ApplyTextTranslations();
            _ApplyPickupTranslations();
            _ApplyBehaviorTranslations();

            _UpdateHandlers();
        }

        public void _SelectLang0()
        {
            _SelectLang(0);
        }

        public void _SelectLang1()
        {
            _SelectLang(1);
        }

        public void _SelectLang2()
        {
            _SelectLang(2);
        }

        int _GetIndex(string key)
        {
            if (!Utilities.IsValid(key))
                return -1;
            if (key.Length == 0)
                return -1;

            for (int i = 0; i < translationTable.keys.Length; i++)
            {
                if (translationTable.keys[i] == key)
                    return i;
            }

            return -1;
        }

        void _ApplyTextTranslations()
        {
            for (int i = 0; i < textTargets.Length; i++)
            {
                Text target = textTargets[i];
                if (!Utilities.IsValid(target))
                    continue;

                int index = _GetIndex(textKeys[i]);
                if (index < 0)
                    continue;

                string value = translationTable._GetValue(selectedLang, index);
                target.text = value;
            }
        }

        void _ApplyPickupTranslations()
        {
            for (int i = 0; i < pickupTargets.Length; i++)
            {
                VRC_Pickup target = pickupTargets[i];
                if (!Utilities.IsValid(target))
                    continue;

                int index = _GetIndex(pickupInteractKeys[i]);
                if (index >= 0)
                {
                    string value = translationTable._GetValue(selectedLang, index);
                    target.InteractionText = value;
                }

                index = _GetIndex(pickupUseKeys[i]);
                if (index >= 0)
                {
                    string value = translationTable._GetValue(selectedLang, index);
                    target.UseText = value;
                }
            }
        }

        void _ApplyBehaviorTranslations()
        {
            for (int i = 0; i < behaviorTargets.Length; i++)
            {
                GameObject targetObj = behaviorTargets[i];
                if (!Utilities.IsValid(targetObj))
                    continue;

                UdonBehaviour target = (UdonBehaviour)targetObj.GetComponent(typeof(UdonBehaviour));
                if (!Utilities.IsValid(target))
                    continue;

                int index = _GetIndex(behaviorInteractKeys[i]);
                if (index < 0)
                    continue;

                string value = translationTable._GetValue(selectedLang, index);
                target.InteractionText = value;
            }
        }

        public void _Regsiter(Component handler, string eventName)
        {
            if (!Utilities.IsValid(handler) || !Utilities.IsValid(eventName))
                return;

            for (int i = 0; i < handlerCount; i++)
            {
                if (handlers[i] == handler)
                    return;
            }

            handlers = (Component[])_AddElement(handlers, handler, typeof(Component));
            handlerEvents = (string[])_AddElement(handlerEvents, eventName, typeof(string));

            handlerCount += 1;
        }

        void _UpdateHandlers()
        {
            for (int i = 0; i < handlerCount; i++)
            {
                UdonBehaviour script = (UdonBehaviour)handlers[i];
                script.SendCustomEvent(handlerEvents[i]);
            }
        }

        Array _AddElement(Array arr, object elem, Type type)
        {
            Array newArr;
            int count = 0;

            if (Utilities.IsValid(arr))
            {
                count = arr.Length;
                newArr = Array.CreateInstance(type, count + 1);
                Array.Copy(arr, newArr, count);
            }
            else
                newArr = Array.CreateInstance(type, 1);

            newArr.SetValue(elem, count);
            return newArr;
        }
    }
}
