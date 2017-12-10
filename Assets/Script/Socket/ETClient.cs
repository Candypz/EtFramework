using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET {
    public class ETClient : MonoBehaviour {
        public string m_ipAddress = "123.206.221.95";
        public int m_port = 12820;

        private ETSocket m_socket = null;

        private void Awake() {
            m_socket = new ETSocket();

            StartCoroutine(CheckConnectState());

        }

        IEnumerator CheckConnectState() {
            while (true) {
                bool _conSuc = false;
                int _count = 1;
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
                    byte[] _a = { (byte)_count };
                    m_socket.send(_a);
                    _count += 1;
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
    }
}