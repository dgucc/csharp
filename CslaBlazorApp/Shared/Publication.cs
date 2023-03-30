using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.Schema;
using Csla;
using Csla.Core;
using Csla.Rules;
using Csla.Rules.CommonRules;
using log4net;
using log4net.Config;

namespace CslaBlazorApp.Shared {

	[Serializable]
	public class Publication : BusinessBase<Publication>{
		const int LEN_MAX_TITLE = 255;
		const int LEN_MAX_EMAIL = 255;
		private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		#region PropertyInfos
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
		[Required]
		public int Id {
			get { return GetProperty(IdProperty); }
			set { SetProperty(IdProperty, value); }
		}

		// ApprovalDate
		public static readonly PropertyInfo<DateTime> ApprovalDateProperty = RegisterProperty<DateTime>(nameof(ApprovalDate));
		public DateTime ApprovalDate {
			get { return GetProperty(ApprovalDateProperty); }
			set { SetProperty(ApprovalDateProperty, value); }
		}

		// PublishDate
		public static readonly PropertyInfo<DateTime> PublishDateProperty = RegisterProperty<DateTime>(nameof(PublishDate));
		public DateTime PublishDate {
			get { return GetProperty(PublishDateProperty); }
			set { SetProperty(PublishDateProperty, value); }
		}

		// RequestorEmail
		public static readonly PropertyInfo<string> RequestorEmailProperty = RegisterProperty<string>(nameof(RequestorEmail));
		public string RequestorEmail {
			get { return GetProperty(RequestorEmailProperty); }
			set { SetProperty(RequestorEmailProperty, value); }
		}

		// TitleFr
		public static readonly PropertyInfo<string> TitleFrProperty = RegisterProperty<string>(nameof(TitleFr));
		public string TitleFr {
			get { return GetProperty(TitleFrProperty); }
			set { SetProperty(TitleFrProperty, value); }
		}

		// TitleNl
		public static readonly PropertyInfo<string> TitleNlProperty = RegisterProperty<string>(nameof(TitleNl));
		public string TitleNl {
			get { return GetProperty(TitleNlProperty); }
			set { SetProperty(TitleNlProperty, value); }
		}

		// TitleDe
		public static readonly PropertyInfo<string> TitleDeProperty = RegisterProperty<string>(nameof(TitleDe));
		public string TitleDe {
			get { return GetProperty(TitleDeProperty); }
			set { SetProperty(TitleDeProperty, value); }
		}

		// TitleEn
		public static readonly PropertyInfo<string> TitleEnProperty = RegisterProperty<string>(nameof(TitleEn));
		public string TitleEn {
			get { return GetProperty(TitleEnProperty); }
			set { SetProperty(TitleEnProperty, value); }
		}

		// Cover
		public static readonly PropertyInfo<byte[]> CoverProperty = RegisterProperty<byte[]>(nameof(Cover));
		public byte[] Cover {
			get { return GetProperty(CoverProperty); }
			set { SetProperty(CoverProperty, value); }
		}

		// Documents
		public static readonly PropertyInfo<DocumentList> DocumentsProperty = RegisterProperty<DocumentList>(nameof(Documents), RelationshipTypes.LazyLoad);
		public DocumentList Documents {
			get => LazyGetProperty(DocumentsProperty, () => ApplicationContext.GetRequiredService<IChildDataPortal<DocumentList>>().CreateChild());
			set => LoadProperty(DocumentsProperty, value);
		}


		#endregion

		#region Rules

		protected override void PropertyHasChanged(IPropertyInfo property) {
			base.PropertyHasChanged(property);
			//_log.Info("[SHARE] CheckObjectRules()");
			BusinessRules.CheckObjectRules();
		}

		
		protected override void AddBusinessRules() {
			base.AddBusinessRules();

			var props = this.FieldManager.GetRegisteredProperties();

			BusinessRules.AddRule(new ValidatePublication(props));
			BusinessRules.AddRule(new Required(ApprovalDateProperty) { MessageDelegate = () => "Publication.Error.The_date_of_approval_must_be_defined." });
			BusinessRules.AddRule(new MaxLength(TitleFrProperty, 255, delegate { return string.Format("Publication.Error.Title_{0}_can_not_exceed_{1}_characters.", "FR", LEN_MAX_TITLE); }));
			BusinessRules.AddRule(new MaxLength(TitleNlProperty, 255, delegate { return string.Format("Publication.Error.Title_{0}_can_not_exceed_{1}_characters.", "NL", LEN_MAX_TITLE); }));
			BusinessRules.AddRule(new MaxLength(TitleDeProperty, 255, delegate { return string.Format("Publication.Error.Title_{0}_can_not_exceed_{1}_characters.", "DE", LEN_MAX_TITLE); }));
			BusinessRules.AddRule(new MaxLength(TitleEnProperty, 255, delegate { return string.Format("Publication.Error.Title_{0}_can_not_exceed_{1}_characters.", "EN", LEN_MAX_TITLE); }));
			BusinessRules.AddRule(new MaxLength(RequestorEmailProperty, 255, delegate { return string.Format("Publication.Error.Email_address_can_not_exceed_{0}_characters.", LEN_MAX_EMAIL); }));
		}

		public class ValidatePublication : Csla.Rules.ObjectRule {
			public ValidatePublication(IEnumerable<IPropertyInfo> fields) {
				AffectedProperties.AddRange(fields);
			}

