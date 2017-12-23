local function numToAscii(num)
    num = num % 256;
    return string.char(num)
end

local function rightShift(num, shift)
    return math.floor(num/(2^shift));
end

local function int16ToBufStr(num)
    local str = ""
    str = str..numToAscii(rightShift(num, 8))
    str = str..numToAscii(num)
    return str
end

local function send(data)
    --ET.ETClient:Get():send(...)
    local _data = string.pack('=', int16ToBufStr(4 + string.len(data)), int16ToBufStr(2819), data)
    print(4 + string.len(data))
    print(_data)
    ET.ETClient:Get():send(_data)
end


return {
    send = send,
}
