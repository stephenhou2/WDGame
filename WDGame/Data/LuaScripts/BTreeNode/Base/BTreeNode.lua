local cls_BTreeNode = classV2("BTreeNode")
---@class BTreeNode


cls_BTreeNode._isBTreeNode = true
cls_BTreeNode._type = BTreeDef.BTREE_TYPE_INVALID
cls_BTreeNode._node_name = "BTreeNode"
cls_BTreeNode._nodeStack = {} --- 子节点堆栈

function cls_BTreeNode.__initialize(self,...)
    self._isBTreeNode = true
    self._node_name = "BTreeNode"
    self._nodeStack = {}
end

function cls_BTreeNode.SetContext(self,context)
    self.Context = context
    for _,node in ipairs(self._nodeStack) do
        node.SetContext(node,context)
    end
end

function cls_BTreeNode.__getContext(self,context)
    return self.Context
end

function cls_BTreeNode.SetScope(self,scope)
    self.nodeScope = scope
    for _,node in ipairs(self._nodeStack) do
        node.SetScope(node,scope)
    end
end

function cls_BTreeNode.__onActive(self)
end

function cls_BTreeNode.ActiveNode(self)
    self.__onActive(self)
    for _,node in ipairs(self._nodeStack) do
        node.ActiveNode(node)
    end
end

--- 推入子节点堆栈
function cls_BTreeNode.PushToStack(self,node)
    if nil == node then
        Log.Error("PushToStack Failed,push nil node")
        return
    end

    if not node._isBTreeNode then
        Log.Error("PushToStack Failed,push invalid type node")
        return
    end

    table.insert(self._nodeStack,node)
    Log.Log("PushToStack Succeed")
    -- Log.Error("stack:" ..vardump(self._nodeStack))
end

function cls_BTreeNode.IsEmpty(self)
    return #self._nodeStack == 0
end

function cls_BTreeNode.__reset(self)
    
end

--- 节点reset
--- 会把子节点也全部reset
function cls_BTreeNode.ResetNode(self)
    self.__reset(self)
    for _,node in ipairs(self._nodeStack) do
        node.ResetNode(node)
    end
end

--- 子节点重新排序
function cls_BTreeNode.Shuffle(self)
    local cnt = #self._nodeStack
    for i=cnt,1,-1 do
        local p = math.random(1,i)
        self._nodeStack[i],self._nodeStack[p] = self._nodeStack[p],self._nodeStack[i]
    end
end

--- 行为树节点tick
--- @param deltaTime number
function cls_BTreeNode.Tick(self,deltaTime)
    Log.Error("not implement Node Tick func,please check! cur node = " ..tostring(self._node_name))
    return BTreeDef.STATUS_FAILURE
end

function cls_BTreeNode.GetNodeType(self)
    return self._type
end

BTreeNode.LogEnable = true
--- 行为树运行日志
function BTreeNode.BTreeLog(str)
    if BTreeNode.LogEnable then
        Util.EditorLogDebug("<color=white>" ..str.."</color>")
    end
end

return cls_BTreeNode