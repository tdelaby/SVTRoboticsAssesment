using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using SVTRoboticsAssesment.Models;

namespace SVTRoboticsAssesment.Controllers
{
    [ApiController]
    [Route("api/robots/[controller]/")]
    public class ClosestController : ControllerBase
    {
        string url = "https://60c8ed887dafc90017ffbd56.mockapi.io/robots";

        private readonly ILogger<ClosestController> _logger;

        public ClosestController(ILogger<ClosestController> logger)
        {
            _logger = logger;
        }

        // GET: api/robots/Closest
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/robots/Closest
        [HttpPost]
        public IActionResult Post([FromBody] LoadInfo load)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();

                    }
                }
            }
            JArray data = JArray.Parse(content);
            var robotsAvailable = data.ToObject<List<Robot>>();
            Robot closestRobot = new Robot();
            List<Robot> closestRobots = new List<Robot>();
            int index = 0;

            foreach (Robot robot in robotsAvailable)
            {
                robot.DistanceToGoal = Math.Sqrt(Math.Pow((load.X - robot.X), 2) + Math.Pow((load.Y - robot.Y), 2));

                if(index == 0)
                {
                    closestRobot = robot;
                    if(robot.DistanceToGoal <= 10)
                    {
                        closestRobots.Add(robot);
                    }
                }
                else if(robot.DistanceToGoal < closestRobot.DistanceToGoal)
                {
                    closestRobot = robot;
                    if (robot.DistanceToGoal <= 10)
                    {
                        closestRobots.Add(robot);
                    }
                }

                index++;
            }

            foreach(Robot robot in closestRobots) 
            {
                if (robot.BatteryLevel > closestRobot.BatteryLevel)
                {
                    closestRobot = robot;
                }
            }

            return Ok(new
            {
                robotId = closestRobot.RobotId,
                distanceToGoal = closestRobot.DistanceToGoal,
                batteryLevel = closestRobot.BatteryLevel
            });
        }
    }
}
