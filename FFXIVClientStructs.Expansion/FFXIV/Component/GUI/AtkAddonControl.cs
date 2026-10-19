using NativeAtkAddonControl = FFXIVClientStructs.FFXIV.Component.GUI.AtkAddonControl;
using FFXIVClientStructs.FFXIV.Component.GUI;
using CStringPointer = InteropGenerator.Runtime.CStringPointer;
using FFXIVClientStructs.FFXIV.Client.System.Memory;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop]
[Inherits<NativeAtkAddonControl>]
[StructLayout(LayoutKind.Explicit, Size = 0x60)]
public unsafe partial struct AtkAddonControl : ICreatable<AtkAddonControl>
{
    [FieldOffset(0x40)]
    public int SelectedChildIndex;

    [FieldOffset(0x48)]
    public ChildAddonInfo* DraggingChildAddon;

    [FieldOffset(0x50)]
    public ChildAddonInfo* AttachingChildAddon;

    [FieldOffset(0x58)]
    public short DragStartX;
    [FieldOffset(0x5A)]
    public short DragStartY;

    [FieldOffset(0x5C)]
    public bool IsParentAddonLinked;

    [FieldOffset(0x5D)]
    public bool IsChildSetupComplete;

    [FieldOffset(0x5E)]
    public bool DisableFocusOnChildSetup;

    [MemberFunction("E8 ?? ?? ?? ?? 33 C9 33 C0 48 89 8B")]
    public partial AtkAddonControl* Ctor();

    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B F9 48 89 01 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? 48 8B 59")]
    public partial void Destructor();

    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 59 ?? 48 8B F9 48 8B 03 48 3B C3 74 ?? 0F 1F 80 ?? ?? ?? ?? 4C 8B 40")]
    public partial void Clear();

    [MemberFunction("48 89 51 ?? 48 85 D2 74 ?? 83 8A")]
    public partial bool Initialize
    (
        AtkUnitBase* parentAddon
    );

    [MemberFunction("40 53 55 57 41 57 48 81 EC ?? ?? ?? ?? 48 8B 79")]
    public partial void Update
    (
        float deltaTime
    );

    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 57 48 83 EC ?? 48 8B 79 ?? 48 8B E9 48 8B 1F 48 3B DF 0F 84")]
    public partial void Draw();

    [MemberFunction("48 83 EC ?? 4C 8B 41 ?? 49 8B 00 49 3B C0 74 ?? 4C 8B 50 ?? 49 39 52")]
    public partial void RemoveChildAddon
    (
        AtkUnitBase* addon
    );

    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 59 ?? 0F B6 F2 48 8B F9 48 8B 03 48 3B C3 74 ?? 0F 1F 40")]
    public partial void Finalizer
    (
        bool a2 = false
    );

    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 56 41 54 41 55 41 56 41 57 48 81 EC")]
    public partial ChildAddonInfo* RegisterChildAddon
    (
        ushort addonID,
        bool   active,
        bool   allowDetach,
        int    tabIndex,
        int    groupID,
        int    a7,
        bool   attached,
        bool   hideWindowAndFocus,
        byte   additionalFlags
    );

    [MemberFunction("48 83 EC ?? 4D 8B D0 4D 85 C0")]
    public partial AtkEvent* BindDragHandle
    (
        ChildAddonInfo* child,
        AtkResNode*     node
    );

    [MemberFunction("40 55 57 41 54 48 83 EC ?? 48 8B 79")]
    public partial uint SelectTab
    (
        int tabIndex
    );

    [MemberFunction("48 89 5C 24 ?? 57 41 56 41 57 48 83 EC ?? 48 8B 79")]
    public partial void ShowAllChildren
    (
        uint unsetShowHideFlags
    );

    [MemberFunction("E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC 40 57 48 83 EC ?? 33 C0")]
    public partial void HideAllChildren
    (
        bool callHideCallback,
        bool disableTransition,
        uint setShowHideFlags
    );

    [MemberFunction("E8 ?? ?? ?? ?? 48 8B CB E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 C7 83")]
    public partial void CloseAllChildren
    (
        bool fireCallback
    );

    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 79 ?? 48 8B 1F 48 3B DF 74")]
    public partial void Hide2AllChildren();

    [MemberFunction("48 89 5C 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC ?? 48 8B 79")]
    public partial void ActivateGroup
    (
        int  groupID,
        bool deactivateOtherGroups = true,
        bool focus                 = true
    );

    [MemberFunction("48 8B 49 ?? 48 8B 01 48 3B C1 74 ?? 48 8B 50 ?? F6 42")]
    public partial ChildAddonInfo* GetActiveChildAddonInfo();

    [MemberFunction("48 8B 49 ?? 48 8B 01 48 3B C1 74 ?? 0F 1F 40 ?? 4C 8B 40 ?? 49 39 50")]
    public partial ChildAddonInfo* GetChildAddonInfoByAtkUnitBase
    (
        AtkUnitBase* addon
    );

    [MemberFunction("48 8B 49 ?? 48 8B 01 48 3B C1 74 ?? 0F 1F 40 ?? 4C 8B 40 ?? 41 39 50")]
    public partial ChildAddonInfo* GetChildAddonInfoByGroupID
    (
        int groupID
    );

    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 79 ?? 33 D2")]
    public partial void FocusSelectedGroup();

