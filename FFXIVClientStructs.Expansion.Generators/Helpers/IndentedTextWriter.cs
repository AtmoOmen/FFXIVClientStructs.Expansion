
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

#pragma warning disable IDE0290

namespace FFXIVClientStructs.Expansion.Generators.Helpers;

/// <summary>
///     A helper type to build sequences of values with pooled buffers.
/// </summary>
internal sealed class IndentedTextWriter : IDisposable {

   
   
   
   
   
   
    public delegate void Callback<T>(T value, IndentedTextWriter writer);

   
   
   
    private const string DefaultIndentation = "    ";

   
   
   
    private const string DefaultNewLine = "\r\n";

   
   
   
    private string[] availableIndentations;

   
   
   
    private ImmutableArrayBuilder<char> builder;

   
   
   
    private string currentIndentation = "";

   
   
   
    private int currentIndentationLevel;

   
   
   
    public IndentedTextWriter() {
        builder = new ImmutableArrayBuilder<char>();
        currentIndentationLevel = 0;
        currentIndentation = "";
        availableIndentations = new string[4];
        availableIndentations[0] = "";

        for (int i = 1, n = availableIndentations.Length; i < n; i++) {
            availableIndentations[i] = availableIndentations[i - 1] + DefaultIndentation;
        }
    }

   
    public void Dispose() {
        builder.Dispose();
    }

   
   
   
   
   
   
   
   
   
    public Span<char> Advance(int requestedSize) {
        if (builder.Count == 0 || builder.WrittenSpan[^2..].SequenceEqual(DefaultNewLine.AsSpan())) {
            builder.AddRange(currentIndentation.AsSpan());
        }

        return builder.Advance(requestedSize);
    }

   
   
   
    public void IncreaseIndent() {
        currentIndentationLevel++;

        if (currentIndentationLevel == availableIndentations.Length) {
            Array.Resize(ref availableIndentations, availableIndentations.Length * 2);
        }
        currentIndentation = availableIndentations[currentIndentationLevel]
            ??= availableIndentations[currentIndentationLevel - 1] + DefaultIndentation;
    }

   
   
   
    public void DecreaseIndent() {
        currentIndentationLevel--;
        currentIndentation = availableIndentations[currentIndentationLevel];
    }

   
   
   
   
    public Block WriteBlock() {
        WriteLine("{");
        IncreaseIndent();

        return new Block(this);
    }

   
   
   
   
   
    public void Write(string content, bool isMultiline = false) {
        Write(content.AsSpan(), isMultiline);
    }

   
   
   
   
   
    public void Write(ReadOnlySpan<char> content, bool isMultiline = false) {
        if (isMultiline) {
            while (content.Length > 0) {
                int newLineIndex = content.IndexOf(DefaultNewLine.AsSpan());

                if (newLineIndex < 0) {
                    WriteRawText(content);

                    break;
                }
                ReadOnlySpan<char> line = content[..newLineIndex];
                WriteIf(!line.IsEmpty, line);
                WriteLine();
                content = content[(newLineIndex + 2)..];
            }
        } else {
            WriteRawText(content);
        }
    }

   
   
   
   
    public void Write([InterpolatedStringHandlerArgument("")] ref WriteInterpolatedStringHandler handler) {
        _ = this;
    }

   
   
   
   
   
   
    public void WriteIf(bool condition, string content, bool isMultiline = false) {
        if (condition) {
            Write(content.AsSpan(), isMultiline);
        }
    }

   
   
   
   
   
   
    public void WriteIf(bool condition, ReadOnlySpan<char> content, bool isMultiline = false) {
        if (condition) {
            Write(content, isMultiline);
        }
    }

   
   
   
   
   
    public void WriteIf(bool condition, [InterpolatedStringHandlerArgument("", nameof(condition))] ref WriteIfInterpolatedStringHandler handler) {
        _ = this;
    }

   
   
   
   
    public void WriteLine(bool skipIfPresent = false) {
        if (skipIfPresent && builder.WrittenSpan is [.., '\r', '\n', '\r', '\n']) {
            return;
        }

        builder.AddRange(DefaultNewLine.AsSpan());
    }

   
   
   
   
   
    public void WriteLine(string content, bool isMultiline = false) {
        WriteLine(content.AsSpan(), isMultiline);
    }

   
   
   
   
   
    public void WriteLine(ReadOnlySpan<char> content, bool isMultiline = false) {
        Write(content, isMultiline);
        WriteLine();
    }

   
   
   
   
