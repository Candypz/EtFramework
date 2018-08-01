local EtSocket = require "module.EtSocket"

local view = {}

function view:start()
    EtSocket.Connect()
    ET.UGUIEventListen.Get(self.gameObject).onClick = function()
        --while (true) do
        --    local _a = 1
        --end
        local _a = 1
        _a = _a + 1
        -- local registration = {
        --     account = 12,
        --     password = "aaaa"
        -- }
        -- local encode = protobuf.encode('CRegistration_Req', registration)
        -- EtSocket.send(1, encode)
    end
end

function view:update()

end

return view
