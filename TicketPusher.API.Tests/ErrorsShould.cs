using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using TicketPusher.API.Utils;
using Xunit;

namespace TicketPusher.API.Tests
{
public sealed class ErrorsShould
    {
        [Fact]
        public void BeUnique()
        {
            List<MethodInfo> methods = typeof(Error)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.ReturnType == typeof(Error))
                .ToList();

            int numberOfUniqueCodes = methods.Select(x => GetErrorCode(x))
                .Distinct()
                .Count();

            numberOfUniqueCodes.Should().Be(methods.Count);
        }

        private string GetErrorCode(MethodInfo method)
        {
            object[] parameters = method.GetParameters()
                .Select<ParameterInfo, object>(x =>
                {
                    if (x.ParameterType == typeof(string))
                        return string.Empty;

                    if (x.ParameterType == typeof(long))
                        return 0;

                    throw new Exception();
                })
                .ToArray();

            var error = (Error)method.Invoke(null, parameters);
            return error.Code;
        }
    }
}