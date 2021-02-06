﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MimeKit;
using Nytte.Email.Core;
using Razor.Templating.Core;

namespace Nytte.Email.Razor
{
    public class RazorEmailMessageBuilder : IRazorEmailMessageBuilder
    {
        public MimeMessage BuildMessage<T>(T messageBlueprint) where T : IEmailServiceMessageBlueprint
        {
            throw new System.NotImplementedException();
        }

        public async Task<MimeMessage> BuildMessageAsync<T>(T messageBlueprint) where T : IEmailServiceMessageBlueprint
        {
            var blueprint = messageBlueprint as RazorEmailMessageBlueprint;
            if (blueprint is null)
                throw new UnsupportedEmailServiceMessageBlueprintTypeException(typeof(T),
                    new[] {typeof(RazorEmailMessageBlueprint)});

            MimeMessage email = new MimeMessage();

            email.To.Add(new MailboxAddress(blueprint.RecipientName, blueprint.RecipientEmailAddress));
            email.From.Add(new MailboxAddress("Rob", "robertbennett1998@outlook.com"));
            email.Subject = blueprint.EmailSubject;

            
            var bodyBuilder = new BodyBuilder();
            if (blueprint.RazorViewModelType is not null)
            {
                var renderMethodInfo = typeof(RazorTemplateEngine).GetMethods()
                                                                    .Where(x => x.Name == "RenderAsync")
                                                                    .FirstOrDefault(x => x.IsGenericMethod);
                if (renderMethodInfo is not null)
                {

                    var renderMethodInstance = renderMethodInfo.MakeGenericMethod(blueprint.RazorViewModelType);
                    // ReSharper disable once PossibleNullReferenceException

                    bodyBuilder.HtmlBody = await (Task<string>) renderMethodInstance.Invoke(null,
                        new[] {blueprint.RazorViewName, blueprint.RazorViewModel});
                }
            }
            else
            {
                bodyBuilder.HtmlBody = await RazorTemplateEngine.RenderAsync(blueprint.RazorViewName);
            }
            
            email.Body = bodyBuilder.ToMessageBody();
            
            return email;
        }
    }
}