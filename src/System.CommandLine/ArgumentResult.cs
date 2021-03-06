﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;

namespace System.CommandLine
{
    public class ArgumentResult : SymbolResult
    {
        private ArgumentConversionResult _conversionResult;

        internal ArgumentResult(
            IArgument argument,
            Token token,
            SymbolResult parent) : base(argument, token, parent)
        {
            Argument = argument;
        }

        public IArgument Argument { get; }

        internal override ArgumentConversionResult ArgumentConversionResult =>
            _conversionResult ??= Convert(this, Argument);

        public override string ToString() => $"{GetType().Name} {Argument.Name}: {string.Join(" ", Tokens.Select(t => $"<{t.Value}>"))}";
    }
}
