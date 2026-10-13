using NativeAgentFreeCompany = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentFreeCompany;
using Dalamud.Game.Text.SeStringHandling;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[Inherits<NativeAgentFreeCompany>]
[Agent(AgentId.FreeCompany)]
[StructLayout(LayoutKind.Explicit, Size = 0xAD8)]
public unsafe partial struct AgentFreeCompany
{
    [FieldOffset(104)]
    public MemberActionType PendingAction;

    [FieldOffset(120)]
    public ulong TargetContentID;

    [FieldOffset(128)]
    public byte TargetRank;

    public void SwitchMemberPage
    (
        int page
    )
    {
        var values = stackalloc AtkValue[2];
        values[0].Type = AtkValueType.Int;
        values[0].Int  = 1;
        values[1].Type = AtkValueType.UInt;
        values[1].UInt = (uint)page;

        ProcessMemberListEvent(values);
    }

    public void ExecuteMemberAction
    (
        ulong            contentID,
        MemberActionType action,
        byte             rank
    )
    {
        TargetContentID = contentID;
        TargetRank      = rank;
        PendingAction   = action;

        var values = stackalloc AtkValue[1];
        values[0].Type = AtkValueType.Int;
        values[0].Int  = 0;

        ConfirmMemberAction(values);
    }

    public readonly string GetMemberLocationText
    (
        int memberIndex
    )
    {
        var stringArray = AtkStage.Instance()->GetStringArrayData(StringArrayType.FreeCompanyMember);

        return stringArray == null ?
                   string.Empty :
                   SeString.Parse(stringArray->StringArray[MEMBER_STRING_LOCATION + (MEMBER_STRING_SIZE * memberIndex)].Value).TextValue;
    }

    public enum MemberActionType : byte
    {
        Dismiss = 0,
        Promote = 4
    }

    #region 常量

    private const int MEMBER_STRING_SIZE     = 5;
    private const int MEMBER_STRING_LOCATION = 1;

    #endregion

    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 41 56 48 83 EC ?? 48 8B F1 48 8B DA")]
    private partial void ProcessMemberListEvent
    (
        AtkValue* values
    );

    [MemberFunction("48 89 5C 24 ?? 56 48 83 EC ?? 48 8B D9 48 8B CA E8 ?? ?? ?? ?? C7 43")]
    private partial void ConfirmMemberAction
    (
        AtkValue* values
    );
}
