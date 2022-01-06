local cls_BTreeDecOnce= classV2("BTreeDecOnce","BTreeDecNode")
---@class BTreeDecOnce:BTreeDecNode

function cls_BTreeDecOnce.__initialize(self)
    self._node_name = "BTreeDecOnce"
    self.count = 0
    self._nodeStack = {}
end

function cls_BTreeDecOnce.Tick(self,deltaTime)
    if self.count >= 1 then
        return BTreeDef.STATUS_SUCCESS
    end

    if #self._nodeStack == 0 then
        Log.Error("BTreeDecRepeatNode Check Error,no child node exist!")
        return false
    end

    local node = self._nodeStack[1]
    local ret = node:Tick(deltaTime)
    BTreeLog.LogNodeTickRet(node,ret)
    if ret == BTreeDef.STATUS_SUCCESS then
        self.count = self.count+1
    end

    return ret
end

function cls_BTreeDecOnce.__reset(self)
    self.count = 0
end

return cls_BTreeDecOnce