namespace StatLeadersUfcComScraper
{
    public class Fight
    {
        public FightItem FastestFinish { get; set; }
        public FightItem FastestKOTKO { get; set; }
        public FightItem FastestSubmission { get; set; }
        public FightItem LatestFinish { get; set; }
        public FightItem LatestKOTKO { get; set; }
        public FightItem LatestSubmission { get; set; }
        public FightItem Knockdowns { get; set; }
        public FightItem SignificantStrikesDifferential { get; set; }
        public FightItem LargestComebackFinish { get; set; }
        public FightItem SigStrikesLanded { get; set; }
        public FightItem SigStrikesAttempted { get; set; }
        public FightItem SigStrikesAccuracy { get; set; }
        public FightItem SigDistanceStrikesLanded { get; set; }
        public FightItem SigClinchStrikesLanded { get; set; }
        public FightItem SigGroundStrikesLanded { get; set; }
        public FightItem SigHeadStrikesLanded { get; set; }
        public FightItem SigBodyStrikesLanded { get; set; }
        public FightItem LegKicksLanded { get; set; }
        public FightItem TotalStrikesLanded { get; set; }
        public FightItem TotalStrikesAttempted { get; set; }
        public FightItem TotalClinchStrikesLanded { get; set; }
        public FightItem TotalGroundStrikesLanded { get; set; }
        public FightItem TotalHeadStrikesLanded { get; set; }
        public FightItem TotalBodyStrikesLanded { get; set; }
        public FightItem TotalLegStrikesLanded { get; set; }
        public FightItem TakedownsLanded { get; set; }
        public FightItem TakedownsAttempted { get; set; }
        public FightItem TakedownsAccuracy { get; set; }
        public FightItem SubmissionsAttempted { get; set; }
    }
}