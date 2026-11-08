using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     任务搜索器界面，按副本类别与筛选条件列出可选任务，并支持勾选任务后申请参加。
/// </summary>
[Addon("ContentsFinder")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 6416)]
public unsafe partial struct AddonContentsFinder
{
    /// <summary>
    ///     副本列表的回调接口，向树形列表提供各列表项所用的渲染器。
    /// </summary>
    [FieldOffset(568)]
    public ContentsFinderListCallBack ListCallBack;

    /// <summary>
    ///     申请按钮与清除选择按钮的游标导航信息备份，共 2 项，在两种按钮之间切换状态时回写。
    /// </summary>
    [FieldOffset(592)]
    [FixedSizeArray]
    internal FixedSizeArray2<AtkCursorNavigationInfo> SelectionButtonCursorNavigationStorage;

    /// <summary>
    ///     界面内嵌的子界面控制器，负责管理随本界面一同打开与关闭的子界面。
    /// </summary>
    [FieldOffset(616)]
    public AtkAddonControl AddonControl;

    /// <summary>
    ///     任务详情子界面的注册信息，界面销毁时随之注销。
    /// </summary>
    [FieldOffset(712)]
    public AtkAddonControl.ChildAddonInfo* DetailAddonInfo;

    /// <summary>
    ///     申请按钮，在已勾选可申请任务时显示为申请，已申请时显示为撤回。
    /// </summary>
    [FieldOffset(720)]
    public AtkComponentButton* JoinButton;

    /// <summary>
    ///     次要操作按钮，存在已勾选任务时用于一次性清空全部勾选，排队中时改为查看排队状况。
    /// </summary>
    [FieldOffset(728)]
    public AtkComponentButton* ClearSelectionButton;

    /// <summary>
    ///     副本类别单选按钮，共 10 项，依次对应随机任务、迷宫挑战、迷宫挑战、行会令、讨伐歼灭战、讨伐歼灭战、大型任务、大型任务、对战、金碟游乐场；
    ///     相邻同名的两项按资料片范围区分，靠前者覆盖早期资料片，靠后者覆盖最新资料片。
    /// </summary>
    [FieldOffset(736)]
    [FixedSizeArray]
    internal FixedSizeArray10<Pointer<AtkComponentRadioButton>> DutyCategoryRadioButtonStorage;

    /// <summary>
    ///     当前选中任务的信息文本节点，共 5 项。
    /// </summary>
    [FieldOffset(816)]
    [FixedSizeArray]
    internal FixedSizeArray5<Pointer<AtkTextNode>> SelectedDutyTextNodeStorage;

    /// <summary>
    ///     任务列表，以树形结构展示可申请的任务及其分组标题。
    /// </summary>
    [FieldOffset(856)]
    public AtkComponentTreeList* DutyList;

    /// <summary>
    ///     排序方式下拉列表，可在等级由低到高与等级由高到低之间切换。
    /// </summary>
    [FieldOffset(864)]
    public AtkComponentDropDownList* OrderByDropDown;

    /// <summary>
    ///     刷新按钮。
    /// </summary>
    [FieldOffset(872)]
    public AtkComponentButton* RefreshButton;

    /// <summary>
    ///     任务难度切换按钮，在普通任务与高难度任务之间切换。
    /// </summary>
    [FieldOffset(880)]
    public AtkComponentButton* DutyTypeButton;

    /// <summary>
    ///     当前职业名称文本节点。
    /// </summary>
    [FieldOffset(888)]
    public AtkTextNode* JobNameTextNode;

    /// <summary>
    ///     当前职业等级文本节点，与职业名称文本节点在同一行紧邻显示。
    /// </summary>
    [FieldOffset(896)]
    public AtkTextNode* JobLevelTextNode;

    /// <summary>
    ///     当前职业等级文本节点，内容取自界面值中的玩家等级。
    /// </summary>
    [FieldOffset(904)]
    public AtkTextNode* LevelTextNode;

    /// <summary>
    ///     当前平均品级文本节点，内容取自界面值中的玩家平均品级。
    /// </summary>
    [FieldOffset(912)]
    public AtkTextNode* ItemLevelTextNode;

    /// <summary>
    ///     当前职能文本节点，内容取自界面值中的玩家职能名称。
    /// </summary>
    [FieldOffset(920)]
    public AtkTextNode* RoleTextNode;

