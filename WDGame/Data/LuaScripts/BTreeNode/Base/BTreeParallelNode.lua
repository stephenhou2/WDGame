local cls_BTreeParallelNode = classV2("BTreeParallelNode","BTreeNode")
---@class BTreeParallelNode:BTreeNode


function cls_BTreeParallelNode.Tick(self,deltaTime)
    local allSucc = true
    for k,v in ipairs(self._nodeStack) do
        local ret = v:Tick(deltaTime)
        if v._type == BTreeDef.BTREE_NODE_ACTION then
            BTreeNode.BTreeLog("BTreeLog Tick Action Node:" ..tostring(v._node_name) ..",ret:" ..tostring(ret))
        end
        if ret ~= BTreeDef.STATUS_SUCCESS then
            allSucc = false
        end
    end
    return allSucc
end

function cls_BTreeParallelNode.__initialize(self,tickType)
    self._type = BTreeDef.BTREE_NODE_PARALLEL
    self._node_name = "BTreeParallelNode"
    self._nodeStack = {}
end

return cls_BTreeParallelNode