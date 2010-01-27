using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress.Quickstart.Domain.Entities
{
    [Serializable]
    public abstract class EntityBase
    {

        public virtual int Id
        {
            get;
            set;
        }

        #region Equality Tests

        public virtual bool Equals(EntityBase obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return obj.Id == Id && (!(obj.Id == 0 || Id == 0));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((EntityBase)obj);
        }
        
        public override int GetHashCode()
        {
            return unchecked((Id.GetHashCode() * 397) ^ GetType().GetHashCode());
        }

        public static bool operator ==(EntityBase left, EntityBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EntityBase left, EntityBase right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
