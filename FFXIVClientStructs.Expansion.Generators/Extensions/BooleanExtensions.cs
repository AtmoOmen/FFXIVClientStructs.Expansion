namespace FFXIVClientStructs.Expansion.Generators.Extensions;

public static class BooleanExtensions {
    public static string ToLowercaseString(this bool boolean) => boolean ? "true" : "false";
}
