local cls_BTreeDecOnceNode= classV2("BTreeDecOnceNode","BTreeDecNode")
---@class BTreeDecOnceNode:BTreeDecNode

function cls_BTreeDecOnceNode.__initialize(self)
    self._node_name = "BTreeDecOnceNode"
    self.count = 0
    self._nodeStack = {}
end

function cls_BTreeDecOnceNode.Tick(self,deltaTime)
    if self.count >= 1 then
        return BTreeDef.STATUS_SUCCESS
    end

    if #self._nodeStack == 0 then
        Log.Error("BTreeDecOnceNode Check Error,no child node exist!")
        return false
    end

    local node = self._nodeStack[1]
    local ret = node:Tick(deltaTime)
    BTreeLog.LogNodeTickRet(node,ret)
    if ret == BTreeDef.STATUS_EXIT then
        return BTreeDef.STATUS_EXIT
    end

    if ret == BTreeDef.STATUS_SUCCESS then
        self.count = self.count+1
    end

    return ret
end

function cls_BTreeDecOnceNode.__reset(self)
    self.count = 0
end

return cls_BTreeDecOnceNode