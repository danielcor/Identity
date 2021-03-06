﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Identity.Service.Mvc
{
    public class QueryResult : IActionResult
    {
        public QueryResult(string redirectUri, IEnumerable<KeyValuePair<string, string>> responseParameters)
        {
            RedirectUri = redirectUri;
            ResponseParameters = responseParameters;
        }

        public IEnumerable<KeyValuePair<string, string>> ResponseParameters { get; }
        public string RedirectUri { get; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var generator = context.HttpContext.RequestServices.GetRequiredService<QueryResponseGenerator>();
            generator.GenerateResponse(context.HttpContext, RedirectUri, ResponseParameters);

            return Task.CompletedTask;
        }
    }
}
