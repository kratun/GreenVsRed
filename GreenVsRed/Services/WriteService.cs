﻿// <copyright file="WriteService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Services
{
    using System;

    /// <summary>
    /// Provide methods that write.
    /// </summary>
    public class WriteService : IWrite
    {
        /// <summary>
        /// Writes the specified string value to the standard output stream.
        /// </summary>
        /// <param name="outputMsg">The value to write.</param>
        public void Write(string outputMsg)
        {
            Console.Write(outputMsg.Trim());
        }

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to
        ///     the standard output stream.
        /// </summary>
        /// <param name="outputMsg">The value to write.</param>
        public void WriteLine(string outputMsg)
        {
            Console.WriteLine(outputMsg.Trim());
        }
    }
}
