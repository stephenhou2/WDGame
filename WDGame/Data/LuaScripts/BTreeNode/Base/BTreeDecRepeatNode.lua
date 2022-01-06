local cls_BTreeDecRepeatNode= classV2("BTreeDecRepeatNode","BTreeDecNode")
---@class BTreeDecRepeatNode:BTreeDecNode

function cls_BTreeDecRepeatNode.__initialize(self,targetRepeatCount)
    self._node_name = "BTreeDecRepeatNode"
    self.targetRepeatCount = targetRepeatCount
    self.count = 0
    self._nodeStack = {}
end

function cls_BTreeDecRepeatNode.Tick(self,deltaTime)
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
    
    if self.targetRepeatCount == 0 then
        return BTreeDef.STATUS_RUNNING
    end

    if self.count >= self.targetRepeatCount then
        return BTreeDef.STATUS_SUCCESS
    end

    return ret
end

function cls_BTreeDecRepeatNode.__reset(self)
    self.count = 0
end

return cls_BTreeDecRepeatNode