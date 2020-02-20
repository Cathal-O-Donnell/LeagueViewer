using LeagueViewer.Models;
using System.Collections.Generic;

namespace LeagueViewer.Helpers
{
    public class LeagueHelper
    {
        private const string PromotionString = "PROMOTION";
        private const string ChampionshipString = "CHAMPIONSHIP";
        private const string RelegationString = "RELEGATION";

        public List<Enums.Result> GetTeamForm(string forme)
        {
            var form = new List<Enums.Result>();
            var formArr = forme.ToCharArray();

            foreach (var result in formArr)
            {
                switch (result)
                {
                    case 'L':
                        form.Add(Enums.Result.Loss);
                        break;
                    case 'D':
                        form.Add(Enums.Result.Draw);
                        break;
                    case 'W':
                        form.Add(Enums.Result.Win);
                        break;
                }
            }

            return form;
        }

        public string GetTableRowClass(string description)
        {
            if (string.IsNullOrEmpty(description))
                return null;
            else if (description.ToUpper().Contains(PromotionString) || (description.ToUpper().Contains(ChampionshipString) && !description.ToUpper().Contains(RelegationString)))
                return "table-success";
            else if (description.ToUpper().Contains(RelegationString))
                return "table-danger";
            else return "";
        }
    }
}
