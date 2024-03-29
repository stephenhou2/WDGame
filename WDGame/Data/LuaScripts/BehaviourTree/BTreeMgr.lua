﻿---@class BTreeMgr
BTreeMgr = {}
BTreeMgr.__index = BTreeMgr

function BTreeMgr:Init()
    self.allBTrees = {}
    self.toResetTrees = {}
    self.tickScope = Scope.CreateLogic("BehaviourTree")
    self.enableUpdate = false
    RegisterUpdater(self,"BTreeUpdate")
end

function BTreeMgr:RefreshUpdateEnable()
    local activeTreeCnt = 0
    for _,tree in pairs(self.allBTrees) do
        if tree:IsActive() == true then
            activeTreeCnt = activeTreeCnt + 1
        end
    end
    self.enableUpdate = (activeTreeCnt > 0)
end

--- 测试用 打印树行结构
local function CreateMap(node)
    local nodeInfo = {}
    nodeInfo.clsName = node.__name
    local subNodes = {}
    if node:IsEmpty() == false then
        for k,v in ipairs(node._nodeStack) do
            table.insert(subNodes,CreateMap(v))
        end
    end
    nodeInfo.subNodes = subNodes
    nodeInfo.nodeType = node._type
    return nodeInfo
end

function BTreeMgr:ActiveTree(treeName,context)
    local tree = self.allBTrees[treeName]
    if tree == nil then
        Log.Error("ActiveTree Failed,行为树未注册,treeName:" ..tostring(treeName))
        return
    end

    tree:ActiveTree(context)
    self:RefreshUpdateEnable()
end

function BTreeMgr:AddTree(rootNode,treeName)
    if rootNode == nil then
        Log.Error("BTreeMgr Add nil Tree!")
        return
    end

    if type(treeName) ~= "string" then
        Log.Error("BTreeMgr Add Tree Failed,Tree Name Is Not String!")
        return
    end

    if not rootNode._isBTreeNode then
        Log.Error("BTreeMgr Add Tree Failed,Tree Root Node Is Not BTree Node!")
        return
    end

    if self.allBTrees[treeName] ~= nil then
        Log.Error("BTreeMgr Add Tree Failed,There Is Already A Tree Named " ..tostring(treeName))
        return
    end

    ---@type BTree
    local tree = BTree.CreateBTree(rootNode,treeName)
    self.allBTrees[treeName] = tree
    self.enableUpdate = true
    Log.Log("AddTree = " ..tostring(treeName))
    -- Log.Error("-------------------------" ..vardump(CreateMap(tree.rootNode)))
end

function BTreeMgr:RemoveTree(treeName)
    Log.Log("RemoveTree = " ..tostring(treeName))

    self:ResetTree(treeName)

    self.allBTrees[treeName] = nil
end 

function BTreeMgr:ResetTree(treeName)
    local tree = self.allBTrees[treeName]
    if tree == nil then
        return
    end

    tree:ResetTree()
    self:RefreshUpdateEnable()
end 

function BTreeMgr:Tick(deltaTime)
    for name,tree in pairs(self.allBTrees) do
        if tree:IsActive() == true then
            local ret = tree:Update(deltaTime)
            if ret == BTreeDef.STATUS_EXIT then
                self.toResetTrees[name] = true
            end
        end
    end 

    for name,rmv in pairs(self.toResetTrees) do
        if rmv then
            self:ResetTree(name)
        end
    end

    self.toResetTrees = {}
end


function BTreeMgr:Update(deltaTime)
    if not self.enableUpdate then
        return
    end
    self:Tick(deltaTime)
end


function BTreeMgr.CreateBTreeMgr()
    local obj = setmetatable({},BTreeMgr)
    obj:Init()
    return obj
end

return BTreeMgr

