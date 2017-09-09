using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET {
    public class UGUIEventListen : MonoBehaviour, IPointerClickHandler {
        public Action onClick;

        public void OnPointerClick(PointerEventData eventData) {
            if (onClick != null) {
                onClick();
            }
        }

        public static UGUIEventListen Get(GameObject go) {
            var _sp = go.GetComponent<UGUIEventListen>();
            if (_sp == null) {
                _sp = go.AddComponent<UGUIEventListen>();
            }
            return _sp;
        }
    }
}