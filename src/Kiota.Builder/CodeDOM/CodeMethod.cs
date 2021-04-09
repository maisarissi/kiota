﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Kiota.Builder
{
    public enum CodeMethodKind
    {
        Custom,
        IndexerBackwardCompatibility,
        RequestExecutor
    }

    public class CodeMethod : CodeTerminal, ICloneable
    {
        public CodeMethod(CodeElement parent): base(parent)
        {
            
        }
        public CodeMethodKind MethodKind {get;set;} = CodeMethodKind.Custom;
        public AccessModifier Access {get;set;} = AccessModifier.Public;
        public CodeTypeBase ReturnType {get;set;}
        public List<CodeParameter> Parameters {get;set;} = new List<CodeParameter>();
        public bool IsStatic {get;set;} = false;
        public bool IsAsync {get;set;} = true;

        public object Clone()
        {
            return new CodeMethod(Parent) {
                MethodKind = MethodKind,
                ReturnType = ReturnType.Clone() as CodeTypeBase,
                Parameters = Parameters.Select(x => x.Clone() as CodeParameter).ToList(),
                Name = Name.Clone() as string,
            };
        }

        internal void AddParameter(params CodeParameter[] methodParameters)
        {
            if(!methodParameters.Any() || methodParameters.Any(x => x == null))
                throw new ArgumentOutOfRangeException(nameof(methodParameters));
            AddMissingParent(methodParameters);
            Parameters.AddRange(methodParameters);
        }
    }
}
