msg = sysLib:Split("1,2,3,4,5,6,7,8,9,10,11", ",")
if sysLib:Len(msg) ~= 11 then
    log.warn("data is invalid")
    return true
end
local temp = {
    ver = "v1.0.0",
    data = {
        id = "msg[0]",
        speed = "msg[1]",
        electric = "msg[2]",
        vi = "msg[3]",
        x_load = "msg[4]",
        y_load = "msg[5]",
        tem1 = "msg[6]",
        tem2 = "msg[7]",
        dur = "msg[8]",
        count = "msg[9]",
        date = "msg[10]",
        time = os.date("%Y-%m-%dT%H:%M:%S") .. ".000",
        dataType = 1,
    },
    dt = os.date("%Y-%m-%dT%H:%M:%S") .. ".000"
}
sysLib:DataToMqtt("OUT:110", json.encode(temp))
log.info(json.encode(temp))

