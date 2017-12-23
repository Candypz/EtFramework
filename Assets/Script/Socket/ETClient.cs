using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET {
    public class ETClient : MonoBehaviour {
        public string m_ipAddress = "123.206.221.95";
        public int m_port = 12820;

        private static ETSocket m_socket = null;

        private void Awake() {
            m_socket = new ETSocket();

            StartCoroutine(CheckConnectState());

        }

        IEnumerator CheckConnectState() {
            while (true) {
                bool _conSuc = false;
                while (!_conSuc) {
                    if (!m_socket.connect(m_ipAddress, m_port)) {
                        Debug.LogError("connect error");
                    }
                    else {
                        _conSuc = true;
                    }
                    yield return new WaitForSeconds(1);
                }
                while (true) {
                    if (!m_socket.isRunning()) {
                        _conSuc = false;
                        break;
                    }
                    yield return new WaitForSeconds(1);
                }
                yield return new WaitForSeconds(1);
            }
        }

        private void OnDestroy() {
            m_socket.colse();
            
        }

        public static ETSocket Get() {
            return m_socket;
        }
    }
}