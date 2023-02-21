Action = function(sn, data)
    msg = sysLib:Split(data, ",")
    if sysLib:Len(msg) ~= 11 then
        log.warn("data is invalid-> " .. data)
        return true
    end
    local temp = {
        ver = "v1.0.0",
        data = {
            id = msg[0],
            speed = msg[1],
            electric = msg[2],
            vi = msg[3],
            x_load = msg[4],
            y_load = msg[5],
            tem1 = msg[6],
            tem2 = msg[7],
            dur = msg[8],
            count = msg[9],
            date = msg[10],
            time = sysLib:Now(),
            dataType = 1,
        },
        dt = sysLib:Now()
    }
    result = sysLib:DataToMqtt(json.encode(temp), sn)
    return result
end