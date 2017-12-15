local view = {}

function view:start()
    self.gameObject:GetComponent("Text").text = "wifi:"..ET.Utility:getSystemState().."\nBatteryLevel"..ET.Utility:getBatteryLevel().."\nBatteryState"..ET.Utility:getBatteryState()
end

function view:update()

end

return view
