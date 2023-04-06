using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess.Mock {
    public class PublicationDal : IPublicationDal {
        #region Test Data
        private static readonly List<PublicationDTO> _publicationTable = new List<PublicationDTO> {
            new PublicationDTO {
                Id=1,
                //LegislativeLevel=Enum.GetName(EnumLegislativeLevel.FederalState),
                //PublicationType=Enum.GetName(EnumPublicationType.ParticularReport),
                //ApprovedBy=Enum.GetName(EnumApprovedBy.GeneralAssembly),
				ApprovalDate= DateTime.ParseExact("11/05/2022","dd/MM/yyyy",  new CultureInfo("fr-BE")),
                PublishDate=DateTime.ParseExact("16/06/2022 09:00","dd/MM/yyyy HH:mm",  new CultureInfo("fr-BE")),
                RequestorEmail ="VanPouckeC@ccrek.be ",
                TitleFr="Formation de base des inspecteurs de police",
                TitleNl="Basisopleiding voor politie-inspecteurs",
				TitleDe="Die Grundausbildung der Polizeiinspektoren",
				TitleEn="Basic training of police inspectors",
				//HeaderFr = "La Cour des comptes a examiné la formation de base des inspecteurs de police qui est essentielle pour le bon fonctionnement de la police intégrée. La Cour des comptes estime que la police fédérale n’est en mesure de garantir ni une formation homogène ni une validation équivalente des acquis dans toutes les écoles. Par ailleurs, aucun dispositif centralisé ne permet le suivi des normes de qualité réglementaires. La Cour des comptes constate également une grande disparité des moyens entre écoles. En outre, la police fédérale n’a pas d’information sur le coût total de la formation par aspirant.",
				//HeaderNl = "Het Rekenhof heeft een audit uitgevoerd over de basisopleiding voor politie-inspecteurs, die essentieel is voor de goede werking van de geïntegreerde politie. Het is van oordeel dat de Federale Politie geen homogene opleiding noch een gelijkwaardige validering van de verworven kennis in alle scholen kan waarborgen. Voorts kunnen de reglementaire kwaliteitsnormen niet worden opgevolgd bij gebrek aan een gecentraliseerd kader. Het Rekenhof stelt ook vast dat de middelen sterk verschillen van school tot school. De Federale Politie heeft bovendien geen zicht op de totale kostprijs van de opleiding per aspirant.",
				//HeaderDe = "Der Rechnungshof untersuchte die Grundausbildung der Polizeiinspektoren, die für das reibungslose Funktionieren der integrierten Polizei von wesentlicher Bedeutung ist. Er ist der Ansicht, dass die Föderalpolizei weder eine einheitliche Ausbildung noch eine gleichwertige Validierung der erworbenen Kenntnisse auf Ebene aller Schulen gewährleisten kann. Darüber hinaus gibt es kein zentralisiertes System zur Überwachung der vorgeschriebenen Qualitätsstandards. Der Rechnungshof stellte auch fest, dass die Mittel, die den einzelnen Schulen bereitgestellt werden, sehr unterschiedlich sind. Darüber hinaus hat die Föderalpolizei keine Informationen über die Gesamtkosten der Ausbildung pro Anwärter.",
				//HeaderEn = "The Belgian Court of Audit examined the basic training of police inspectors that is essential to the proper functioning of the integrated police services. The Court considers that the federal police can guarantee neither training homogeneity nor the equivalent validation of the knowledge acquired in every school. Furthermore, there is no centralised system to ensure the monitoring of the regulatory quality standards. The Court also notes a significant disparity of resources available for schools. Besides, the federal police have no information on the total training cost per candidate.",
				//Topics= new List<EnumTopicType>{ EnumTopicType.CivilService,EnumTopicType.HomeAffairs},
                Cover = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"../TEMP/cover/Publication_01_FR_cover.jpg"))
			},
            new PublicationDTO {
                Id=2,
                //LegislativeLevel=Enum.GetName(EnumLegislativeLevel.BrusselsCapitalRegion),
                //PublicationType=Enum.GetName(EnumPublicationType.ParticularReportToBrusselsCapitalParliament),
                //ApprovedBy=Enum.GetName(EnumApprovedBy.FrenchChamber),
                ApprovalDate= DateTime.ParseExact("08/06/2022","dd/MM/yyyy",  new CultureInfo("fr-BE")),
                PublishDate=DateTime.ParseExact("27/06/2022 09:00","dd/MM/yyyy HH:mm",  new CultureInfo("fr-BE")),
                //ApprovalDate=DateTime.Parse("2022-06-08"),
                //PublishDate=DateTime.Parse("2022-06-27 09:00"),
                RequestorEmail ="HendrickxK@ccrek.be",
				TitleFr="Paiements vers des paradis fiscaux",
                TitleNl="Betalingen aan belastingparadijzen",
				TitleDe="Zahlungen nach Steueroasen",
				TitleEn="Payments to Tax Havens",
				//HeaderFr = "Dans son rapport adressé au Parlement fédéral, la Cour des comptes examine la manière dont l’administration fiscale contrôle l'obligation de déclaration des paiements effectués vers des paradis fiscaux. Elle constate que la réglementation manque de clarté : il existe trois listes officielles de paradis fiscaux et la liste belge n'est plus conforme à la réglementation belge. En outre, la réglementation est rendue difficilement applicable notamment par une disposition de l'exposé des motifs de la loi-programme, l'influence de la libre circulation des capitaux et les conventions préventives de la double imposition. Enfin, les contrôles sont peu productifs et l'obligation de déclaration peut facilement être contournée. La Cour recommande dès lors à l’administration fiscale d'adapter sa stratégie de contrôle et de se concentrer davantage sur la détection de paiements non déclarés.",
				//HeaderNl = "In zijn verslag aan het federale parlement onderzoekt het Rekenhof hoe de fiscus de verplichte aangifte van betalingen aan belastingparadijzen controleert. Het stelt vast dat de regelgeving niet duidelijk is: er zijn drie officiële lijsten met belastingparadijzen en de Belgische lijst is niet meer in overeenstemming met de Belgische regelgeving. Bovendien is de regelgeving moeilijk afdwingbaar door onder meer een bepaling in de memorie van toelichting van de programmawet, door de invloed van het vrij verkeer van kapitaal en door dubbelbelastingverdragen. De controles leveren tot slot weinig op en de aangifteplicht kan gemakkelijk omzeild worden. Het Rekenhof beveelt de fiscus dan ook aan zijn controlestrategie aan te passen en meer in te zetten op het detecteren van niet-aangegeven betalingen.",
				//HeaderDe = "In seinem Bericht an das Föderalparlament untersuchte der Rechnungshof, wie die Steuerverwaltung die Einhaltung der Meldepflicht für Zahlungen nach Steueroasen kontrolliert. Er stellte fest, dass die Rechtsvorschriften unklar sind: es gibt drei offizielle Listen von Steueroasen und die belgische Liste ist nicht mehr in Einklang mit den belgischen Vorschriften. Darüber hinaus wird die Anwendung der Rechtsvorschriften insbesondere durch eine Bestimmung in der Begründung des Programmgesetzes, den Einfluss des freien Kapitalverkehrs und die Doppelbesteuerungsabkommen erschwert. Schließlich sind die Kontrollen wenig erfolgreich, und die Meldepflicht kann leicht umgangen werden. Der Rechnungshof empfiehlt der Steuerverwaltung daher, ihre Kontrollstrategie anzupassen und sich verstärkt auf die Aufdeckung nicht angemeldeter Zahlungen zu konzentrieren.",
				//HeaderEn = "The Belgian Court of Audit examined whether the tax administration is adequately organised and has the necessary tools to analyse the reported payments to tax havens and use these in its control approach. To that end, the Court examined both the selection process of the cases to be controlled and the control procedure per se for the payments made to tax havens. ",
				//Topics= new List<EnumTopicType>{ EnumTopicType.Taxation,EnumTopicType.Finances}
                Cover = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"../TEMP/cover/Publication_02_FR_cover.jpg"))
            },
            new PublicationDTO { 
                Id=3,
                //LegislativeLevel = Enum.GetName(EnumLegislativeLevel.FlemishCommunity),
                //PublicationType = Enum.GetName(EnumPublicationType.ParticularReport),
                //ApprovedBy = Enum.GetName(EnumApprovedBy.DutchChamber),
                ApprovalDate = DateTime.Parse("2022-06-07"),
                PublishDate = DateTime.Parse("2022-06-22 09:00"),
				RequestorEmail ="WeytensT@ccrek.be",
				TitleFr="Approche en matière de points dangereux pour une meilleure sécurité routière",
				TitleNl="Aanpak van gevaarlijke punten voor een betere verkeersveiligheid",
				TitleDe="Umbau von Gefahrenstellen für mehr Verkehrssicherheit",
				TitleEn="Tackling dangerous points for better road safety",
				//HeaderFr = "L’approche en matière de points dangereux doit permettre au gouvernement flamand de viser un trafic routier exempt de victimes d’ici 2050. Le programme historique, selon lequel 800 points dangereux devraient être traités en cinq ans, n’aura pleinement été mis en œuvre que 22 ans après son lancement, pour un coût d'environ un milliard d'euros. Depuis 2018, le gouvernement flamand a adopté une nouvelle approche : chaque année, il publie une liste dynamique des points dangereux détectés au cours de l’année en question. La liste dynamique de 2021 contenait 313 points dangereux : 101 lieux étaient nouveaux et 212 figuraient déjà sur des listes antérieures. Au cours des quatre années de fonctionnement dynamique, 580 points dangereux uniques ont été détectés ; pour 232 d’entre eux, des travaux étaient en cours de réalisation ou avaient été réalisés fin 2021. Le coût du fonctionnement dynamique n'est pas clair. Le gouvernement a assuré un suivi et une évaluation limités pendant toute la période de mise en œuvre ; il n'y a donc aucune certitude quant à l’utilisation optimale des moyens.",
				//HeaderNl = "Met de aanpak van gevaarlijke punten streeft de Vlaamse overheid naar een slachtoffervrij verkeer tegen 2050. Het historisch programma, waarbij 800 gevaarlijke punten in 5 jaar tijd zouden worden aangepakt, zal pas 22 jaar na de start volledig zijn uitgevoerd tegen een kostprijs van ongeveer één miljard euro. Sinds 2018 hanteert de Vlaamse overheid een nieuwe aanpak: jaarlijks publiceert ze een dynamische lijst met de gevaarlijke punten die dat jaar werden gedetecteerd. De dynamische lijst van 2021 bevatte 313 gevaarlijke punten: 101 locaties waren nieuw en 212 locaties stonden al op eerdere lijsten. In de 4 jaar dynamische werking zijn er 580 unieke gevaarlijke punten gedetecteerd, waarvan er eind 2021 232 punten in uitvoering of uitgevoerd waren. De kostprijs van de dynamische werking is onduidelijk. Tijdens de volledige uitvoeringsperiode heeft de overheid weinig gemonitord en geëvalueerd, zodat er geen zekerheid bestaat over de value for money.",
				//HeaderDe = "Mit dem Umbau von Gefahrenstellen strebt die flämische Regierung einen opferfreien Verkehr bis 2050 an. Das historische Programm, in dem 800 Gefahrenstellen in 5 Jahren umgebaut werden sollten, wird erst 22 Jahre nach dem Start umgesetzt, mit Kosten von etwa einer Milliarde Euro. Seit 2018 verfolgt die flämische Regierung einen neuen Ansatz: Sie veröffentlicht jährlich eine dynamische Liste der Gefahrenstellen, die in diesem Jahr entdeckt wurden. Die dynamische Liste 2021 enthielt 313 Gefahrenstellen: 101 Stellen waren neu und 212 Stellen waren bereits auf früheren Listen. In den 4 Jahren des dynamischen Ansatzes wurden 580 einzigartige Gefahrenstellen entdeckt, von denen Ende 2021 232 Stellen sich in der Implementierung befinden oder implementiert waren. Die Kosten des dynamischen Ansatzes sind unklar. Während der gesamten Implementierung hat die Regierung nur wenig Monitoring und Evaluierung durchgeführt, sodass keine Gewissheit über das Preis-Leistungs-Verhältnis besteht.",
				//HeaderEn = "With its approach to dangerous points, Flemish government is aiming for victim-free traffic by 2050. The historic programme, in which 800 dangerous points would be tackled in 5 years, will only be fully implemented 22 years after the start at a cost of approximately one billion euros. Since 2018, Flemish government has adopted a new approach: it annually publishes a dynamic list of dangerous points detected that year. The 2021 dynamic list contained 313 dangerous points: 101 new locations and 212 locations that were already on previous lists. In the 4 years of dynamic operation, 580 unique dangerous points were detected, of which 232 points were under construction or implemented by the end of 2021. The dynamic operation cost is unclear. During the entire implementation period, government did little monitoring or evaluation, so that there is no certainty about the value for money.",
				//Topics= new List<EnumTopicType>{ EnumTopicType.Mobility, EnumTopicType.PublicWorks }
                Cover = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"../TEMP/cover/Publication_03_NL_cover.jpg"))
			},
            new PublicationDTO { 
                Id=4,
                //LegislativeLevel = Enum.GetName(EnumLegislativeLevel.FlemishCommunity),
                //PublicationType = Enum.GetName(EnumPublicationType.AnnualBooks),
                //ApprovedBy = Enum.GetName(EnumApprovedBy.DutchChamber),
                ApprovalDate = DateTime.Parse("2022-06-28"),
                PublishDate = DateTime.Parse("2022-06-29 09:00"),
				RequestorEmail ="GalleM@ccrek.be",
				TitleFr="Rapport des comptes 2021",
				TitleNl="Rekeningenrapport over 2021",
				TitleDe="Rechnungsbericht über 2021",
				TitleEn="Report on the Public Accounts 2021",
				//HeaderFr = "La Cour des comptes a présenté les résultats de son audit des comptes du gouvernement flamand pour 2021 dans son rapport comptable au Parlement flamand. L'exercice 2021 a encore été en partie marqué par les conséquences financières de la pandémie de corona, que la Cour des comptes estime à 1,8 milliard d'euros. Cependant, un certain nombre de chiffres clés se sont fortement améliorés par rapport à l'année précédente. Le gouvernement flamand a également pris à cœur un certain nombre de recommandations de la Cour des comptes, telles que la valorisation des filiales d’entreprises ou la création de diverses provisions, afin que la comptabilité flamande soit désormais plus conforme à la réalité. La Cour des comptes a ainsi pu approuver tous les comptes, à l'exception des comptes économiques, sur lesquels elle ne peut se prononcer pour l'instant en raison de trop nombreuses incertitudes. Par exemple, de nombreux terrains et bâtiments et ouvrages de génie civil sont absents du bilan. Les obligations hors bilan, par exemple pour le projet Oosterweel, n'ont pas encore été suffisamment expliquées. Enfin, la Cour des comptes a traité un certain nombre de thèmes politiques pertinents sur le plan social, tels que l'élaboration du plan de relance flamand, les listes d'attente dans les garderies et les soins aux personnes handicapées, et la mise en œuvre des intentions politiques concernant les panneaux solaires et les groupes cibles. Le message est généralement que le gouvernement flamand progresse, bien qu'insuffisamment pour réaliser toutes les ambitions législatives.",
                //HeaderNl = "Het Rekenhof heeft de resultaten van zijn controle op de rekeningen over 2021 van de Vlaamse overheid in zijn rekeningenrapport aan het Vlaams Parlement voorgelegd. Het boekjaar 2021 werd nog altijd mede getekend door de financiële gevolgen van de coronapandemie, die het Rekenhof raamt op 1,8 miljard euro. Een aantal kerncijfers verbeterden echter sterk tegenover het vorige jaar. De Vlaamse overheid nam ook een aantal aanbevelingen van het Rekenhof ter harte, zoals de waardering van dochterondernemingen of de aanleg van diverse voorzieningen, waardoor de Vlaamse boekhouding nu meer met de werkelijkheid overeenstemt. Het Rekenhof kon dan ook alle rekeningen goedkeuren, op de economische rekeningen na, waarover het voorlopig geen oordeel kan vellen door nog te veel onzekerheden. Zo ontbreken in de balans veel percelen en gebouwen en werken van burgerlijke bouwkunde. Ook zijn de buitenbalansverplichtingen, bijvoorbeeld voor het Oosterweelproject, nog onvoldoende toegelicht. Ten slotte ging het Rekenhof in op enkele maatschappelijk relevante beleidsthema’s, zoals de ontplooiing van het Vlaams relanceplan, de wachtlijsten in de kinderopvang en in de zorg voor personen met een functiebeperking, en de uitvoering van de beleidsintenties inzake zonnepanelen en doelgroepen. Doorgaans is de boodschap dat de Vlaamse overheid vooruitgang boekt, zij het onvoldoende om alle legislatuurambities te vervullen.",
                //HeaderDe = "Der Rechnungshof hat die Ergebnisse seiner Prüfung des Jahresabschlusses der flämischen Regierung für 2021 in seinem Rechnungsabschlussbericht an das flämische Parlament vorgelegt. Das Geschäftsjahr 2021 war noch teilweise von den finanziellen Folgen der Corona-Pandemie geprägt, die der Rechnungshof auf 1,8 Milliarden Euro beziffert. Allerdings haben sich einige Kennzahlen im Vergleich zum Vorjahr stark verbessert. Die flämische Regierung hat sich auch eine Reihe von Empfehlungen des Rechnungshofs zu Herzen genommen, wie zum Beispiel die Bewertung von Tochtergesellschaften oder den Aufbau verschiedener Provisionen, sodass die flämische öffentliche Rechnungsführung jetzt mehr der Realität entspricht. Der Rechnungshof konnte daher alle Rechnungen genehmigen, mit Ausnahme der volkswirtschaftlichen Gesamtrechnungen, die er aufgrund zu vieler Unsicherheiten vorerst nicht beurteilen kann. So fehlen beispielsweise viele Grundstücke und Gebäude sowie Tiefbauarbeiten in der Bilanz. Die außerbilanziellen Verpflichtungen, beispielsweise für das Oosterweel-Projekt, wurden noch nicht ausreichend erläutert. Schließlich besprach der Rechnungshof eine Reihe gesellschaftlich relevanter politischer Themen, wie die Entwicklung des flämischen Sanierungsplans, die Wartelisten bei der Kinderbetreuung und der Betreuung von Menschen mit Behinderungen sowie die Umsetzung politischer Absichten in Bezug auf Solarmodule und Zielgruppen. Die Botschaft lautet in der Regel, dass die flämische Regierung Fortschritte macht, wenn auch nicht ausreichend, um alle gesetzgeberischen Ambitionen zu erfüllen.",
                //HeaderEn = "The Court of Audit has presented the results of its audit of the Flemish government's accounts for 2021 in its accounts report to the Flemish Parliament. The 2021 financial year was still partly marked by the financial consequences of the corona pandemic, which the Court of Audit estimates at 1.8 billion euros. However, a number of key figures improved strongly compared to the previous year. Flemish government also took a number of recommendations from the Court of Audit to heart, such as the valuation of subsidiary companies or the creation of various provisions, so that Flemish public accounting is now more in line with reality. The Court of Audit was therefore able to approve all accounts, except for the economic accounts, on which it cannot make a judgment for the time being due to too many uncertainties. For example, many plots of land and buildings as well as civil engineering works are missing from the balance sheet. The off-balance sheet obligations, for example for the Oosterweel project, have not yet been sufficiently explained. Finally, the Court of Audit discussed a number of socially relevant policy themes, such as the development of the Flemish recovery plan, the waiting lists in childcare and care for people with disabilities, and the implementation of policy intentions regarding solar panels and target groups. The message is usually that the Flemish government is making progress, albeit insufficiently to fulfill all legislative ambitions.",
                //Topics = new List<EnumTopicType>{ EnumTopicType.Finances, EnumTopicType.Budget}
                Cover = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"../TEMP/cover/Publication_04_NL_cover.jpg"))
            },
			new PublicationDTO {
				Id=5,
				//LegislativeLevel = Enum.GetName(EnumLegislativeLevel.FrenchCommunity),
				//PublicationType = Enum.GetName(EnumPublicationType.IndividualReport),
				//ApprovedBy = Enum.GetName(EnumApprovedBy.FrenchChamber),
				ApprovalDate = DateTime.Parse("2023-02-12"),
                PublishDate = DateTime.Parse("2023-01-31 09:00"),
				RequestorEmail ="Vanducci@ccrek",
				TitleFr="Lorem ipsum dolor sit amet [FR]",
				TitleNl= new string('X', 52),
				//TitleDe="Lorem ipsum dolor sit amet [DE]",
				//TitleEn="Lorem ipsum dolor sit amet [EN]",
				//HeaderFr = "Header [FR]",
                //HeaderNl = "Header [NL]",
                //HeaderDe = "Header [DE]",
                //HeaderEn = "Header [EN]"
                //Topics = new List<EnumTopicType>{ EnumTopicType.Finances, EnumTopicType.Budget}
                Cover = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"../TEMP/cover.jpg"))
            }
		};
		#endregion

		public bool Delete(int id) {
			Console.WriteLine("[DAL] Delete(id:{0})", id);
			var publication = _publicationTable.Where(p => p.Id == id).FirstOrDefault();
            if (publication != null) {
                lock (_publicationTable) {
                    _publicationTable.Remove(publication);
                    return true;
                }
            } else {
                return false;
            }            
        }

		//public object Delete(Func<object, int> value) {
		//	throw new NotImplementedException();
		//}

		public bool Exists(int id) {
            var publication = _publicationTable.Where(p => p.Id == id).FirstOrDefault();
            return !(publication == null);
        }

        public PublicationDTO Get(int id) {
			Console.WriteLine("[DAL] Get(id:{0})", id);
			var publication = _publicationTable.Where(p => p.Id == id).FirstOrDefault();
            if (publication != null) {
                return publication;
            } else {
                throw new KeyNotFoundException($"Id {id}");
            }

        }

        public List<PublicationDTO> Get() {
            return _publicationTable.Where(r => true).ToList();
        }

        public PublicationDTO Insert(PublicationDTO publication) {
			Console.WriteLine("[DAL] Insert()");
			if (Exists(publication.Id))
                throw new InvalidOperationException($"Key exists {publication.Id}");
            lock (_publicationTable) {
                int lastId = _publicationTable.Max(m => m.Id);
                publication.Id = ++lastId;
                _publicationTable.Add(publication);
            }
            return publication;
        }

        public PublicationDTO Update(PublicationDTO publication) {
            Console.WriteLine("[DAL] Update()");
            lock (_publicationTable) {
                var old = Get(publication.Id);
                //old.LegislativeLevel= publication.LegislativeLevel;
                //old.PublicationType= publication.PublicationType;
                //old.ApprovedBy= publication.ApprovedBy;
                old.ApprovalDate= publication.ApprovalDate;
                old.PublishDate= publication.PublishDate;
                old.RequestorEmail = publication.RequestorEmail;
                old.TitleFr = publication.TitleFr;
                old.TitleNl = publication.TitleNl;
                old.TitleDe = publication.TitleDe;
                old.TitleEn = publication.TitleEn;
                old.Cover = publication.Cover;
                return old;
            }
        }
    }
}
