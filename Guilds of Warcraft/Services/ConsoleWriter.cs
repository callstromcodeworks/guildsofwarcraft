/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using System.Text;

namespace CCW.GoW.Services;

/// <summary>
/// Simple text writer that outputs text to the selected textbox (thread-safe)
/// </summary>
public class ConsoleWriter : TextWriter
{
    private readonly TextBox console;

    public ConsoleWriter(TextBox console) => this.console = console;

    public override void Write(char value)
    {
        if (console.InvokeRequired) console.Invoke(() => console.Text += value);
        else console.Text += value;
    }

    public override void WriteLine(string? value)
    {
        if (console.InvokeRequired) console.Invoke(() => console.AppendText($"{value}{Environment.NewLine}"));
        else console.AppendText($"{value}{Environment.NewLine}");
    }
    public override Encoding Encoding { get { return Encoding.UTF8; } }
}
