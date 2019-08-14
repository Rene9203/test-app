using System.ComponentModel.DataAnnotations;

namespace TestApp.Core.SharedKernel
{
    public abstract class Entity<TId>
    {
        [Key]
        public TId Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var objB = obj as Entity<TId>;

            if (objB == null)
                return false;

            if (ReferenceEquals(this, objB))
                return true;

            if (GetType() != objB.GetType())
                return false;

            return (dynamic)Id == (dynamic)objB.Id;
        }

        public static bool operator ==(Entity<TId> objA, Entity<TId> objB)
        {
            if (objA is null && objB is null)
                return true;

            if (objA is null || objB is null)
                return false;

            return objA.Equals(objB);
        }

        public static bool operator !=(Entity<TId> objA, Entity<TId> objB)
        {
            return !(objA == objB);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id.ToString()).GetHashCode();
        }
    }
}
