using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LogicLayer.Models;
using System.Globalization;

namespace LogicLayer.Services
{
    public class SubTitleService : ISubTitleService
    {
        public List<SubtitleModel> ReadFromFile(string path)
        {
            var result = new List<SubtitleModel>();
            var lines = ReadAllLine(path);
            var counter = 0;
            var index = 0;
            var isCorrect = true;
            while (index < lines.Count)
            {
                var subtitle = new SubtitleModel();
                var line = lines[index];
                line.Trim();
                //Read number in subtitle
                counter++;
                if (!string.IsNullOrEmpty(line))
                {
                    try
                    {
                        var number = int.Parse(line);
                        if (counter != number)
                        {
                            isCorrect = false;
                        }
                        else
                        {
                            subtitle.Id = number;
                        }
                    }
                    catch
                    {
                        isCorrect = false;
                    }
                }
                //Read begin time and end time
                if (isCorrect)
                {
                    try
                    {
                        index++;
                        line = lines[index];
                        var startTime = line.Substring(0, 12);
                        var endTime = line.Substring(line.Length - 12, 12);
                        subtitle.StartTime = TimeSpan.ParseExact(startTime, @"h\:mm\:ss\,fff", CultureInfo.InvariantCulture);
                        subtitle.EndTime = TimeSpan.ParseExact(endTime, @"h\:mm\:ss\,fff", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        isCorrect = false;
                    }
                }
                //Read caption
                if (isCorrect)
                {
                    index++;
                    line = lines[index].Trim();
                    var text = string.Empty;
                    while (!string.IsNullOrEmpty(line))
                    {
                        text += line + "\r\n";
                        index++;
                        if (index < lines.Count)
                        {
                            line = lines[index].Trim();
                        }
                        else
                        {
                            line = "";
                        }
                    }
                    subtitle.Caption = text;
                }
                if (isCorrect)
                {
                    result.Add(subtitle);
                }
                index++;
            }
            return result;
        }

        private List<string> ReadAllLine(string path)
        {
            return File.ReadAllLines(path).ToList();
        }

        public Message ExportToFile(List<SubtitleModel> subtitles, string path)
        {
            if (File.Exists(path))
            {
                var message = new Message
                {
                    IsSuccess = false,
                    Content = "Cannot create file"
                };
                return message;
            }
            using (var sw = File.CreateText(path))
            {
                foreach (var subtitle in subtitles)
                {
                    sw.WriteLine(subtitle.Id);
                    sw.WriteLine(subtitle.StartTime.ToString(@"hh\:mm\:ss\,fff") + " --> " + subtitle.EndTime.ToString(@"hh\:mm\:ss\,fff"));
                    sw.WriteLine(subtitle.Caption);
                }
            }
            return new Message
            {
                IsSuccess = true,
                Content = "File exports successfully"
            };
        }
    }
}
