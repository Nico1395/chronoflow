﻿namespace ChronoFlow.Client.Common.Browser;

public interface IBrowserLogger
{
    public ValueTask LogAsync(string message);
    public ValueTask LogAsync(Exception ex);
}
