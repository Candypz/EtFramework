local EtSocket = require "module/EtSocket"

local view = {}

function view:start()
    self.gameObject:GetComponent("Text").text = "wifi:"..ET.Utility:getSystemState().."\nBatteryLevel"..ET.Utility:getBatteryLevel().."\nBatteryState"..ET.Utility:getBatteryState()
    local registration = {
        account = 12,
        password = "aaaa"
    }
    local encode = protobuf.encode('Registration_Req', registration)
    local decode = protobuf.decode('Registration_Req', encode)

    EtSocket.send("111")


    self.gameObject:GetComponent("Text").text = decode.account.."\n"..decode.password
end

function view:update()

end

return view
