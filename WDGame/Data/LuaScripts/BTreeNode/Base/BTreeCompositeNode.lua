local cls_BTreeCompositeNode = classV2("BTreeCompositeNode","BTreeNode")
---@class cls_BTreeCompositeNode:BTreeNode

cls_BTreeCompositeNode._type = BTreeDef.BTREE_NODE_COMPOSITE

function cls_BTreeCompositeNode.__initialize(self,tickType)
    self._node_name = "BTreeCompositeNode"
    self._tick_type = tickType or BTreeDef.BTree_TICK_SEQUENCE
    self._nodeStack = {}
end

local function Tick_Composite(self,deltaTime)
    for k,v in ipairs(self._nodeStack) do
        local ret = v:Tick(deltaTime)
        if v._type == BTreeDef.BTREE_NODE_ACTION then
            BTreeNode.BTreeLog("BTreeLog Tick Action Node:" ..tostring(v._node_name) ..",ret:" ..tostring(ret))
        end
        if ret == BTreeDef.STATUS_RUNNING then
            return BTreeDef.STATUS_RUNNING
        elseif ret == BTreeDef.STATUS_FAILURE then
            return BTreeDef.STATUS_FAILURE
        end
    end
    return BTreeDef.STATUS_SUCCESS
end

function cls_BTreeCompositeNode.Tick(self,deltaTime)
    if self._tick_type == BTreeDef.BTree_TICK_SEQUENCE then
        return Tick_Composite(self,deltaTime)
    elseif self._tick_type == BTreeDef.BTree_TICK_RANDOM then
        self.Shuffle(self)
        return Tick_Composite(self,deltaTime)
    end
    return false
end

return cls_BTreeCompositeNode