    public void WriteLine([InterpolatedStringHandlerArgument("")] ref WriteInterpolatedStringHandler handler) {
        WriteLine();
    }

   
   
   
   
   
    public void WriteLineIf(bool condition, bool skipIfPresent = false) {
        if (condition) {
            WriteLine(skipIfPresent);
        }
    }

   
   
   
   
   
   
    public void WriteLineIf(bool condition, string content, bool isMultiline = false) {
        if (condition) {
            WriteLine(content.AsSpan(), isMultiline);
        }
    }

   
   
   
   
   
   
    public void WriteLineIf(bool condition, ReadOnlySpan<char> content, bool isMultiline = false) {
        if (condition) {
            Write(content, isMultiline);
            WriteLine();
        }
    }

   
   
   
   
   
    public void WriteLineIf(bool condition, [InterpolatedStringHandlerArgument("", nameof(condition))] ref WriteIfInterpolatedStringHandler handler) {
        if (condition) {
            WriteLine();
        }
    }

   
    public override string ToString() => builder.WrittenSpan.Trim().ToString();

   
   
   
   
    private void WriteRawText(ReadOnlySpan<char> content) {
        if (builder.Count == 0 || builder.WrittenSpan[^2..].SequenceEqual(DefaultNewLine.AsSpan())) {
            builder.AddRange(currentIndentation.AsSpan());
        }

        builder.AddRange(content);
    }

   
   
   
   
    public struct Block(IndentedTextWriter writer) : IDisposable {
       
       
       
        private IndentedTextWriter? writer = writer;

       
        public void Dispose() {
            IndentedTextWriter? writer = this.writer;

            this.writer = null;

            if (writer is not null) {
                writer.DecreaseIndent();
                writer.WriteLine("}");
            }
        }
    }

   
   
   
   
    [EditorBrowsable(EditorBrowsableState.Never)]
    [InterpolatedStringHandler]
    public readonly ref struct WriteInterpolatedStringHandler {
       
        private readonly IndentedTextWriter writer;

       
       
       
       
       
       
       
       
       
       
       
        public WriteInterpolatedStringHandler(int literalLength, int formattedCount, IndentedTextWriter writer) {
            this.writer = writer;
        }

       
       
        public void AppendLiteral(string value) {
            writer.Write(value);
        }

       
       
        public void AppendFormatted(string? value) {
            AppendFormatted<string?>(value);
        }

       
       
        public void AppendFormatted(ReadOnlySpan<char> value) {
            writer.Write(value);
        }

       
       
       
        public void AppendFormatted<T>(T value) {
            if (value is not null) {
                writer.Write(value.ToString());
            }
        }

       
       
       
       
        public void AppendFormatted<T>(T value, string? format) {
            if (value is IFormattable) {
                writer.Write(((IFormattable)value).ToString(format, CultureInfo.InvariantCulture));
            } else if (value is not null) {
                writer.Write(value.ToString());
            }
        }
    }

   
   
   
   
    [EditorBrowsable(EditorBrowsableState.Never)]
    [InterpolatedStringHandler]
    public readonly ref struct WriteIfInterpolatedStringHandler {
       
        private readonly WriteInterpolatedStringHandler handler;

       
       
       
       
       
       
       
       
       
       
       
       
       
        public WriteIfInterpolatedStringHandler(int literalLength, int formattedCount, IndentedTextWriter writer, bool condition, out bool shouldAppend) {
            if (condition) {
                handler = new WriteInterpolatedStringHandler(literalLength, formattedCount, writer);

                shouldAppend = true;
            } else {
                handler = default;

                shouldAppend = false;
            }
        }

       
        public void AppendLiteral(string value) {
            handler.AppendLiteral(value);
        }

       
        public void AppendFormatted(string? value) {
            handler.AppendFormatted(value);
        }

       
        public void AppendFormatted(ReadOnlySpan<char> value) {
            handler.AppendFormatted(value);
        }

       
        public void AppendFormatted<T>(T value) {
            handler.AppendFormatted(value);
        }

       
        public void AppendFormatted<T>(T value, string? format) {
            handler.AppendFormatted(value, format);
        }
    }
}