    /// <summary>
    ///     已勾选任务数量文本节点。
    /// </summary>
    [FieldOffset(928)]
    public AtkTextNode* NumberSelectedTextNode;

    /// <summary>
    ///     数据获取中提示文本节点，在任务列表为空时显示。
    /// </summary>
    [FieldOffset(936)]
    public AtkTextNode* ObtainingDataTextNode;

    /// <summary>
    ///     其他队伍招募数量文本节点，用于提示当前任务的其他队长招募情况。
    /// </summary>
    [FieldOffset(944)]
    public AtkTextNode* NumOtherPartiesRecruitingTextNode;

    /// <summary>
    ///     职能图标纹理所在的资源条目，图标通过其中的纹理加载。
    /// </summary>
    [FieldOffset(952)]
    public AtkUldAsset* RoleIconAsset;

    /// <summary>
    ///     收藏星级图像节点。
    /// </summary>
    [FieldOffset(960)]
    public AtkImageNode* StarImageNode;

    /// <summary>
    ///     职能图标节点，控制职能图标的显隐与动画。
    /// </summary>
    [FieldOffset(968)]
    public AtkResNode* RoleIconResNode;

    /// <summary>
    ///     职能图标图像节点。
    /// </summary>
    [FieldOffset(976)]
    public AtkImageNode* RoleIconImageNode;

    /// <summary>
    ///     其他队伍招募数量图标节点。
    /// </summary>
    [FieldOffset(984)]
    public AtkResNode* NumOtherPartiesRecruitingResNode;

    /// <summary>
    ///     筛选条件按钮，共 8 项，用于切换解除人数限制、等级同步等任务搜索选项。
    /// </summary>
    [FieldOffset(992)]
    [FixedSizeArray]
    internal FixedSizeArray8<Pointer<AtkComponentButton>> SettingsButtonStorage;

    /// <summary>
    ///     日语匹配语言标识组件。
    /// </summary>
    [FieldOffset(1056)]
    public AtkComponentBase* LanguageLetterJ;

    /// <summary>
    ///     英语匹配语言标识组件。
    /// </summary>
    [FieldOffset(1064)]
    public AtkComponentBase* LanguageLetterE;

    /// <summary>
    ///     德语匹配语言标识组件。
    /// </summary>
    [FieldOffset(1072)]
    public AtkComponentBase* LanguageLetterD;

    /// <summary>
    ///     法语匹配语言标识组件。
    /// </summary>
    [FieldOffset(1080)]
    public AtkComponentBase* LanguageLetterF;

    /// <summary>
    ///     排序方式下拉列表各条目的文本，共 2 项。
    /// </summary>
    [FieldOffset(1088)]
    [FixedSizeArray]
    internal FixedSizeArray2<CStringPointer> OrderByLabelStorage;

    /// <summary>
    ///     界面刷新时缓存的界面值，共 330 项，用于任务详情子界面的刷新。
    /// </summary>
    [FieldOffset(1104)]
    [FixedSizeArray]
    internal FixedSizeArray330<AtkValue> CachedAtkValuesStorage;

    /// <summary>
    ///     当前选中的副本类别单选按钮索引。
    /// </summary>
    [FieldOffset(6384)]
    public uint SelectedRadioButton;

    /// <summary>
    ///     当前可用单选按钮中的最小索引，用于在不可用类别之间跳转时判断下界。
    /// </summary>
    [FieldOffset(6388)]
    public uint FirstEnabledRadioButtonIndex;

    /// <summary>
    ///     当前可用单选按钮中的最大索引，用于在不可用类别之间跳转时判断上界。
    /// </summary>
    [FieldOffset(6392)]
    public uint LastEnabledRadioButtonIndex;

    /// <summary>
    ///     任务列表中当前高亮的行索引，包含不可点击的分组标题行。
    /// </summary>
    [FieldOffset(6396)]
    public uint SelectedRow;

    /// <summary>
    ///     当前类别下的任务列表条目数量，包含分组标题行。
    /// </summary>
    [FieldOffset(6400)]
    public uint NumEntries;

    /// <summary>
    ///     当前是否处于排队中，对应排队状态为等待、排队中、已就绪或已接受；
    ///     为真时确认键不再触发参加申请，操作指南切换为排队中的提示。
    /// </summary>
    [FieldOffset(6404)]
    public bool IsQueued;

    /// <summary>
    ///     是否已向界面回调通知过界面进入可交互状态，界面隐藏时重置。
    /// </summary>
    [FieldOffset(6405)]
    public bool HasFiredReadyCallback;

