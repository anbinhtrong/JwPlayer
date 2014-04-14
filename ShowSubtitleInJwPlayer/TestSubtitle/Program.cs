using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;
using LogicLayer.Services;
using LogicLayer.Models;

namespace TestSubtitle
{
    class Program
    {
        static void Main(string[] args)
        {
            ISubTitleService subTitleService = new SubTitleService();
            var enSubTitle = subTitleService.ReadFromFile("en.srt");
            PrintFile(enSubTitle);
            var cnSubTitle = subTitleService.ReadFromFile("cn.srt");
            PrintFile(cnSubTitle);
        }

        static void PrintFile(List<SubtitleModel> subtitles)
        {
            foreach (var subtitle in subtitles)
            {
                Console.WriteLine(subtitle.Id);
                Console.WriteLine(subtitle.StartTime + "---" + subtitle.EndTime);
                Console.WriteLine(subtitle.Caption);
                Console.WriteLine();
            }
        }
    }
}
