---@class ErrorLevel
ErrorLevel = {}
ErrorLevel.Fatal 	= 0
ErrorLevel.Critical = 1
ErrorLevel.Normal 	= 2
ErrorLevel.Hint 	= 3

---@class LogLevel
LogLevel = {}
LogLevel.Critical 	= 0
LogLevel.Normal 	= 1
LogLevel.Hint 		= 2

---@class Log
Log = {}

---@param LogLevel number @日志等级
---@param str string @日志内容
function Log.Log(LogLevel,str)
    CS.LuaLog.Logic(LogLevel,str)
end

---@param ErrorLevel number @日志等级
---@param str string @日志内容
function Log.Error(ErrorLevel,str)
    local output = debug.traceback(str,2)
    CS.LuaLog.Error(ErrorLevel,output)
end

return Log