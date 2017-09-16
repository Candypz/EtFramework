local view = {}

function view:start()
    CS.ET.UGUIEventListen.Get(self.gameObject).onClick = function()
        CS.UnityEngine.GameObject.Destroy(self.gameObject)
    end
end

function view:update()

end

return view
