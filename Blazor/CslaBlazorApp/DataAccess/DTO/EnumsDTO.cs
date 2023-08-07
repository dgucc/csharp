using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess;

public enum EnumLanguageCode
{
    French = 1,
    Dutch = 2,
    German = 3,
    English = 4
}

#region Enums Publications
public enum EnumApprovedBy
{
    [Display(Name = "Assemblée générale")]
    GeneralAssembly = 1,

    [Display(Name = "Chambre française")]
    FrenchChamber = 2,

    [Display(Name = "Chambre néerlandaise")]
    DutchChamber = 3
}

public enum EnumPublicationType
{
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

public enum EnumLegislativeLevel
{
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
#endregion

#region Enums Documents
public enum EnumDocumentType
{
    [Display(Name = "Rapport")]
    Report = 1,

    [Display(Name = "Synthèse")]
    Summary = 2,

    [Display(Name = "Communiqué de presse")]
    PressRelease = 3,

    [Display(Name = "Abstrait")]
    Abstract = 4,

    [Display(Name = "Conclusions et recommandations")]
    ConclusionsAndRecommendations = 5,

    [Display(Name = "Annexe")]
    Attachment = 6,

    [Display(Name = "Divers")]
    Misc = 7,

    [Display(Name = "Photo de couverture")]
    CoverPhoto = 8
}

#endregion

#region Enums Topics
public enum EnumTopicType
{

    [Display(Name = "Innovation")]
    Innovation = 1,

    [Display(Name = "Médias")]
    Media = 2,

    [Display(Name = "Aménagement du territoire, politique du logement et patrimoine immobilier")]
    TerritoryDevelopmentAndHousingPolicyAndRealEstate = 3,

    [Display(Name = "Budget")]
    Budget = 4,

    [Display(Name = "Énergie")]
    Energy = 5,

    [Display(Name = "Intérieur")]
    HomeAffairs = 6,

    [Display(Name = "Travaux publics")]
    PublicWorks = 7,

    [Display(Name = "Environnement")]
    Environment = 8,

    [Display(Name = "Fonction publique")]
    CivilService = 9,

    [Display(Name = "Sciences")]
    Science = 10,

    [Display(Name = "Coopération au développement")]
    DevelopmentCooperation = 11,

    [Display(Name = "Sécurité sociale")]
    SocialSecurity = 12,

    [Display(Name = "Économie")]
    Economy = 13,

    [Display(Name = "Intégration sociale")]
    SocialInclusion = 14,

    [Display(Name = "Finances")]
    Finances = 15,

    [Display(Name = "Affaires étrangères")]
    ForeignAffairs = 16,

    [Display(Name = "Justice")]
    Justice = 17,

    [Display(Name = "Emploi et économie sociale")]
    WorkAndSocialEconomy = 18,

    [Display(Name = "Marchés publics")]
    PublicTenders = 19,

    [Display(Name = "Défense")]
    Defence = 20,

    [Display(Name = "Enseignement et formation")]
    EducationAndTraining = 21,

    [Display(Name = "Mobilité")]
    Mobility = 22,

    [Display(Name = "Administration")]
    Administration = 23,

    [Display(Name = "Services de politique générale du gouvernement")]
    GeneralGovernmentPolicy = 24,

    [Display(Name = "Sports")]
    Sports = 25,

    [Display(Name = "Culture")]
    Culture = 26,

    [Display(Name = "Agriculture et pêche")]
    AgricultureAndFisheries = 27,

    [Display(Name = "Jeunesse")]
    Youth = 28,

    [Display(Name = "Affaires étrangères flamandes")]
    FlemishForeignAffairs = 29,

    [Display(Name = "Bien-être, santé publique et famille")]
    WelfarePublicHealthAndFamily = 30,

    [Display(Name = "Fiscalité")]
    Taxation = 31
}
#endregion
