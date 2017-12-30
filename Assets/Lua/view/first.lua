local EtSocket = require "module/EtSocket"

local view = {}

function view:start()
    ET.UGUIEventListen.Get(self.gameObject).onClick = function()
        local registration = {
            account = 12,
            password = "aaaa"
        }
        local encode = protobuf.encode('Registration_Req', registration)
        EtSocket.send(1, encode)
    end
end

function view:update()

end

return view
