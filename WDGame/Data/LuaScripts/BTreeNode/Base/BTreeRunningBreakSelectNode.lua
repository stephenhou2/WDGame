local cls_BTreeRunningBreakSelectNode = classV2("BTreeRunningBreakSelectNode","BTreeNode")
---@class BTreeRunningBreakSelectNode:BTreeSelectNode

function cls_BTreeRunningBreakSelectNode.__initialize(self,tickType)
    self._tick_type = tickType or BTreeDef.BTree_TICK_SEQUENCE
    self._node_name = "BTreeRunningBreakSelectNode"
    self._nodeStack = {}
end

function cls_BTreeRunningBreakSelectNode.Tick_Select(self,deltaTime)
    local hasRuningChild = false
    for k,v in ipairs(self._nodeStack) do
        local ret = v:Tick(deltaTime)
        BTreeLog.LogNodeTickRet(v,ret)
        if ret == BTreeDef.STATUS_SUCCESS then
            return BTreeDef.STATUS_SUCCESS
        elseif ret == BTreeDef.STATUS_RUNNING then
            return BTreeDef.STATUS_RUNNING
        end
    end

    return BTreeDef.STATUS_FAILURE
end


function cls_BTreeRunningBreakSelectNode.Tick(self,deltaTime)
    if self._tick_type == BTreeDef.BTree_TICK_SEQUENCE then
        return self.Tick_Select(self,deltaTime)
    elseif self._tick_type == BTreeDef.BTree_TICK_RANDOM then
        self.Shuffle(self)
        return self.Tick_Select(self,deltaTime)
    end

    return BTreeDef.STATUS_SUCCESS
end

return cls_BTreeRunningBreakSelectNode