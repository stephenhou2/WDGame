---@class BTreeRegister
if BTreeRegister == nil then
    BTreeRegister = {}
end

local treeMap = 
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
                        clsName = "NoDispatchTarget",
                        nodeType = "ACTION",
                        subNodes = {
                        },
                    },
                    [2] = {
                        clsName = "BTreeDecOnce",
                        subNodes = {
                            [1] = {
                                clsName = "SetDispatchTargetFail",
                                nodeType = "ACTION",
                                subNodes = {
                                },
                            },
                        },
                    },
                    [3] = {
                        clsName = "BTreeCompositeNode",
                        nodeType = "Composite",
                        subNodes = {
                            [1] = {
                                clsName = "BTreeDecOnce",
                                subNodes = {
                                    [1] = {
                                        clsName = "SetDispatchTarget",
                                        nodeType = "ACTION",
                                        subNodes = {
                                        },
                                    },
                                },
                            },
                            [2] = {
                                clsName = "WaitClickScreenOnDispatch",
                                nodeType = "ACTION",
                                subNodes = {
                                },
                            },
                            [3] = {
                                clsName = "BTreeSelectNode",
                                nodeType = "Select",
                                subNodes = {
                                    [1] = {
                                        clsName = "ClickCancelDispatch",
                                        nodeType = "ACTION",
                                        subNodes = {
                                        },
                                    },
                                    [2] = {
                                        clsName = "BTreeCompositeNode",
                                        nodeType = "Composite",
                                        subNodes = {
                                            [1] = {
                                                clsName = "WaitClickFleetDispatch",
                                                nodeType = "ACTION",
                                                subNodes = {
                                                },
                                            },
                                            [2] = {
                                                clsName = "BTreeSelectNode",
                                                nodeType = "Select",
                                                subNodes = {
                                                    [1] = {
                                                        clsName = "CheckBeforeDispatch",
                                                        nodeType = "ACTION",
                                                        subNodes = {
                                                        },
                                                    },
                                                    [2] = {
                                                        clsName = "BTreeSelectNode",
                                                        nodeType = "Select",
                                                        subNodes = {
                                                            [1] = {
                                                                clsName = "BTreeDecOnce",
                                                                subNodes = {
                                                                    [1] = {
                                                                        clsName = "FleetAttackHomeActor",
                                                                        nodeType = "ACTION",
                                                                        subNodes = {
                                                                        },
                                                                    },
                                                                },
                                                            },
                                                            [2] = {
                                                                clsName = "BTreeDecOnce",
                                                                subNodes = {
                                                                    [1] = {
                                                                        clsName = "FleetAttackHomeActor",
                                                                        nodeType = "ACTION",
                                                                        subNodes = {
                                                                        },
                                                                    },
                                                                },
                                                            },
                                                            [3] = {
                                                                clsName = "BTreeDecOnce",
                                                                nodeType = "Decorate",
                                                                subNodes = {
                                                                    [1] = {
                                                                        clsName = "FleetAINav",
                                                                        nodeType = "ACTION",
                                                                        subNodes = {
                                                                        },
                                                                    },
                                                                },
                                                            },
                                                            [4] = {
                                                                clsName = "BTreeDecOnce",
                                                                subNodes = {
                                                                    [1] = {
                                                                        clsName = "BTreeCompositeNode",
                                                                        nodeType = "Composite",
                                                                        subNodes = {
                                                                            [1] = {
                                                                                clsName = "CheckFleetInHome",
                                                                                nodeType = "ACTION",
                                                                                subNodes ={
                                                                                },
                                                                            },
                                                                            [2] = {
                                                                                clsName = "BTreeCompositeNode",
                                                                                nodeType = "Composite",
                                                                                subNodes = {
                                                                                    [1] = {
                                                                                        clsName = "HomeFleetAddSailor",
                                                                                        nodeType = "ACTION",
                                                                                        subNodes = {
                                                                                        },
                                                                                    },
                                                                                    [2] = {
                                                                                        clsName = "HomeFleetLeaveHome",
                                                                                        nodeType = "ACTION",
                                                                                        subNodes = {
                                                                                            [1] = {
                                                                                                clsName = "CheckBeforeFleetLeaveHome",
                                                                                                nodeType = "ACTION",
                                                                                                subNodes = {
                                                                                                },
                                                                                            },
                                                                                            [2] = {
                                                                                                clsName = "BTreeCompositeNode",
                                                                                                nodeType = "Composite",
                                                                                                subNodes = {
                                                                                                    [1] = {
                                                                                                        clsName = "HomeFleetLeaveHome",
                                                                                                        nodeType = "ACTION",
                                                                                                        subNodes = {
                                                                                                        },
                                                                                                    },
                                                                                                    [2] = {
                                                                                                        clsName = "BTreeDecOnce",
                                                                                                        subNodes = {
                                                                                                            [1] = {
                                                                                                                clsName = "FleetDispatch",
                                                                                                                nodeType = "ACTION",
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
                                                                        },
                                                                    },
                                                                },
                                                            },
                                                            [5] = {
                                                                clsName = "BTreeDecOnce",
                                                                nodeType = "Decorate",
                                                                subNodes = {
                                                                    [1] = {
                                                                        clsName = "BTreeCompositeNode",
                                                                        nodeType = "Composite",
                                                                        subNodes = {
                                                                            [1] = {
                                                                                clsName = "BTreeDecInvertNode",
                                                                                nodeType = "Decorate",
                                                                                subNodes = {
                                                                                    [1] = {
                                                                                        clsName = "CheckFleetInHome",
                                                                                        nodeType = "ACTION",
                                                                                        subNodes = {
                                                                                        },
                                                                                    },
                                                                                },
                                                                            },
                                                                            [2] = {
                                                                                clsName = "FleetDispatch",
                                                                                nodeType = "ACTION",
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
                                        },
                                    },
                                },
                            },
                        },
                    },
                },
            },
            [2] = {
                clsName = "ExitDispatch",
                nodeType = "ACTION",
                subNodes = {
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
    -- local rootNode_1 = BTreeCompositeNode.New()

    -- local selectNode_2_1 = BTreeSelectNode.New()
    -- local exitDispatchNode_2_2 = ExitDispatch.New()

    -- local noDispatchTargetNode_3_1 = NoDispatchTarget.New()
    -- local onceNode_3_2 = BTreeDecOnce.New()
    -- local compositeNode_3_3 = BTreeCompositeNode.New()

    -- local setDispatchFailNode_4_1 = SetDispatchTargetFail.New()
    -- local onceNode_4_2 = BTreeDecOnce.New()
    -- local waitClickScreenOnDispatchNode_4_3 = WaitClickScreenOnDispatch.New()
    -- local selectNode_4_4 = BTreeSelectNode.New()

    -- local setDispatchNode_5_1 = SetDispatchTarget.New()
    -- local clickCancelDispatchNode_5_2 = ClickCancelDispatch.New()
    -- local compositeNode__ = BTreeCompositeNode.New()
    -- local waitClickDispatchNode = WaitClickFleetDispatch.New()

    -- local selectNode_5_3 = BTreeSelectNode.New()

    -- local checkBeforeDispatchNode_6_1 = CheckBeforeDispatch.New()
    -- local selectNode_6_2 = BTreeSelectNode.New()
    
    -- local onceNode_7_1 = BTreeDecOnce.New()
    -- local onceNode_7_2 = BTreeDecOnce.New()
    -- local onceNode_7_3 = BTreeDecOnce.New()
    -- local onceNode_7_4 = BTreeDecOnce.New()

    -- local fleetAttackHomeActorNode_8_1 = FleetAttackHomeActor.New()
    -- local fleetTaskDispatchNode_8_2 = FleetAttackHomeActor.New()
    -- local fleetAINavNode_8_3 = FleetAINav.New()
    -- local selectNode_8_4 = BTreeSelectNode.New()

    -- local compositeNode_9_1 = BTreeCompositeNode.New()
    -- local onceNode_9_2 = BTreeDecOnce.New()

    -- local homeFleetAddSailorNode_10_1 = HomeFleetAddSailor.New()
    -- local selectNode_10_2 = HomeFleetLeaveHome.New()
    -- local fleetDispatchNode_10_3 = FleetDispatch.New()

    -- local checkBeforeFleetLeaveHomeNode_11_1 = CheckBeforeFleetLeaveHome.New()
    -- local compositeNode_11_2 = BTreeCompositeNode.New()

    -- local homeFleetLeaveHomeNode_12_1 = HomeFleetLeaveHome.New()
    -- local onceNode_12_2 = BTreeDecOnce.New()

    -- local fleetDispatch_13_1 = FleetDispatch.New()
    

    -- rootNode_1:PushToStack(selectNode_2_1)
    -- rootNode_1:PushToStack(exitDispatchNode_2_2)

    -- selectNode_2_1:PushToStack(noDispatchTargetNode_3_1)
    -- selectNode_2_1:PushToStack(onceNode_3_2)
    -- selectNode_2_1:PushToStack(compositeNode_3_3)

    -- onceNode_3_2:PushToStack(setDispatchFailNode_4_1)
    -- compositeNode_3_3:PushToStack(onceNode_4_2)
    -- compositeNode_3_3:PushToStack(waitClickScreenOnDispatchNode_4_3)
    -- compositeNode_3_3:PushToStack(selectNode_4_4)

    -- onceNode_4_2:PushToStack(setDispatchNode_5_1)

    -- selectNode_4_4:PushToStack(clickCancelDispatchNode_5_2)
    -- selectNode_4_4:PushToStack(compositeNode__)

    -- compositeNode__:PushToStack(waitClickDispatchNode)
    -- compositeNode__:PushToStack(selectNode_5_3)

    -- selectNode_5_3:PushToStack(checkBeforeDispatchNode_6_1)
    -- selectNode_5_3:PushToStack(selectNode_6_2)

    -- selectNode_6_2:PushToStack(onceNode_7_1)
    -- selectNode_6_2:PushToStack(onceNode_7_2)
    -- selectNode_6_2:PushToStack(onceNode_7_3)
    -- selectNode_6_2:PushToStack(onceNode_7_4)

    -- onceNode_7_1:PushToStack(fleetAttackHomeActorNode_8_1)
    -- onceNode_7_2:PushToStack(fleetTaskDispatchNode_8_2)
    -- onceNode_7_3:PushToStack(fleetAINavNode_8_3)
    -- onceNode_7_4:PushToStack(selectNode_8_4)

    -- selectNode_8_4:PushToStack(compositeNode_9_1)
    -- selectNode_8_4:PushToStack(onceNode_9_2)

    -- compositeNode_9_1:PushToStack(homeFleetAddSailorNode_10_1)
    -- compositeNode_9_1:PushToStack(selectNode_10_2)
    -- onceNode_9_2:PushToStack(fleetDispatchNode_10_3)


    -- selectNode_10_2:PushToStack(checkBeforeFleetLeaveHomeNode_11_1)
    -- selectNode_10_2:PushToStack(compositeNode_11_2)

    -- compositeNode_11_2:PushToStack(homeFleetLeaveHomeNode_12_1)
    -- compositeNode_11_2:PushToStack(onceNode_12_2)

    -- onceNode_12_2:PushToStack(fleetDispatch_13_1)

    local rootNode = BTreeSelectNode.New()
    DecodeTreeMap(treeMap,rootNode)

    DataCenter:GetBTreeMgr():AddTree(rootNode,"FleetDispatchTree")
end



return BTreeRegister