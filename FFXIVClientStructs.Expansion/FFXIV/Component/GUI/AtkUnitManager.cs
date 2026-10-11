using FFXIVClientStructs.FFXIV.Component.GUI;
using CStringPointer = FFXIVClientStructs.Expansion.Runtime.CStringPointer;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Common.Math;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop(isInherited: true)]
[Inherits<AtkEventListener>]
[VirtualTable("48 8D 05 ?? ?? ?? ?? 48 8B D9 ?? ?? ?? 48 81 C1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8D 15", 3, 43)]
[StructLayout(LayoutKind.Explicit, Size = 0x9CA0)]
public unsafe partial struct AtkUnitManager
{
    [FieldOffset(0x30), FixedSizeArray]
    internal FixedSizeArray13<AtkUnitList> DepthLayersStorage;
    [FieldOffset(0x30)]
    public AtkUnitList DepthLayerOneList;
    [FieldOffset(0x840)]
    public AtkUnitList DepthLayerTwoList;
    [FieldOffset(0x1050)]
    public AtkUnitList DepthLayerThreeList;
    [FieldOffset(0x1860)]
    public AtkUnitList DepthLayerFourList;
    [FieldOffset(0x2070)]
    public AtkUnitList DepthLayerFiveList;
    [FieldOffset(0x2880)]
    public AtkUnitList DepthLayerSixList;
    [FieldOffset(0x3090)]
    public AtkUnitList DepthLayerSevenList;
    [FieldOffset(0x38A0)]
    public AtkUnitList DepthLayerEightList;
    [FieldOffset(0x40B0)]
    public AtkUnitList DepthLayerNineList;
    [FieldOffset(0x48C0)]
    public AtkUnitList DepthLayerTenList;
    [FieldOffset(0x50D0)]
    public AtkUnitList DepthLayerElevenList;
    [FieldOffset(0x58E0)]
    public AtkUnitList DepthLayerTwelveList;
    [FieldOffset(0x60F0)]
    public AtkUnitList DepthLayerThirteenList;
    [FieldOffset(0x6900)]
    public AtkUnitList AllLoadedUnitsList;
    [FieldOffset(0x7110)]
    public AtkUnitList FocusedUnitsList;
    [FieldOffset(0x7920)]
    public AtkUnitList UnitList16;
    [FieldOffset(0x8130)]
    public AtkUnitList UnitList17;
    [FieldOffset(0x8940)]
    public AtkUnitList UnitList18;
    [FieldOffset(0x9150)]
    public AtkUnitBase* FocusedAddon;
    [FieldOffset(0x9158)]
    private AtkUnitBase* focusedAddon2;
    [FieldOffset(0x9160)]
    public AddonCursor* AddonCursor;
    [FieldOffset(0x9168)]
    public AddonOperationGuide* AddonOperationGuide;
    [FieldOffset(0x9170)]
    public AddonFilter* AddonFilter;
    [FieldOffset(0x9178)]
    public AddonFilter* AddonFilterSystem;
    [FieldOffset(0x9180)]
    public AddonDragDrop* AddonDragDrop;

    [FieldOffset(0x9188)]
    public AtkManagedInterface* ManagedScreenFrame;
    [FieldOffset(0x9190)]
    private AtkResNode* chatLogFocusNode;
    [FieldOffset(0x9198)]
    public AtkResNode* CursorFocusNode;

    [FieldOffset(0x91A0), FixedSizeArray]
    internal FixedSizeArray128<ushort> HudInitAddonIdsStorage;
    [FieldOffset(0x92A0)]
    private AtkResNode unk92A0;
    [FieldOffset(0x9360)]
    public Size LastScreenSize;
    [FieldOffset(0x9368)]
    public float LastScreenSizeScale;
    [FieldOffset(0x936C)]
    public float HUDScaleDefault;

    [FieldOffset(0x9370)]
    public float UiBaseScale;

    [FieldOffset(0x9374)]
    public float ScreenTextBaseScale;

    [FieldOffset(0x9378), FixedSizeArray]
    internal FixedSizeArray13<ushort> NextDrawOrderIndexesStorage;

    [FieldOffset(0x9392)]
    public short NumHudInitAddonIds;

    [FieldOffset(0x9398), FixedSizeArray]
    internal FixedSizeArray48<HudAnchoringInfo> HudAnchoringTableStorage;
    [FieldOffset(0x9C98)]
    public AtkUnitManagerFlags Flags;
    [FieldOffset(0x9C99)]
    private byte flags2;

    [VirtualFunction(7)]
    public partial void GetAddonCollision
    (
        AddonCollision* collisionInfo,
        short           x,
        short           y
    );

    [VirtualFunction(8)]
    public partial bool SetAddonVisibility
    (
        ushort addonId,
        bool   visible
    );

    [VirtualFunction(9)]
    public partial AddonStatus GetAddonStatus
    (
        ushort addonId
    );

    [VirtualFunction(10)]
    public partial bool RefreshAddon
    (
        AtkUnitBase* addon,
        uint         valueCount,
        AtkValue*    values
    );

