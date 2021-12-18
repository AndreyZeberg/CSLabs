using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    [Serializable]
    class Edition : IComparable, IComparer<Edition>
    {
        protected string editionName;
        protected DateTime release;
        protected int copies;

        public Edition(string editionName, DateTime release, int copies)
        {
            this.editionName = editionName;
            this.release = release;
            this.copies = copies;
        }

        public Edition() : this("Квант", new DateTime(1970, 1, 1), 5000) { }

        public string EditionName
        {
            get
            {
                return editionName;
            }
            set
            {
                editionName = value;
            }
        }

        public DateTime Release
        {
            get
            {
                return release;
            }
            set
            {
                release = value;
            }
        }

        public int Copies
        {
            get
            {
                return copies;
            }
            set
            {
                copies = value;
            }
        }

        public int Copies_s
        {
            get
            {
                return copies;
            }
            set
            {
                if(value < 0)
                {
                    throw new Exception("Тираж - неотрицательное число");
                }

                copies = value;
            }
        }

        public virtual object DeepCopy()
        {
            return new Edition(editionName, new DateTime(release.Year, release.Month, release.Day), copies);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Edition edition = (Edition)obj;

                if (editionName != edition.editionName ||
                    release != edition.release ||
                    copies != edition.copies)
                {
                    return false;
                }

                return true;
            }
        }
        public static bool operator ==(Edition e1, Edition e2)
        {
            return e1.Equals(e2);
        }

        public static bool operator !=(Edition e1, Edition e2)
        {
            return !e1.Equals(e2);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                hash = hash * 486187739 + editionName.GetHashCode();
                hash = hash * 486187739 + release.GetHashCode();
                hash = hash * 486187739 + copies.GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            return '\"' + editionName + '\"' + " " + release.ToString() + " тираж: " + copies;
        }

        public int CompareTo(object obj)
        {
            Edition p = obj as Edition;
            if (p != null)
                return editionName.CompareTo(p.editionName);
            else
                throw new Exception("Невозможно сравнить два объекта.");
        }

        public int Compare(Edition e1, Edition e2)
        {
            return e1.release.CompareTo(e2.release);
        }
    }
} 