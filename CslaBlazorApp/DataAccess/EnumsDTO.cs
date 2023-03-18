using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess {
    public enum EnumApprovedBy {
        [Display(Name = "Assemblée générale")]
        GeneralAssembly = 1,
        
        [Display(Name = "Chambre française")]
        FrenchChamber = 2,
        
        [Display(Name = "Chambre néerlandaise")]
        DutchChamber = 3
    }

    public enum EnumPublicationType {
        [Display(Name = "Rapport particulier")]
        ParticularReport = 1,
       
        [Display(Name = "Communication annuelle de commentaires")]
        AnnualPostingOfComments = 2,
        
        [Display(Name = "Projets collaboratifs")]
        Collaborativeprojects = 3,
       
        [Display(Name = "Rapport particulier pour la Communauté française")]
        ParticularReportToFrenchCommunityCommission = 4,
        
        [Display(Name = "Rapport particulier pour Bruxelles-Capitale")]
        ParticularReportToBrusselsCapitalParliament = 5,
        
        [Display(Name = "Rapport particulier pour la Commission communautaire commune")]
        ParticularReportToCommonCommunityCommission = 6,
        
        [Display(Name = "Comptes annuels")]
        AnnualBooks = 7,
        
        [Display(Name = "Rapport d'activité de la chambre néerlandaise pour le Parlement flamand")]
        ActivityReportOfDutchChamberToFlemishParliament = 8,
        
        [Display(Name = "Rapport en forme de lettre")]
        ReportsInLetterForm = 9,
        
        [Display(Name = "Rapport individuel")]
        IndividualReport = 10

    }
    public enum EnumLegislativeLevel {
        [Display(Name = "État fédéral")]
        FederalState = 1,
        
        [Display(Name = "Communauté flamande")]
        FlemishCommunity = 2,
        
        [Display(Name = "Communauté française")]
        FrenchCommunity = 3,
        
        [Display(Name = "Communauté germanophone")]
        GermanSpeakingCommunity = 4,
        
        [Display(Name = "Région wallonne")]
        WalloonRegion = 5,
        
        [Display(Name = "Région de Bruxelles-Capitale")]
        BrusselsCapitalRegion = 6,
        
        [Display(Name = "Provinces")]
        Provinces = 7

    }

}
