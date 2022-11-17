---@diagnostic disable: undefined-global

-- Success
function Success()
    print("======> success")
end
-- Failed
function Failed(error)
    print("======> failed:", error)
end

-- Actions
Actions = function(sn, data)
    result = sysLib:Split(data, ',')
    print(result[1])
    return result[1], data
end

Action = function(sn, data)

    msg = sysLib:Split(data, ',')
    result = sysLib:DataToMqtt(sn, msg)
    return result
end


