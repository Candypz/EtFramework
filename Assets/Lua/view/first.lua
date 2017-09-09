local view = {}

function view:init()
    print("111")
end

function start()
    view:init()
    CS.ET.UGUIEventListen.Get(self.gameObject).onClick = function()
        print("logging")
    end
end

function update()

end
