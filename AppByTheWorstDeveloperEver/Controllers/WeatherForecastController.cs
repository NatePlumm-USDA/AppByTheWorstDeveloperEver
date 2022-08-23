using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppByTheWorstDeveloperEver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [ThreadStatic]
        private object MyLock = new object(); // unused and named improperly
        

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            RegularExpressionFooClass c = new RegularExpressionFooClass();

            bool x = c.Foo(
                RegexOptions.Compiled,
                TimeSpan.FromSeconds(3),
                "aowejg",
                "aoiwegj",
                match => "false");

            SetSomeCookies();
            DoSomething();

            // This might show up as a logic bomb
            DoSomethingElse(DateTime.UtcNow.Ticks);

            if (DateTime.UtcNow.Ticks > 325)
            {
                // logic bomb?
                System.IO.File.Delete("Z:\\does-not-exist.txt");
            }

            Random rng = new Random(); // this should show up as weak in code scans

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)] +
                              " animal of the day = " +
                              UnstableCode().FirstOrDefault() +
                              $" x={x}"
                })
                .ToArray();

        }

        private void DoSomething()
        {
            // This should show up as an ignored exception and perhaps unused variable (ex)

            try
            {
                for (int i = 10; i < 10; i++) // impossible to enter foreach
                {
                    while (true) // never ending loop
                    {
                        Console.WriteLine("hello world");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void DoSomethingElse(long someArg)
        {
            // This should show up as an ignored exception and perhaps unused variable (ex)
            if (someArg > 1)
            {
                // Create an exception but don't throw it... should show up as a bug
                new Exception("blah blah");
            }

            switch (someArg)
            {
                case 1:
                    SomeNonFlagsEnum flags =
                        SomeNonFlagsEnum.Fantastic |
                        SomeNonFlagsEnum.Something |
                        SomeNonFlagsEnum.SomethingAgain |
                        SomeNonFlagsEnum.SomethingElse |
                        (SomeNonFlagsEnum) someArg;

                    throw new Exception($"blah 1 {flags}");
                // code smell - default order
                default:
                    throw new Exception("blah 2");
                case 2:
                    throw new Exception("blah 3");
            }
        }

        enum SomeNonFlagsEnum
        {
            Fantastic = 0, // code smell? should be none/undefined?
            Something = 1,
            SomethingElse = 2,
            SomethingAgain = 3,
        }

        private List<string> UnstableCode()
        {
            List<string> animals = new List<string> {"cat", "dog"};

            // In debug we end up with just cat, in release we have cat and dog
            Debug.Assert(animals.Remove("dog"));

            return animals;
        }

        private void SetSomeCookies()
        {
            Response.Headers.Add("Set-Cookie", "blah=blah-blah");
            Response.Cookies.Append("blah", "blah-blah");
        }


        [HttpGet()]
        public IActionResult InsecureRedirect(string url)
        {
            // input isn't checked at all
            return Redirect(url);
        }

        [HttpGet]
        public IActionResult InsecureDeserialization(string typeName)
        {
            //
            // Types are all mixed up here and not checked
            //

            Type t = Type.GetType(typeName);
            XmlSerializer serializer = new XmlSerializer(t);
            string obj = (string)serializer.Deserialize(new StreamReader(new MemoryStream()));

            return Ok();
        }

        [HttpGet]
        public IActionResult SqlInjectionIssues(string dirtyInput)
        {
            SqlConnection conn = new SqlConnection("fake-connection-string");
            conn.Open();

            using SqlCommand cmd = conn.CreateCommand();
			// SQL Injection
            cmd.CommandText = "SELECT * FROM TABLE WHERE USERNAME = '" + dirtyInput + "'";
            object obj = cmd.ExecuteScalar();

            return Ok(obj);
        }

        [HttpGet]
        public IActionResult InsecureCiphers()
        {
            SymmetricAlgorithm[] algorithms =
            {
                new TripleDESCryptoServiceProvider(),
                new DESCryptoServiceProvider(),
                new RC2CryptoServiceProvider(),
            };

            foreach (SymmetricAlgorithm badAlg in algorithms)
            {
                ICryptoTransform transformer = badAlg.CreateEncryptor();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, transformer, CryptoStreamMode.Write))
                    using (StreamWriter writer = new StreamWriter(cryptoStream))
                    {
                        writer.Write("hello world");
                    }

                    // unused variable
                    byte[] encrypted = ms.ToArray();
                }
            }

            return Ok();
        }
    }
}


class AClassWithoutANameSpace
{
    public string X { get; set; }

    private AClassWithoutANameSpace()
    {
        // now the class can't even be created traditionally
    }
}
