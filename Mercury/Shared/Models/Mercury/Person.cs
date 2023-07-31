using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Mercury.Shared.Models.AspNetUser;
namespace Mercury.Shared.Models.Mercury
{
    /// <summary>
    /// The Person model class.
    /// </summary>
    [Table("Person", Schema = "Mercury")]
    [Serializable]
    public partial class Person 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the person ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int PersonID
        {
            get; set;
        }
        int _personID;

        /// <summary>
        /// Gets or sets the AppUser ID.
        /// </summary>
        [DataMember]
        public int? AppUserID
        {
            get; set;
        }
        int? _appUserID;

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        [Required]
        [StringLength(20)]
        [DataMember]
        public string Culture
        {
            get; set;
        }
        string _culture;


        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [StringLength(80)]
        [DataMember]
        public string Title
        {
            get; set;
        }
        string _title;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [StringLength(80)]
        [DataMember]
        public string LastName
        {
            get; set;
        }
        string _lastName;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [StringLength(80)]
        [DataMember]
        public string FirstName
        {
            get; set;
        }
        string _firstName;

        /// <summary>
        /// Gets or sets the vat number.
        /// </summary>
        [StringLength(20)]
        public string VatNumber
        {
            get; set;
        }
        string _vatNumber;

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(200)]
        [DataMember]
        public string Remarks
        {
            get; set;
        }
        string _remarks;

        /// <summary>
        /// Gets or sets the annual revenue.
        /// </summary>
        [DataMember]
        public decimal? AnnualRevenue
        {
            get; set;
        }
        decimal? _annualRevenue;

        /// <summary>
        /// Gets the full name, 
        /// i.e, the concatenation of first name and last name.
        /// </summary>
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets the inverted full name,
        /// i.e, the concatenation of last name and first name.
        /// </summary>
        [NotMapped]
        public string FullNameInv => $"{LastName} {FirstName}";

        /// <summary>
        /// The AppUser associated with this Person.
        /// </summary>
        [DataMember]
        public virtual AspNetUser.AspNetUser AppUser
        { get; set; }

        /// <summary>
        /// The collection of Addresses associated with this Person.
        /// </summary>
        [ForeignKey("PersonID")]
        [DataMember]
        public virtual ICollection<Address> Addresses { get; set; }

        /// <summary>
        /// The collection of Emails associated with this Person.
        /// </summary>
        [ForeignKey("PersonID")]
        [DataMember]
        public virtual ICollection<Email> Emails { get; set; }

        /// <summary>
        /// The collection of PersonProfiles associated with this Person.
        /// </summary>
        [ForeignKey("PersonID")]
        [DataMember]
        public virtual ICollection<PersonProfile> PersonProfiles { get; set; }

        /// <summary>
        /// The collection of Phones associated with this Person.
        /// </summary>
        [ForeignKey("PersonID")]
        [DataMember]
        public virtual ICollection<Phone> Phones { get; set; }

        #endregion
    }
}