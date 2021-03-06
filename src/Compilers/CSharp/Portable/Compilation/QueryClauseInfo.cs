﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;
using System;

namespace Microsoft.CodeAnalysis.CSharp
{
    /// <summary>
    /// Semantic information associated with a query clause in a C# query expression.
    /// </summary>
    public struct QueryClauseInfo : IEquatable<QueryClauseInfo>
    {
        private readonly SymbolInfo _castInfo;
        private readonly SymbolInfo _operationInfo;

        internal QueryClauseInfo(SymbolInfo castInfo, SymbolInfo operationInfo)
        {
            _castInfo = castInfo;
            _operationInfo = operationInfo;
        }

        /// <summary>
        /// The .Cast&lt;T&gt;() operation generated from the query range variable's type restriction.
        /// If you want the type restriction, when this is non-null use Cast.TypeArguments[0].
        /// </summary>
        public SymbolInfo CastInfo
        {
            get { return _castInfo; }
        }

        /// <summary>
        /// The operation (e.g. Select(), Where(), etc) that implements the given clause.
        /// If it is an extension method, it is returned in reduced form.
        /// </summary>
        public SymbolInfo OperationInfo
        {
            get { return _operationInfo; }
        }

        public override bool Equals(object obj)
        {
            return obj is QueryClauseInfo && Equals((QueryClauseInfo)obj);
        }

        public bool Equals(QueryClauseInfo other)
        {
            return _castInfo.Equals(other._castInfo)
                && _operationInfo.Equals(other._operationInfo);
        }

        public override int GetHashCode()
        {
            return Hash.Combine(this.CastInfo.GetHashCode(), _operationInfo.GetHashCode());
        }
    }
}
