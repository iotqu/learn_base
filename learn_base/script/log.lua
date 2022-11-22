--
-- log.lua
--
-- Copyright (c) 2016 rxi
--
-- This library is free software; you can redistribute it and/or modify it
-- under the terms of the MIT license. See LICENSE for details.
--

local log = { _version = "0.1.0" }

log.use_color = true
log.outfile = nil
log.level = "trace"

local modes = {
    { name = "trace", color = "\27[34m", },
    { name = "debug", color = "\27[36m", },
    { name = "info", color = "\27[32m", },
    { name = "warn", color = "\27[33m", },
    { name = "error", color = "\27[31m", },
    { name = "fatal", color = "\27[35m", },
}

local levels = {}
for i, v in ipairs(modes) do
    levels[v.name] = i
end

local round = function(x, increment)
    increment = increment or 1
    x = x / increment
    return (x > 0 and math.floor(x + .5) or math.ceil(x - .5)) * increment
end

local _tostring = tostring

local tostring = function(...)
    local t = {}
    for i = 1, select('#', ...) do
        local x = select(i, ...)
        if type(x) == "number" then
            x = round(x, .01)
        end
        t[#t + 1] = _tostring(x)
    end
    return table.concat(t, " ")
end

for i, x in ipairs(modes) do
    local name_upper = x.name
    log[x.name] = function(...)

        -- Return early if we're below the log level
        if i < levels[log.level] then
            return
        end

        local date = os.date("%Y/%m/%d %H:%M:%S");
        local msg = tostring(...)
        local info = debug.getinfo(2, "Sl")
        local line = info.short_src .. ":" .. info.currentline

        -- Output to console
        print(string.format("%s %s %s[%-5s]%s %s",
                date, line,
                log.use_color and x.color or "",
                x.name,
                log.use_color and "\27[0m" or "",
                msg))

        -- Output to log file

        if log.outfile then
            local outfile = log.outfile .. '.' .. os.date("%Y-%m-%d") .. '.txt'
            local file = io.open(outfile, "a")
            local str = string.format("%s %s [%-5s] %s\n",
                    date, line, name_upper, msg)
            file:write(str)
            file:flush()
            file:close()
        end

    end
end

return log
