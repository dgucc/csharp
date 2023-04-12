using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Csla.Core;
using Csla.Rules;
using Csla;
using DataAccess;
using log4net;
using Csla.Rules.CommonRules;

namespace CslaBlazorApp.Shared {
	[Serializable]
	public class Document : BusinessBase<Document> {
		private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public Document() {
			MarkAsChild();
		}

		#region	PropertyInfos
		//Id 
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
		[Required]
		public int Id {
			get => GetProperty(IdProperty);
			set => SetProperty(IdProperty, value);
		}

		// Filename
		public static readonly PropertyInfo<string> FilenameProperty = RegisterProperty<string>(nameof(Filename));
		public string Filename {
			get => GetProperty(FilenameProperty);
			set => SetProperty(FilenameProperty, value);
		}

		// MimeType
		public static readonly PropertyInfo<string> MimeTypeProperty = RegisterProperty<string>(nameof(MimeType));
		public string MimeType {
			get => GetProperty(MimeTypeProperty);
			set => SetProperty(MimeTypeProperty, value);
		}

		// Extension
		public static readonly PropertyInfo<string> ExtensionProperty = RegisterProperty<string>(nameof(Extension));
		public string Extension {
			get => GetProperty(ExtensionProperty);
			set => SetProperty(ExtensionProperty, value);
		}

		// CreatedOn
		public static readonly PropertyInfo<DateTime> CreatedOnProperty = RegisterProperty<DateTime>(nameof(CreatedOn));
		public DateTime CreatedOn {
			get => GetProperty(CreatedOnProperty);
			set => SetProperty(CreatedOnProperty, value);
		}

		// Description
		public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(nameof(Description));
		public string Description {
			get => GetProperty(DescriptionProperty);
			set => SetProperty(DescriptionProperty, value);
		}

		// UploadedBy
		public static readonly PropertyInfo<string> UploadedByProperty = RegisterProperty<string>(nameof(UploadedBy));
		public string UploadedBy {
			get => GetProperty(UploadedByProperty);
			set => SetProperty(UploadedByProperty, value);
		}

		// File
		public static readonly PropertyInfo<byte[]> FileProperty = RegisterProperty<byte[]>(nameof(File));
		public byte[] File {
			get => GetProperty(FileProperty);
			set => SetProperty(FileProperty, value);
		}

		// Thumbnail
		public static readonly PropertyInfo<byte[]> ThumbnailProperty = RegisterProperty<byte[]>(nameof(Thumbnail));
		public byte[] Thumbnail {
			get => GetProperty(ThumbnailProperty);
			set => SetProperty(ThumbnailProperty, value);
		}

		// DocumentType
		public static readonly PropertyInfo<EnumDocumentType> DocumentTypeProperty = RegisterProperty<EnumDocumentType>(nameof(DocumentType));
		public EnumDocumentType DocumentType {
			get => GetProperty(DocumentTypeProperty);
			set => SetProperty(DocumentTypeProperty, value);
		}

		// Language
		public static readonly PropertyInfo<EnumLanguageCode> LanguageProperty = RegisterProperty<EnumLanguageCode>(nameof(Language));
		public EnumLanguageCode Language {
			get => GetProperty(LanguageProperty);
			set => SetProperty(LanguageProperty, value);
		}

		// PublicationId
		public static readonly PropertyInfo<int> PublicationIdProperty = RegisterProperty<int>(nameof(PublicationId));
		[Required]
		public int PublicationId {
			get => GetProperty(PublicationIdProperty);
			set => SetProperty(PublicationIdProperty, value);
		}

		#endregion

		#region	Rules

		protected override void PropertyHasChanged(IPropertyInfo property) {
			base.PropertyHasChanged(property);

			BusinessRules.CheckObjectRules();
		}

		protected override void AddBusinessRules() {
			base.AddBusinessRules();

			var props = this.FieldManager.GetRegisteredProperties();
			// TODO: add business rules
			BusinessRules.AddRule(new Required(LanguageProperty) { MessageDelegate = () => "Document.Error.Language.Required" });
			BusinessRules.AddRule(new Required(DocumentTypeProperty) { MessageDelegate = () => "Document.Error.DocumentType.Required" });
			BusinessRules.AddRule(new Required(FileProperty) { MessageDelegate = () => "Document.Error.File.Required" });

		}

