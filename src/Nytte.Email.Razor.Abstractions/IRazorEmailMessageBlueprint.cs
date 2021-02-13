﻿using System;
using Nytte.Email.Abstractions;

namespace Nytte.Email.Razor.Abstractions
{
    public interface IRazorEmailMessageBlueprint : IEmailServiceMessageBlueprint
    {
        string RazorViewName { get; }
        object RazorViewModel { get; }
        Type RazorViewModelType { get; }
    }
}