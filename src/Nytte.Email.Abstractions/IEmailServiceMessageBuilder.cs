﻿using System.Threading.Tasks;
using MimeKit;

namespace Nytte.Email.Abstractions
{
    public interface IEmailServiceMessageBuilder
    {
        MimeMessage BuildMessage<T>(T messageBlueprint) where T : IEmailServiceMessageBlueprint;
        Task<MimeMessage> BuildMessageAsync<T>(T messageBlueprint) where T : IEmailServiceMessageBlueprint;
    }
}