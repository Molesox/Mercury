using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Mercury.Shared.Models.Mercury
{
    /// <summary>
    /// The PersonProfile model class.
    /// </summary>
    [Table("PersonProfile", Schema = "Mercury")]
    [Serializable]
    public partial class PersonProfile
    {
        #region Properties

        /// <summary>
        /// Gets or sets the PersonProfile ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int PersonProfileID
        {
            get; set;
        }
        int _personProfileID;

        /// <summary>
        /// Gets or sets the person ID.
        /// </summary>
        [DataMember]
        public int PersonID
        {
            get; set;
        }
        int _personID;

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        [DataMember]
        public byte[] Photo
        {
            get; set;
        }
        byte[] _photo;

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        [Column(TypeName = "date")]
        [DataMember]
        public DateTime? BirthDate
        {
            get; set;
        }
        DateTime? _birthDate;

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [DataMember]
        public int Gender
        {
            get; set;
        }
        int _gender;

        /// <summary>
        /// The Person associated with this PersonProfile.
        /// </summary>
        [DataMember]
        public virtual Person Person
        { get; set; }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        [DataMember]
        public virtual string Theme { get; set; }

        #endregion
    }
}