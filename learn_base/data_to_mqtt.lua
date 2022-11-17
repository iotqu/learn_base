Action = function(sn, data)
    msg = sysLib:Split(data, ",")
    sysLib:log(msg)
    sysLib:log(sizeof(msg))
    return true
end