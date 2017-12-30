using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace ET {
    public class ETSocket {
        private Socket m_socket;
        private static ManualResetEvent m_timeoutObject = new ManualResetEvent(false);

        private static void connectCb(IAsyncResult asyncresult) {
            m_timeoutObject.Set();
        }

        private static void recvCb(IAsyncResult asyncresult) {
            var _socket = (Socket)asyncresult.AsyncState;
            var _len = _socket.EndReceive(asyncresult);
            asyncresult.AsyncWaitHandle.Close();
            if (_len > 0) {
                ETReadBuffer.getInstance().read(_len);
            }
            _socket.BeginReceive(ETReadBuffer.getInstance().data, 0, ETMemoryStream.BUFFER_MAX, SocketFlags.None, new AsyncCallback(recvCb), _socket);
        }

        public bool connect(string ipAddress, int port) {
            int _count = 0;
__RETRY:
            reset();
            m_timeoutObject.Reset();

            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, ETMemoryStream.BUFFER_MAX);
            try {
                IPEndPoint _endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                m_socket.BeginConnect(_endPoint, new AsyncCallback(connectCb), m_socket);
                m_socket.BeginReceive(ETReadBuffer.getInstance().data, 0, ETMemoryStream.BUFFER_MAX, SocketFlags.None, new AsyncCallback(recvCb), m_socket);
                if (m_timeoutObject.WaitOne(10000)) {
                    if (isRunning()) {

                    }
                    else {

                    }
                }
                else {
                    reset();
                }
            }catch(Exception ex) {
                Debug.LogError("func send connect" + ex.Message);
                if (_count < 3) {
                    _count += 1;
                    goto __RETRY;
                }
                return false;
            }

            return isRunning();
        }

        public bool send(byte[] data) {
            if (!isRunning()) {
                Debug.LogError("func send socket close");
                return false;
            }
            if(data == null) {
                Debug.LogError("func send data null");
                return false;
            }
            try {
                m_socket.Send(ETWriteBuffer.getInstance().creatWriteBuffer(data), data.Length + 4, SocketFlags.None);
                return true;
            }
            catch (SocketException err) {
                if (err.ErrorCode == 10054 || err.ErrorCode == 10053) {
                    Debug.LogError("func send disable connect");
                }
                else {
                    Debug.LogError("func send error" + err.ErrorCode);
                }
                m_socket.Close();
            }
            return false;
        }

        public void reset() {
            colse();
            m_timeoutObject.Set();
        }

        public void colse() {
            if (isRunning()) {
                try {
                    m_socket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception ex) {
                    Debug.LogError("func send colse" + ex.Message);
                }
            }
            if (m_socket != null) {
                m_socket.Close(0);
            }
            m_socket = null;
        }

        public bool isRunning() {
            return (m_socket != null) && (m_socket.Connected);
        }

        public Socket getSocket() {
            return m_socket;
        }
    }
}