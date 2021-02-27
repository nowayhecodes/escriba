using System;
using System.Collections;
using System.Collections.Generic;

namespace Escriba
{
    public static class Constants
    {
        static string DEFAULT_LOG_FILE_NAME = "escriba.log";

        public enum LoggerModes
        {
            Console,
            Custom,
            File,
            Off,
        }

        public enum LogColor
        {
            GREEN,
            MAGENTA,
            YELLOW,
            RED,
        }

        public enum LogPrefix
        {
            INFO,
            IMPORTANT,
            WARNING,
            ERROR,
        }

        public static Array LoggerModesOptions = new object[]
        {
            LoggerModes.Console,
            LoggerModes.Custom,
            LoggerModes.File,
            LoggerModes.Off
        };
    }
}