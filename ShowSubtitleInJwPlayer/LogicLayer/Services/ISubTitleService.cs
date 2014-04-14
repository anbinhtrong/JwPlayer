using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Models;

namespace LogicLayer.Services
{
    public interface ISubTitleService
    {
        List<SubtitleModel> ReadFromFile(string path);
        Message ExportToFile(List<SubtitleModel> subtitles, string path);
    }
}
