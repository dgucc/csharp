using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess {
    public class Topic {
        //public int Id { get; set; }
        public EnumTopicType TopicType { get; set; }
    }

    public enum EnumTopicType {

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
}
