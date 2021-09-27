---@class Log
Log = {}

---@param LogLevel number @日志等级
---@param str string @日志内容
function Log.Log(LogLevel,str)
    LuaLog.Logic(LogLevel,str)
end

---@param ErrorLevel number @日志等级
---@param str string @日志内容
function Log.Error(ErrorLevel,str)
    local output = debug.traceback(str,2)
    LuaLog.Error(ErrorLevel,output)
end

return Log