    [VirtualFunction(11)]
    public partial void AddonRequestUpdateById
    (
        ushort            addonId,
        NumberArrayData** numberArrayData,
        StringArrayData** stringArrayData,
        bool              forced
    );

    [VirtualFunction(24)]
    public partial void ToggleUi
    (
        UiFlags flags,
        bool    show,
        bool    disableTransition,
        bool    a5 = false
    );

    [VirtualFunction(25)]
    public partial bool IsUiFlagsSet
    (
        UiFlags flags
    );

    [VirtualFunction(39)]
    public partial bool ShouldApplyUiBaseScale
    (
        AtkUnitBase* addon
    );

    [VirtualFunction(40)]
    public partial bool ShouldApplyScreenTextBaseScale
    (
        AtkUnitBase* addon
    );

    [MemberFunction("E8 ?? ?? ?? ?? 48 8B F8 41 B0 01"), GenerateStringOverloads]
    public partial AtkUnitBase* GetAddonByName
    (
        CStringPointer name,
        int            index = 1
    );

    [MemberFunction("E8 ?? ?? ?? ?? 8B 6B 20")]
    public partial AtkUnitBase* GetAddonById
    (
        ushort id
    );

    [MemberFunction("E8 ?? ?? ?? ?? 48 3B E8 75 0E")]
    public partial AtkUnitBase* GetAddonByNode
    (
        AtkResNode* node
    );

    [MemberFunction("E8 ?? ?? ?? ?? 40 B5 ?? 48 83 C3")]
    public partial bool SetAddonDepthLayer
    (
        ushort id,
        uint   depthLayerIndex
    );

    [MemberFunction("E8 ?? ?? ?? ?? EB ?? 48 81 C1 ?? ?? ?? ?? 41 B9")]
    public partial bool SetAddonDepthLayerAndOrder
    (
        AtkUnitBase* addon,
        uint         depthLayerIndex,
        ushort       previousAddonId,
        ushort       nextAddonId
    );

    [MemberFunction("E8 ?? ?? ?? ?? B0 ?? EB ?? 41 B0")]
    public partial bool SetAddonOrderAfter
    (
        AtkUnitBase* referenceAddon,
        AtkUnitBase* addon,
        bool         insertImmediately
    );

    [MemberFunction("E8 ?? ?? ?? ?? F6 86 ?? ?? ?? ?? ?? 0F 28 B4 24")]
    public partial void UpdateDrawOrderIndexes();

    [MemberFunction("E8 ?? ?? ?? ?? 0F 28 CE 48 8B CB E8 ?? ?? ?? ?? 48 8B CB E8 ?? ?? ?? ?? 0F 28 CE")]
    public partial void UpdateCursor();

    [MemberFunction("E8 ?? ?? ?? ?? 44 0F B6 8C 24 ?? ?? ?? ?? 48 8D 8F")]
    public partial void SetAddonParentId
    (
        ushort addonId,
        ushort parentId
    );

    public void SetAddonBlocking
    (
        ushort addonId,
        bool   blocking
    ) =>
        SetAddonBlocking
        (
            addonId,
            blocking ?
                (byte)1 :
                (byte)0
        );

    [MemberFunction("E8 ?? ?? ?? ?? 48 8B 07 44 8B C5")]
    public partial void SetAddonBlocking
    (
        ushort addonId,
        byte   isBlocking
    );

    [MemberFunction("E8 ?? ?? ?? ?? 48 8B 9C 24 ?? ?? ?? ?? 4D 8D 6D 20")]
    public partial void SetAddonHideOnCallback
    (
        ushort addonId,
        bool   hideAfterAddonReady,
        bool   addToHudInitList
    );

    [MemberFunction("E8 ?? ?? ?? ?? 4D 85 F6 0F 84 ?? ?? ?? ?? 0F B7 D6")]
    public partial void AddAddonToHudInitAddonIds
    (
        ushort addonId
    );

    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 33 F6")]
    public partial uint GetParentAddonsById
    (
        AtkUnitBase** outAddons,
        ushort        parentId,
        int           maxCount
    );

    [MemberFunction("40 55 41 56 48 83 EC ?? 48 8B 01 4C 8B F1")]
    public partial void HideBlockedAddons();

    [MemberFunction("40 56 41 55 41 57 48 83 EC ?? 48 8B 01")]
    public partial void RestoreBlockedAddons
    (
        AtkUnitBase* excludedAddon
    );

    [MemberFunction("E8 ?? ?? ?? ?? BF ?? ?? ?? ?? 49 FF C7")]
    public partial void CloseAddonById
    (
        ushort addonId
    );

    [MemberFunction("40 53 57 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 32 DB")]
    public partial void CloseOrHideAddon
    (
        AtkUnitBase* addon
    );

    [StructLayout(LayoutKind.Explicit, Size = 0x30)]
    public struct HudAnchoringInfo
    {
        [FieldOffset(0x00)]
        public AtkUnitBase* AtkUnitBase;
        [FieldOffset(0x08)]
        public uint NameHash;
        [FieldOffset(0x0C)]
        public AlignmentType AlignmentType;

        [FieldOffset(0x10)]
        public float NormalizedCoordinate;
    }
}
