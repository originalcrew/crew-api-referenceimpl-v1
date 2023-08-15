using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crew.Api.ReferenceImpl.V1.UnitTests
{
    [TestClass]
    public class SolutionTests
    {
        private const string AssemblyName = "Crew.Api.ReferenceImpl.V1.dll";

        [TestMethod]
        public void EnumsAreNotPlural()
        {
            // act
            /* force the Crew.Api.ReferenceImpl.V1.dll to be loaded into the CurrentDomain */
            new Program();

            /* get the enums in our assembly that haven't been code generated */
            IList<Type> enums = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.ManifestModule.Name == AssemblyName)
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsEnum && !t.IsDefined(typeof(GeneratedCodeAttribute), false))
                .ToList();

            // assert
            foreach (Type @enum in enums)
            {
                /* if the enum doesn't end with "us" eg. status or "ass" eg. class, then assert that the enum doesn't end with "s" */
                if (!@enum.Name.EndsWith("us") && !@enum.Name.EndsWith("ass"))
                {
                    Assert.IsFalse(@enum.Name.EndsWith("s"), $"enum '{@enum.FullName}' must not be plural");
                }
            }
        }

        [TestMethod]
        public void ControllerClassesHaveAttributes()
        {
            // act
            /* get the controllers in our assembly */
            IList<Type> controllers = GetControllers();

            // assert
            foreach (Type controller in controllers)
            {
                /* that the class has all the attributes we want */
                bool hasApiControllerAttribute = controller.IsDefined(typeof(ApiControllerAttribute), true);
                bool hasResponseCacheAttribute = controller.IsDefined(typeof(ResponseCacheAttribute), true);

                Assert.IsTrue(hasApiControllerAttribute, $"{controller.Name} - requires [ApiController] attribute.");
                Assert.IsTrue(hasResponseCacheAttribute, $"{controller.Name} - requires [ResponseCache] attribute.");

                /* that those attributes have the props we want */
                ResponseCacheAttribute responseCacheAttribute = controller.GetCustomAttributes<ResponseCacheAttribute>(true).Single();

                Assert.AreEqual(
                    "No",
                    responseCacheAttribute.CacheProfileName,
                    $"{controller.Name} - [ResponseCache] attribute requires CacheProfileName = \"No\".");
            }
        }

        [TestMethod]
        [Ignore]
        public void GenerateDiagnosticsKey()
        {
            /* The GetRandomFileName method returns a cryptographically strong, random string, in rrrrrrrr.rrr format :-) */

            /* generate a key */
            var key = new StringBuilder(35)
                .Append(Path.GetRandomFileName().Replace(".", null).Substring(0, 10))
                .Append(Path.GetRandomFileName().Replace(".", null).Substring(0, 10))
                .Append(Path.GetRandomFileName().Replace(".", null).Substring(0, 10))
                .Append(Path.GetRandomFileName().Replace(".", null).Substring(0, 5))
                .ToString();

            Console.Out.WriteLine("key = {0}", key);
        }

        private static IList<Type> GetControllers()
        {
            /* force the Crew.Api.ReferenceImpl.V1.dll to be loaded into the CurrentDomain */
            new Program();

            /* get the controllers in our assembly */
            IList<Type> controllers = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.ManifestModule.Name == AssemblyName)
                .SelectMany(a => a.GetTypes())
                .Where(
                    t => t.IsClass &&
                         !t.IsAbstract &&
                         t.IsSubclassOf(typeof(ControllerBase)))
                .ToList();

            return controllers;
        }
    }
}