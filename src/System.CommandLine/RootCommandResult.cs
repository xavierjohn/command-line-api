﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;

namespace System.CommandLine
{
    internal class RootCommandResult : CommandResult
    {
        private Dictionary<IArgument, ArgumentResult> _allArgumentResults;
        private Dictionary<ICommand, CommandResult> _allCommandResults;
        private Dictionary<IOption, OptionResult> _allOptionResults;

        public RootCommandResult(
            ICommand command,
            Token token) : base(command, token)
        {
        }

        internal override RootCommandResult Root => this;

        private void EnsureResultMapsAreInitialized()
        {
            if (_allArgumentResults != null)
            {
                return;
            }

            _allArgumentResults = new Dictionary<IArgument, ArgumentResult>();
            _allCommandResults = new Dictionary<ICommand, CommandResult>();
            _allOptionResults = new Dictionary<IOption, OptionResult>();

            foreach (var symbolResult in this.AllSymbolResults())
            {
                switch (symbolResult)
                {
                    case ArgumentResult argumentResult:
                        _allArgumentResults.Add(argumentResult.Argument, argumentResult);
                        break;
                    case CommandResult commandResult:
                        _allCommandResults.Add(commandResult.Command, commandResult);
                        break;
                    case OptionResult optionResult:
                        _allOptionResults.Add(optionResult.Option, optionResult);
                        break;
                }
            }
        }

        public ArgumentResult FindResultFor(IArgument argument)
        {
            EnsureResultMapsAreInitialized();

            _allArgumentResults.TryGetValue(argument, out var result);

            return result;
        }

        public CommandResult FindResultFor(ICommand command)
        {
            EnsureResultMapsAreInitialized();

            _allCommandResults.TryGetValue(command, out var result);

            return result;
        }

        public OptionResult FindResultFor(IOption option)
        {
            EnsureResultMapsAreInitialized();

            _allOptionResults.TryGetValue(option, out var result);

            return result;
        }
    }
}
