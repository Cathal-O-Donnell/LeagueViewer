using LeagueViewer.Models;

namespace LeagueViewer.Helpers
{
    public class FixtureHelper
    {
        private const string GoalString = "GOAL";
        private const string PenaltyString = "PENALTY";
        private const string CardString = "CARD";
        private const string SubString = "SUBST";

        public string GetFixtureEventHtml(FixtureEvent fixtureEvent)
        {
            string htmlString = string.Empty;

            if (fixtureEvent.Type.ToUpper() == GoalString)
            {
                if (fixtureEvent.Detail.ToUpper() == PenaltyString)
                    htmlString = $"<p>{fixtureEvent.Elapsed}&#39; <span>Goal (P)</span> {fixtureEvent.PlayerName}</p>";
                else
                    htmlString = $"<p>{fixtureEvent.Elapsed}&#39; <span>Goal</span> {fixtureEvent.PlayerName}</p>";
            }
            else if (fixtureEvent.Type.ToUpper() == CardString)
            {
                string cardClass = "referee-card-red";
                if (fixtureEvent.Detail.ToUpper().Contains("YELLOW")) cardClass = "referee-card-yellow";

                htmlString = $"<p>{fixtureEvent.Elapsed}&#39; <span class='referee-card {cardClass}'></span> {fixtureEvent.PlayerName}</p>";
            }
            else if (fixtureEvent.Type.ToUpper() == SubString)
                htmlString = $"<p>{fixtureEvent.Elapsed}&#39; <span>Sub</span> <span class='text-success'>in</span> {fixtureEvent.PlayerName} <span class='text-danger'>out</span> {fixtureEvent.Assist}</p>";

            return htmlString;
        }
    }
}