			protected override void Execute(IRuleContext context) {

				// Titles Rules
				var titleFr = (string)ReadProperty(context.Target, TitleFrProperty);
				var titleNl = (string)ReadProperty(context.Target, TitleNlProperty);
				var titleDe = (string)ReadProperty(context.Target, TitleDeProperty);
				var titleEn = (string)ReadProperty(context.Target, TitleEnProperty);

				if (string.IsNullOrEmpty(titleFr) &&
					string.IsNullOrEmpty(titleNl) &&
					string.IsNullOrEmpty(titleDe) &&
					string.IsNullOrEmpty(titleEn)) {
						context.AddErrorResult(TitleFrProperty, "Publication.Error.At_least_one_title_(FR,NL,DE,EN)_must_be_defined.");
						context.AddErrorResult(TitleNlProperty, "Publication.Error.At_least_one_title_(FR,NL,DE,EN)_must_be_defined.");
						context.AddErrorResult(TitleDeProperty, "Publication.Error.At_least_one_title_(FR,NL,DE,EN)_must_be_defined.");
						context.AddErrorResult(TitleEnProperty, "Publication.Error.At_least_one_title_(FR,NL,DE,EN)_must_be_defined.");
				}

				// Dates Rules
				var approvalDate = (DateTime)ReadProperty(context.Target, ApprovalDateProperty);
				var publishDate = (DateTime)ReadProperty(context.Target, PublishDateProperty);

				// PublishDate > ApprovalDate
				if (approvalDate != null && publishDate != null) {
					var d1 = new DateTime(approvalDate.Year, approvalDate.Month, approvalDate.Day, approvalDate.Hour, approvalDate.Minute, approvalDate.Second);
					var d2 = new DateTime(publishDate.Year, publishDate.Month, publishDate.Day, publishDate.Hour, publishDate.Minute, publishDate.Second);
					if (DateTime.Compare(d1, d2)>=0) {
						context.AddErrorResult(PublishDateProperty, "Publication.Error.Publication_must_occur_after_an_approval.");
					}
				}

				// Email RegEx Rules
				var email = (string)ReadProperty(context.Target, RequestorEmailProperty);
				if(!string.IsNullOrEmpty(email)) {
					Regex regex = new Regex(@"^([\w\.\-_]+)@([\w\-_]+)((\.(\w){2,})+)$");
					Match match = regex.Match(email);
					if (!match.Success) {
						context.AddWarningResult(RequestorEmailProperty, "Publication.Warning.Example : name@example.com");
						context.AddErrorResult(RequestorEmailProperty, "Publication.Error.The_email_address_is_not_valid.");
					}
				}
			}
		}
		#endregion

		#region DataPortal
		[Create]
		private void Create() {
			Id = -1;
			ApprovalDate = DateTime.Today;
			PublishDate = DateTime.Now;
			_log.Info("[PORTAL] Create(Publication)");
			base.Child_Create();
		}

		[Fetch]
		private void Fetch(int id, [Inject] DataAccess.IPublicationDal dal) {
			var data = dal.Get(id);
			using (BypassPropertyChecks)

			Csla.Data.DataMapper.Map(data, this);
			_log.Info(string.Format("[PORTAL] Fetch(Publication id:{0})",id));
			BusinessRules.CheckRules();
		}

		[FetchChild]
		private void Fetch(DataAccess.PublicationDTO data) {
			Id = data.Id;
			ApprovalDate = (DateTime)data.ApprovalDate;
			PublishDate = (DateTime)data.PublishDate;
			RequestorEmail = data.RequestorEmail;
			TitleFr = data.TitleFr;
			TitleNl = data.TitleNl;
			TitleDe = data.TitleDe;
			TitleEn = data.TitleEn;
			Cover = data.Cover;
			_log.Info("[PORTAL] FetchChild(Publication)");
		}

		[Insert]
		private void Insert([Inject] DataAccess.IPublicationDal dal) {
			_log.Info("[PORTAL] Insert(Publication)");
			using (BypassPropertyChecks) {
				var data = new DataAccess.PublicationDTO {
					ApprovalDate= ApprovalDate,
					PublishDate= PublishDate,
					RequestorEmail= RequestorEmail,
					TitleFr = TitleFr,
					TitleNl = TitleNl,
					TitleDe = TitleDe,
					TitleEn = TitleEn,
					Cover = Cover
				};
				var result = dal.Insert(data);
				_log.Info(string.Format("Inserted new Publication {0}", result.Id));
				Id = result.Id;
			}
		}

		[Update]
		private void Update([Inject] DataAccess.IPublicationDal dal) {
			_log.Info("[PORTAL] Update()");
			using (BypassPropertyChecks) {
				var data = new DataAccess.PublicationDTO {
					Id = Id,
					ApprovalDate = ApprovalDate,
					PublishDate = PublishDate,
					RequestorEmail = RequestorEmail,
					TitleFr = TitleFr,
					TitleNl = TitleNl,
					TitleDe = TitleDe,
					TitleEn = TitleEn,
					Cover = Cover
				};
				_log.Info(string.Format("Update Publication {0}", Id));
				dal.Update(data);
			}
		}

		[DeleteSelf]
		private void DeleteSelf([Inject] DataAccess.IPublicationDal dal) {
			_log.Info("[PORTAL] DeleteSelf(Publication)");
			Delete(ReadProperty(IdProperty), dal);
		}

		[Delete]
		private void Delete(int id, [Inject] DataAccess.IPublicationDal dal) {
			_log.Info(string.Format("[PORTAL] Delete(Publication id:{0})", id));
			dal.Delete(id);
		}
		#endregion
	}

}