		public class ValidateDocument : Csla.Rules.ObjectRule {
			public ValidateDocument(IEnumerable<IPropertyInfo> fields) {
				AffectedProperties.AddRange(fields);
			}

			protected override void Execute(IRuleContext context) {				

				// TODO: Define Rules here...
				var mimeType = (string)ReadProperty(context.Target, MimeTypeProperty);
				var extension = (string)ReadProperty(context.Target, ExtensionProperty);

				if (!mimeType.ToLower().Equals("application/pdf") || !extension.ToLower().Equals("pdf")) { 
					context.AddErrorResult(MimeTypeProperty, "Document.Error.FileType.NotSupported");
				}

			}
		}

		#endregion

		#region	DataPortal

		[Create]
		private void Create() {
			Id = -1;
			_log.Info("[PORTAL] Document Create()");
			base.Child_Create();
		}

		[Fetch]
		private void Fetch(int publicationId, [Inject] DataAccess.IDocumentDal dal) {
			var data = dal.GetByPublication(publicationId);
			MarkAsChild();
			using (BypassPropertyChecks) {
				Csla.Data.DataMapper.Map(data, this); // source : data->DocumentDTO, target: this->Shared.Document				
			}

			_log.Info(string.Format("[PORTAL] Fetch(Document.publicationId:{0})\n", publicationId)); 
			BusinessRules.CheckRules();
		}

		[FetchChild]
		private void Fetch(DataAccess.DocumentDTO data) {
			Id = data.Id;
			Filename = data.Filename;
			MimeType = data.MimeType;
			Extension = data.Extension;
			CreatedOn = (DateTime)data.CreatedOn;
			Description = data.Description;
			UploadedBy = data.UploadedBy;
			File = data.File;
			Thumbnail = data.Thumbnail;
			DocumentType = (EnumDocumentType)data.DocumentType;
			Language = (EnumLanguageCode)data.Language;
			PublicationId = data.PublicationId;

			MarkAsChild();
			_log.Info("[PORTAL] FetchChild(Document)");
		}

		[Insert]
		private void Insert([Inject] DataAccess.IDocumentDal dal) {
			_log.Info("[PORTAL] Insert(Document)");
			using (BypassPropertyChecks) {
				var data = new DataAccess.DocumentDTO {
					Filename = Filename,
					MimeType = MimeType,
					Extension = Extension,
					CreatedOn = CreatedOn,
					Description = Description,
					UploadedBy = UploadedBy,
					File = File,
					Thumbnail = Thumbnail,
					DocumentType = (DataAccess.EnumDocumentType)DocumentType,
					Language = (DataAccess.EnumLanguageCode)Language,
					PublicationId = PublicationId
				};

				var result = dal.Insert(data);
				_log.Info(string.Format("Inserted new Document {0}\n", result.Id));
				Id = result.Id;
			}
		}

		[Update]
		private void Update([Inject] DataAccess.IDocumentDal dal) {
			_log.Info("[PORTAL] Update(Document)");
			using (BypassPropertyChecks) {
				var data = new DataAccess.DocumentDTO {
					Id = Id,
					Filename = Filename,
					MimeType = MimeType,
					Extension = Extension,
					CreatedOn = CreatedOn,
					Description = Description,
					UploadedBy = UploadedBy,
					File = File,
					Thumbnail = Thumbnail,
					DocumentType = (DataAccess.EnumDocumentType)DocumentType,
					Language = (DataAccess.EnumLanguageCode)Language,
					PublicationId = PublicationId
				};
				_log.Info(string.Format("Update Document {0}\n", Id));
				dal.Update(data);
			}
		}

		[DeleteSelf]
		private void DeleteSelf([Inject] DataAccess.IDocumentDal dal) {
			_log.Info("[PORTAL] DeleteSelf(Document)");
			Delete(ReadProperty(IdProperty), dal);
		}

		[Delete]
		private void Delete(int id, [Inject] DataAccess.IDocumentDal dal) {
			_log.Info(string.Format("[PORTAL] Delete(Document.id:{0})\n", id));
			dal.Delete(id);
		}

		#endregion
	}
}
