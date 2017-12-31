local snId = 1

local function send(command, data)
    local _message = {
        content = data,
        cmd = command,
        sn = snId,
    }
    local _data = protobuf.encode('CMessage', _message)
    if ET.ETClient:Get():send(_data) then
        snId = snId + 1
    end
end

function recvCallBack(data)
    local _data = protobuf.decode('CMessage', data)
    local _content = protobuf.decode('CRegistration_Req', _data.content)
    print(_data.sn, _data.cmd)
    print(_content.account, _content.password)
end

return {
    send = send,
}
