using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace JmeterLoadTestGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Config
            var numberOfRequests = 3000;
            var doctorIds = new[] { 1, 2, 3 };
            var timeStamps = new[]
            {
                "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "13:00", "13:30",
                "14:00", "14:30", "15:00", "15:30", "16:00", "16:30"
            };
            var outputDirectory = $@"C:\Temp\LoadTests\{Guid.NewGuid()}\";
            var requestNumber = 1;

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            Console.WriteLine("Start generating request files.");

            foreach (var doctorId in doctorIds)
            {
                var date = GetNextWeekday(DateTime.UtcNow.Date);
                var timeStampIndex = 0;

                for (int i = 0; i < numberOfRequests / doctorIds.Length; i++)
                {
                    var request = new Request
                    {
                        SelectedConsultantId = doctorId,
                        SelectedDate = date,
                        SelectedPatientId = 1,
                        SelectedTime = timeStamps[timeStampIndex]
                    };

                    var json = JsonConvert.SerializeObject(request);
                    File.WriteAllText($"{outputDirectory}{requestNumber}.txt", json);
                    Console.WriteLine($"Saved request file {requestNumber}");

                    requestNumber++;
                    timeStampIndex++;
                    if (timeStampIndex + 1 > timeStamps.Length)
                    {
                        date = GetNextWeekday(date);
                        timeStampIndex = 0;
                    }
                }    
            }

            var fileNames = Enumerable.Range(1, numberOfRequests);
            File.WriteAllText($"{outputDirectory}index.csv", string.Join(Environment.NewLine, fileNames));

            Console.WriteLine("Finished generating request files.");
        }

        private static DateTime GetNextWeekday(DateTime date)
        {
            do
            {
                date = date.AddDays(1);
            } while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);

            return date;
        }
    }
}
