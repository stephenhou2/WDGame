local Loader = {}

function Loader.LoadLuaFile(fileName)
    if type(fileName) == "string" then
        require(fileName)
    else
        Log.Error(ErrorLevel.Fatal,string.format("RegisterLuaFile Error,file name \'%s\' not valid",tostring(v)))
    end
end

return Loader