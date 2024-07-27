﻿using System;

namespace Common.Logger
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message, Exception ex);
    }
}
