﻿using System;

namespace LoginStat.Common.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string name, string id)
            : base($"Entity {name} with id ({id}) was not found.")
        {
        }

        public NotFoundException(string name) : base($"Entity {name} was not found.")
        {
        }
    }
}