---@class BTreeRegister
if BTreeRegister == nil then
    BTreeRegister = {}
end

local fleetSimpleLeaveHomeTree = 
{
    [1] = {
        clsName = "BTreeCompositeNode",
        nodeType = "Composite",
        subNodes = {
            [1] = {
                clsName = "BTreeCompositeNode",
                nodeType = "Composite",
                subNodes = {
                    [1] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "HomeFleetAddSailor",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [2] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "FleetWarehouseTips",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [3] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "CheckBeforeFleetLeaveHome",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [4] = {
                        clsName = "BTreeExitNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "HomeFleetLeaveHome",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                },
            },
            [2] = {
                clsName = "BTreeExitNode",
                nodeType = "Decorate",
                subNodes = {
                    [1] = {
                        clsName = "BTreeEmptyAtcion",
                        nodeType = "Action",
                        subNodes = {
                        },
                    },
                },
            },
        },
    },
}

--- 选定舰队的派遣树
local dispathTreeMap = 
{
    [1] = {
        clsName = "BTreeCompositeNode",
        nodeType = "Composite",
        subNodes = {
            [1] = {
                clsName = "BTreeCompositeNode",
                nodeType = "Composite",
                subNodes = {
                    [1] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "CheckBeforeDispatch",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [2] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "HomeFleetAddSailor",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [3] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "StrengthProtectTips",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [4] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "FleetWarehouseTips",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [5] = {
                        clsName = "PlayerEnergyTips",
                        nodeType = "Action",
                        subNodes = {
                        },
                    },
                    [6] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "AttackAllianceCampTips",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [7] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "AllianceResourceTips",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [8] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "FleetStatusTips",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [9] = 
                    {
                        clsName = "BTreeSelectNode",
                        nodeType = "Select",
                        subNodes = {
                            [1] = {
                                clsName = "BTreeCompositeNode",
                                nodeType = "Composite",
                                subNodes = {
                                    [1] = {
                                        clsName = "FleetAttackHomeActor",
                                        nodeType = "Action",
                                        subNodes = {
                                        },
                                    },
                                    [2] = {
                                        clsName = "BTreeExitNode",
                                        nodeType = "Decorate",
                                        subNodes = {
                                            [1] = {
                                                clsName = "ExitDispatch",
                                                nodeType = "Action",
                                                subNodes = {
                                                },
                                            },
                                        },
                                    },
                                },
                            },
                            [2] = {
                                clsName = "BTreeCompositeNode",
                                nodeType = "Composite",
                                subNodes = {
                                    [1] = {
                                        clsName = "FleetTaskDispatch",
                                        nodeType = "Action",
                                        subNodes = {
                                        },
                                    },
                                    [2] = {
                                        clsName = "BTreeExitNode",
                                        nodeType = "Decorate",
                                        subNodes = {
                                            [1] = {
                                                clsName = "ExitDispatch",
                                                nodeType = "Action",
                                                subNodes = {
                                                },
                                            },
                                        },
                                    }
                                },
                            },
                            [3] = {
                                clsName = "BTreeCompositeNode",
                                nodeType = "Composite",
                                subNodes = {
                                    [1] = {
                                        clsName = "FleetAINav",
                                        nodeType = "Action",
                                        subNodes = {
                                        },
                                    },
                                    [2] = {
                                        clsName = "BTreeExitNode",
                                        nodeType = "Decorate",
                                        subNodes = {
                                            [1] = {
                                                clsName = "ExitDispatch",
                                                nodeType = "Action",
                                                subNodes = {
                                                },
                                            },
                                        },
                                    }
                                },
                            },
                            [4] = {
                                clsName = "BTreeCompositeNode",
                                nodeType = "Composite",
                                subNodes = {
                                    [1] = {
                                        clsName = "CheckFleetInHome",
                                        nodeType = "Action",
                                        subNodes = {
                                        },
                                    },
                                    [2] = {
                                        clsName = "BTreeCompositeNode",
                                        nodeType = "Composite",
                                        subNodes = {
                                            [1] = {
                                                clsName = "CheckBeforeFleetLeaveHome",
                                                nodeType = "Action",
                                                subNodes = {
                                                },
                                            },
                                            [2] = {
                                                clsName = "BTreeCompositeNode",
                                                nodeType = "Composite",
                                                subNodes = {
                                                    [1] = {
                                                        clsName = "HomeFleetLeaveHome",
                                                        nodeType = "Action",
                                                        subNodes = {
                                                        },
                                                    },
                                                    [2] = {
                                                        clsName = "FleetDispatch",
                                                        nodeType = "Action",
                                                        subNodes = {
                                                        },
                                                    },
                                                    [3] = {
                                                        clsName = "BTreeExitNode",
                                                        nodeType = "Decorate",
                                                        subNodes = {
                                                            [1] = {
                                                                clsName = "ExitDispatch",
                                                                nodeType = "Action",
                                                                subNodes = {
                                                                },
                                                            },
                                                        },
                                                    },
                                                },
                                            },
                                            [3] = {
                                                clsName = "BTreeExitNode",
                                                nodeType = "Decorate",
                                                subNodes = {
                                                    [1] = {
                                                        clsName = "ExitDispatch",
                                                        nodeType = "Action",
                                                        subNodes = {
                                                        },
                                                    },
                                                },
                                            },
                                        },
                                    },
                                    [3] = {
                                        clsName = "BTreeExitNode",
                                        nodeType = "Decorate",
                                        subNodes = {
                                            [1] = {
                                                clsName = "ExitDispatch",
                                                nodeType = "Action",
                                                subNodes = {
                                                },
                                            },
                                        },
                                    },
                                },
                            },
                            [5] = {
                                clsName = "BTreeCompositeNode",
                                nodeType = "Composite",
                                subNodes = {
                                    [1] = {
                                        clsName = "BTreeDecInvertNode",
                                        nodeType = "Decorate",
                                        subNodes = {
                                            [1] = {
                                                clsName = "CheckFleetInHome",
                                                nodeType = "Action",
                                                subNodes = {
                                                },
                                            },
                                        },
                                    },
                                    [2] = {
                                        clsName = "BTreeDecOnceNode",
                                        nodeType = "Decorate",
                                        subNodes = {
                                            [1] = {
                                                clsName = "FleetDispatch",
                                                nodeType = "Action",
                                                subNodes = {
                                                },
                                            },
                                        },
                                    },
                                    [3] = {
                                        clsName = "BTreeExitNode",
                                        nodeType = "Decorate",
                                        subNodes = {
                                            [1] = {
                                                clsName = "ExitDispatch",
                                                nodeType = "Action",
                                                subNodes = {
                                                },
                                            },
                                        },
                                    },
                                },
                            },
                        },
                    },
                },
            },
            [2] = {
                clsName = "BTreeExitNode",
                nodeType = "Decorate",
                subNodes = {
                    [1] = {
                        clsName = "ExitDispatch",
                        nodeType = "Action",
                        subNodes = {
                        },
                    },
                },
            },
        },
    },
    [2] = {
        clsName = "BTreeExitNode",
        nodeType = "Decorate",
        subNodes = {
            [1] = {
                clsName = "ExitDispatch",
                nodeType = "Action",
                subNodes = {
                },
            },
        },
    },
}

--- 需要走派遣流程的派遣树
local selectFleetAndDispatchTreeMap = 
{
    [1] = {
        clsName = "BTreeCompositeNode",
        nodeType = "Composite",
        subNodes = {
            [1] = {
                clsName = "BTreeSelectNode",
                nodeType = "Select",
                subNodes = {
                    [1] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "NoDispatchTarget",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [2] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "GetRecommendFleetFail",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [3] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "CheckNewPlayerProtect",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [4] = {
                        clsName = "BTreeDecOnceNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "NewPlayerProtectTips",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [5] = {
                        clsName = "BTreeCompositeNode",
                        nodeType = "Composite",
                        subNodes = {
                            [1] = {
                                clsName = "BTreeDecOnceNode",
                                nodeType = "Decorate",
                                subNodes = {
                                    [1] = {
                                        clsName = "SetDispatchTarget",
                                        nodeType = "Action",
                                        subNodes = {
                                        },
                                    },
                                },
                            },
                            [2] = {
                                clsName = "BTreeCompositeNode",
                                nodeType = "Composite",
                                subNodes = {
                                    [1] = {
                                        clsName = "WaitSelectFleetDispatch",
                                        nodeType = "Action",
                                        subNodes = {
                                        },
                                    },
                                    [2] = dispathTreeMap[1],
                                },
                            },
                            [3] = {
                                clsName = "BTreeExitNode",
                                nodeType = "Decorate",
                                subNodes = {
                                    [1] = {
                                        clsName = "ExitDispatch",
                                        nodeType = "Action",
                                        subNodes = {
                                        },
                                    },
                                },
                            },
                        },
                    },
                    [6] = {
                        clsName = "BTreeExitNode",
                        nodeType = "Decorate",
                        subNodes = {
                            [1] = {
                                clsName = "ExitDispatch",
                                nodeType = "Action",
                                subNodes = {
                                },
                            },
                        },
                    },
                },
            },
            [2] = {
                clsName = "BTreeExitNode",
                nodeType = "Decorate",
                subNodes = {
                    [1] = {
                        clsName = "ExitDispatch",
                        nodeType = "Action",
                        subNodes = {
                        },
                    },
                },
            },
        },
    },
}



local function DecodeTreeMap(map,parentNode)
    for _,nodeInfo in ipairs(map) do
        local clsName = nodeInfo.clsName
        if type(clsName) ~= "string" then
            Log.Error("DecodeTreeMap Error,map:" ..vardump(map))
        end
        local node = _G[clsName].New()
        if parentNode ~= nil then
            parentNode:PushToStack(node)
        end

        DecodeTreeMap(nodeInfo.subNodes,node)
    end
end

function BTreeRegister.RegistFleetDispatchTree()
    local rootNode1 = BTreeSelectNode.New()
    DecodeTreeMap(selectFleetAndDispatchTreeMap,rootNode1)
    DataCenter:GetBTreeMgr():AddTree(rootNode1,"SelectFleetAndDispatchTree") -- 选择舰队+派遣 的行为树
    
    local rootNode2 = BTreeSelectNode.New()
    DecodeTreeMap(dispathTreeMap,rootNode2)
    DataCenter:GetBTreeMgr():AddTree(rootNode2,"DispatchTree") -- 已有目标舰队的派遣行为树

    local rootNode3 = BTreeSelectNode.New()
    DecodeTreeMap(fleetSimpleLeaveHomeTree,rootNode3)
    DataCenter:GetBTreeMgr():AddTree(rootNode3,"FleetSimpleLeaveHome") -- 舰队直接出征行为树

end



return BTreeRegister