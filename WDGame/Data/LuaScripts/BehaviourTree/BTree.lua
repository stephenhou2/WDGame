---@class BTree
BTree = {}
BTree.__index = BTree

function BTree:Init(rootNode,treeName)
    self.treeName = treeName
    if rootNode ~= nil then
        self.rootNode = rootNode
        self.scope = Scope.CreateBTreeScope(treeName)
        rootNode:SetScope(self.scope)
        self.active = false
    end
end

function BTree:ResetTree()
    Log.Log("ResetTree treeName:" ..tostring(self.treeName))
    local rootNode = self.rootNode
    if rootNode ~= nil then
        rootNode:ResetNode()
    end
    
    local scope = self.scope
    if scope ~= nil then
        scope:Clear()
    end

    self.active = false
end

function BTree:IsActive()
    return self.active
end

function BTree:ActiveTree(context)
    Log.Log("ActiveTree treeName:" ..tostring(self.treeName))
    local rootNode = self.rootNode
    if rootNode ~= nil then
        rootNode:SetContext(context)
        rootNode:ActiveNode()
    end
    self.active = true
end

function BTree:Update(deltaTime)
    local rootNode = self.rootNode
    if rootNode ~= nil then
        return rootNode:Tick(deltaTime)
    end

    Log.Error("BTree Update Error,root node is nil! remove tree,name:" ..tostring(self.treeName))
    return BTreeDef.STATUS_FAILURE
end


function BTree.CreateBTree(rootNode,treeName)
    local obj = setmetatable({},BTree)
    obj:Init(rootNode,treeName)
    return obj
end

return BTree

