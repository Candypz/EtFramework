local socket = ET.EtSocket()

socket.onConnected = function(from)

end

socket.onError = function(from, err)
    print("@@", err)
end

local function connect()
    socket:init()
    socket:getChannel():Connect()
end

local function send(mess)
    socket:getChannel():Send(mess)
end

return {
    Send = send,
    Connect = connect,
}
