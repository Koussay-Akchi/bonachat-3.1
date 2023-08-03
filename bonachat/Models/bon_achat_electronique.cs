namespace bonachat.Models
{
    public class BonAchatElectronique
    {
        public int Id { get; set; }
        public int IdUtilisateur { get; set; }
        public decimal SoldeCarteUtilisateurAvant { get; set; }
        public decimal SoldeCarteUtilisateurApres { get; set; }
        public DateTime DateGeneration { get; set; }
        public string CodeEtat { get; set; }
        public string CodeType { get; set; }
        public decimal ValeurInitiale { get; set; }
        public decimal ValeurRestante { get; set; }
        public string EmailBeneficiaire { get; set; }
        public string TelephoneBeneficiaire { get; set; }
        public string CodeSource { get; set; }
        public string Couleur { get; internal set; }
    }
}
