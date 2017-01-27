using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers.ResultModels {
    public class StatisticsResultModel {
        public string[] MostPopularSentences { get; set; }
        public string[] MostRecentSentences { get; set; }
        public int TotalSentences { get; set; }
    }
}
