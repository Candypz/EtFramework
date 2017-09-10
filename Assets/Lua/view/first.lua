function start()
    local _go = CS.UnityEngine.GameObject.Find("Image (1)")
    CS.ET.UGUIEventListen.Get(_go.gameObject).onClick = function()
        CS.UnityEngine.GameObject.Destroy(_go.gameObject)
    end
end

function update()

end