    /// <summary>
    ///     职业等级文本节点相对职业名称文本节点的横坐标差值，用于重新排版时还原位置。
    /// </summary>
    [FieldOffset(6406)]
    public short JobLevelTextOffsetX;

    /// <summary>
    ///     当前焦点所在的区域，决定操作指南显示的内容。
    /// </summary>
    [FieldOffset(6408)]
    public FocusAreaType FocusArea;

    /// <summary>
    ///     构造界面并初始化游标导航信息、子界面控制器与各状态字段。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 33 ED 48 8D 05 ?? ?? ?? ?? 48 89 03 48 8D BB ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 89 AB ?? ?? ?? ?? 48 89 83 ?? ?? ?? ?? 8D 75 ?? 48 89 AB ?? ?? ?? ?? 66 90 48 8B CF E8 ?? ?? ?? ?? 48 83 C7 ?? 48 83 EE ?? 75 ?? 48 8D 8B"
    )]
    public partial AddonContentsFinder* Ctor();

    /// <summary>
    ///     析构界面，先销毁缓存的界面值并复位回调接口虚表，再销毁子界面控制器与基类。
    /// </summary>
    /// <param name="freeFlags">释放标志，最低位为 1 时释放界面自身占用的内存。</param>
    /// <returns>界面自身。</returns>
    [VirtualFunction(0)]
    public partial AtkUnitBase* PrepareDestructor
    (
        byte freeFlags
    );

    /// <summary>
    ///     处理界面事件，把按钮点击、列表项点击、滚动与焦点变化转换为界面回调与状态更新。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数，用于区分同一事件类型下的不同来源控件。</param>
    /// <param name="atkEvent">触发本次事件的节点事件。</param>
    /// <param name="atkEventData">本次事件携带的数据。</param>
    [VirtualFunction(2)]
    public partial void ReceiveAddonEvent
    (
        AtkEventType  eventType,
        int           eventParam,
        AtkEvent*     atkEvent,
        AtkEventData* atkEventData
    );

    /// <summary>
    ///     显示界面，存在任务条目时才同时显示内嵌子界面的全部控件。
    /// </summary>
    /// <param name="disableShowTransition">是否禁用显示过渡动画。</param>
    /// <param name="unsetShowHideFlags">显示时需要清除的显隐标志位。</param>
    [VirtualFunction(5)]
    public partial void ShowAddon
    (
        bool disableShowTransition,
        uint unsetShowHideFlags
    );

    /// <summary>
    ///     隐藏界面，隐藏时递减阻塞弹窗计数并重置就绪回调标志。
    /// </summary>
    /// <param name="disableHideTransition">是否禁用隐藏过渡动画。</param>
    /// <param name="callCloseCallback">是否在隐藏后触发关闭回调。</param>
    /// <param name="setShowHideFlags">隐藏时需要设置的显隐标志位。</param>
    [VirtualFunction(6)]
    public partial void HideAddon
    (
        bool disableHideTransition,
        bool callCloseCallback,
        uint setShowHideFlags
    );

    /// <summary>
    ///     隐藏内嵌子界面的全部子控件并向界面回调通知已关闭。
    /// </summary>
    [VirtualFunction(16)]
    public partial void HideChildComponents();

    /// <summary>
    ///     按窗口组件当前的位置与缩放换算根节点位置，使界面内容跟随窗口移动。
    /// </summary>
    [VirtualFunction(25)]
    public partial void SyncPositionWithWindowComponent();

    /// <summary>
    ///     初始化界面，同时把本界面作为宿主初始化内嵌的子界面控制器。
    /// </summary>
    [VirtualFunction(41)]
    public partial void InitializeAddon();

    /// <summary>
    ///     销毁前的收尾处理，注销任务详情子界面并解除对数组数据的订阅。
    /// </summary>
    [VirtualFunction(42)]
    public partial void FinalizeAddon();

    /// <summary>
    ///     每帧更新，界面进入可交互状态后向界面回调通知一次。
    /// </summary>
    /// <param name="delta">距上一帧的时间间隔。</param>
    [VirtualFunction(43)]
    public partial void UpdateAddon
    (
        float delta
    );

    /// <summary>
    ///     绘制界面及其内嵌的子界面控制器。
    /// </summary>
    [VirtualFunction(44)]
    public partial void DrawAddon();

    /// <summary>
    ///     加载任务搜索器的界面布局资源。
    /// </summary>
    /// <returns>布局资源是否加载成功。</returns>
    [VirtualFunction(46)]
    public partial bool LoadContentsFinderUld();

    /// <summary>
    ///     界面建立时绑定全部控件节点、注册节点事件并应用界面值。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    [VirtualFunction(49)]
    public partial void SetupAddon
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     向界面回调通知界面已打开。
    /// </summary>
    /// <returns>界面回调的返回值。</returns>
    [VirtualFunction(50)]
    public partial bool FireOpenCallback();

    /// <summary>
    ///     界面刷新时按界面值重建任务列表并更新各控件的可用状态与显示文本。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    /// <returns>界面是否已建立，未建立时返回假且不刷新。</returns>
    [VirtualFunction(51)]
    public partial bool RefreshDutyList
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     按需更新时刷新职业名称与职业等级文本并重新对齐两者位置。
    /// </summary>
    /// <param name="numberArrayData">数字数组数据指针数组。</param>
    /// <param name="stringArrayData">字符串数组数据指针数组。</param>
    [VirtualFunction(52)]
    public partial void RefreshJobTextNodes
    (
        NumberArrayData** numberArrayData,
        StringArrayData** stringArrayData
    );

    /// <summary>
    ///     关闭内嵌子界面的全部子界面并触发关闭回调，随后清空任务详情子界面注册信息。
    /// </summary>
    [VirtualFunction(54)]
    public partial void CloseChildAddons();

    /// <summary>
    ///     处理界面自定义按键输入，包括确认键与任务条目勾选切换。
    /// </summary>
    /// <param name="inputData">本次按键输入的数据。</param>
    /// <returns>是否已消费本次输入。</returns>
    [VirtualFunction(57)]
    public partial bool ProcessCustomInput
    (
        AtkEventData.AtkInputData* inputData
    );

    /// <summary>
    ///     处理方向键输入，在任务列表内部导航，或在焦点位于列表内时进入任务详情子界面。
    /// </summary>
    /// <param name="inputID">输入标识。</param>
    /// <param name="a3">是否处于重复触发状态。</param>
    /// <returns>是否已消费本次输入。</returns>
    [VirtualFunction(69)]
    public partial bool ProcessDPadInput
    (
        int  inputID,
        bool a3
    );

    /// <summary>
    ///     处理返回键输入，在启用的副本类别单选按钮之间循环切换。
    /// </summary>
    /// <param name="inputID">输入标识。</param>
    /// <param name="a3">是否处于重复触发状态。</param>
    /// <returns>是否已消费本次输入。</returns>
    [VirtualFunction(71)]
    public partial bool ProcessBackButtonInput
    (
        int  inputID,
        bool a3
    );

    /// <summary>
    ///     显示鼠标悬停任务条目所对应的说明提示。
    /// </summary>
    /// <param name="atkEventData">鼠标悬停事件携带的数据。</param>
    [MemberFunction("40 53 55 56 48 83 EC ?? 48 8B 2A")]
    public partial void ShowHoveredItemTooltip
    (
        AtkEventData* atkEventData
    );

    /// <summary>
    ///     界面内嵌的列表回调接口，供任务列表取得各列表项所用的渲染器。
    /// </summary>
    [GenerateInterop]
    [Inherits<ListComponentCallBackInterface>]
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public unsafe partial struct ContentsFinderListCallBack
    {
        /// <summary>
        ///     回调接口所属的界面。
        /// </summary>
        [FieldOffset(8)]
        public AtkUnitBase* Owner;

        /// <summary>
        ///     按列表项类型返回对应的列表项渲染器。
        /// </summary>
        [FieldOffset(16)]
        public delegate* unmanaged<AddonContentsFinder*, AtkComponentListItemPopulator.ListItemInfo*, AtkComponentListItemRenderer*> GetItemRenderer;
    }

    /// <summary>
    ///     当前焦点所在的区域。
    /// </summary>
    public enum FocusAreaType : byte
    {
        /// <summary>
        ///     焦点位于任务列表本身。
        /// </summary>
        DutyList = 0,

        /// <summary>
        ///     焦点位于任务列表条目内的按钮上。
        /// </summary>
        ListItemButton = 1,

        /// <summary>
        ///     焦点位于任务详情子界面内。
        /// </summary>
        DetailAddon = 2
    }
}
