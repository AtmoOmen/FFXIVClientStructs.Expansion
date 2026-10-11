namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game;

public enum AccountInfoState
{
    Uninitialized      = -2,
    ReadingCacheFile   = -1,
    CacheFileInvalid   = 0,
    UpdatedFromServer  = 1,
    CacheFileProcessed = 2
}
