using System.ComponentModel.DataAnnotations;

namespace Football.Web.Models
{
    public class MatchPredictionViewModel
    {
        [Required]
        [Display(Name = "Home team name")]
        public string HomeTeamName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Away team name")]
        public string AwayTeamName { get; set; } = string.Empty;

        [Display(Name = "Home goals for (avg)")]
        [Range(0, 10)]
        public float HomeGoalsForAvg { get; set; }

        [Display(Name = "Home goals against (avg)")]
        [Range(0, 10)]
        public float HomeGoalsAgainstAvg { get; set; }

        [Display(Name = "Away goals for (avg)")]
        [Range(0, 10)]
        public float AwayGoalsForAvg { get; set; }

        [Display(Name = "Away goals against (avg)")]
        [Range(0, 10)]
        public float AwayGoalsAgainstAvg { get; set; }

        [Display(Name = "Home Over 2.5 rate")]
        [Range(0, 1, ErrorMessage = "Value must be between 0 and 1")]
        public float HomeOver25Rate { get; set; }

        [Display(Name = "Away Over 2.5 rate")]
        [Range(0, 1, ErrorMessage = "Value must be between 0 and 1")]
        public float AwayOver25Rate { get; set; }

        // Rezultat
        public bool? IsOver25 { get; set; }
        public float? Score { get; set; }
    }
}