    [MemberFunction("45 8B D0 48 85 D2 74 ?? 48 8B 4A")]
    public partial bool ChildRefresh
    (
        ChildAddonInfo* child,
        uint            atkValueCount,
        AtkValue*       atkValues
    );

    [MemberFunction("4D 8B D0 48 85 D2 74")]
    public partial void ChildRequestedUpdate
    (
        ChildAddonInfo*   child,
        NumberArrayData** numberArrayData,
        StringArrayData** stringArrayData
    );

    [MemberFunction("E8 ?? ?? ?? ?? 48 8B 8D ?? ?? ?? ?? 48 8B 89 ?? ?? ?? ?? E8 ?? ?? ?? ?? 66 2B B5")]
    public partial void ChildSetSize
    (
        ushort width,
        ushort height,
        bool   includeDetached = false
    );

    [MemberFunction
    (
        "48 83 EC ?? 48 83 C1 ?? C6 44 24 ?? ?? E8 ?? ?? ?? ?? 48 83 C4 ?? C3 CC CC CC CC CC CC CC CC CC 48 8B 41 ?? 48 85 C0 74 ?? 0F B7 D2 0F 1F 40 ?? 0F B6 48 ?? 3B CA 74 ?? 48 8B 40 ?? 48 85 C0 75 ?? 32 C0 C3 B0 ?? C3 CC CC CC CC CC CC CC CC CC 48 83 EC"
    )]
    public partial bool UnregisterEvent
    (
        AtkEventType      eventType,
        uint              eventParam,
        AtkEventListener* listener
    );

    [MemberFunction
        ("48 8B 41 ?? 48 85 C0 74 ?? 0F B7 D2 0F 1F 40 ?? 0F B6 48 ?? 3B CA 74 ?? 48 8B 40 ?? 48 85 C0 75 ?? 32 C0 C3 B0 ?? C3 CC CC CC CC CC CC CC CC CC 48 83 EC")]
    public partial bool IsEventRegistered
    (
        AtkEventType eventType
    );

    [MemberFunction
    (
        "48 83 EC ?? 48 8B 41 ?? 4C 8D 49 ?? 48 85 C0 74 ?? 44 0F B7 02 0F B6 48 ?? 41 3B C8 74 ?? 48 8B 40 ?? 48 85 C0 75 ?? 33 C0 48 83 C4 ?? C3 45 33 C0 49 8B C9 E8 ?? ?? ?? ?? 84 C0 74 ?? B8 ?? ?? ?? ?? 48 83 C4 ?? C3 CC CC CC CC CC CC CC CC CC 48 85 D2"
    )]
    public partial bool DispatchEvent
    (
        AtkEventDispatcher.Event* evt
    );

    [MemberFunction("E8 ?? ?? ?? ?? 8B 87 ?? ?? ?? ?? 48 8B 8C C7")]
    public partial void DetachChildAddon
    (
        ChildAddonInfo* child
    );

    [MemberFunction("48 85 D2 74 ?? 48 8B 42 ?? 48 85 C0 74 ?? 80 4A")]
    public partial void AttachChildAddon
    (
        ChildAddonInfo* child
    );

    [MemberFunction("48 85 D2 0F 84 ?? ?? ?? ?? 48 8B 4A ?? 48 85 C9")]
    public partial void SetChildAttachedState
    (
        ChildAddonInfo* child,
        bool            attached
    );

    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 33 C0 4C 8D 49")]
    public partial bool CancelDragListeners();

    [MemberFunction("40 56 41 57 48 81 EC ?? ?? ?? ?? F6 42")]
    public partial void CompleteChildSetup
    (
        ChildAddonInfo* child
    );

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 0x48)]
    public partial struct ChildAddonInfo
    {
        [FieldOffset(0x00)]
        private CStringPointer unk0;
        [FieldOffset(0x08)]
        public AtkUnitBase* AtkUnitBase;
        [FieldOffset(0x10)]
        public AtkResNode* DragHandle;

        [FieldOffset(0x18)]
        public AtkResNode* AttachmentHiddenNode;
        [FieldOffset(0x20)]
        public AtkCollisionNode* CollisionNode;
        [FieldOffset(0x28)]
        public ushort AddonID;

        [FieldOffset(0x2C)]
        public int GroupID;
        [FieldOffset(0x30)]
        public int TabIndex;
        [FieldOffset(0x34)]
        public float Scale;

        [FieldOffset(0x3C)]
        public short PositionX;
        [FieldOffset(0x3E)]
        public short PositionY;

        [BitField<bool>(nameof(IsSetupComplete),     0)]
        [BitField<bool>(nameof(IsGroupActive),       1)]
        [BitField<bool>(nameof(AllowDetach),         2)]
        [BitField<bool>(nameof(IsAttached),          3)]
        [BitField<bool>(nameof(WasDragging),         4)]
        [BitField<bool>(nameof(IsAttachmentPending), 5)]
        [BitField<bool>(nameof(InitiallyAttached),   6)]
        [FieldOffset(0x40)]
        public byte Flags1;
        [BitField<bool>(nameof(IsActive),                  1)]
        [BitField<bool>(nameof(HideWindowAndFocusOnSetup), 2)]
        [FieldOffset(0x41)]
        public byte Flags2;
    }
}
